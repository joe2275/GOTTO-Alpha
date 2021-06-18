using System;
using Camera3D;
using Manager;
using UnityEngine;

namespace GTAlpha
{
    public class Player : Character
    {
        #region Fields

        private PlayerStatus mStatus;
        private PlayerInput mInput;
        private ThirdPersonCamera mCamera;
        private PlayerAttackMotion[] mAttackMotions;
        private int mAttackMotionCount;

        private PlayerAnimationEvent mAnimationEvent;
        private PlayerTransparentAnimationEvent mTransparentAnimationEvent;

        #endregion

        #region Serialized Fields

        [SerializeField] private Animator transparentAnimator;

        #endregion

        #region Properties

        public new PlayerStatus Status
        {
            get => mStatus;
            set => base.Status = mStatus = value;
        }

        public new PlayerInput Input
        {
            get => mInput;
            protected set => base.Input = mInput = value;
        }

        #endregion

        #region Move State Events

        protected override void UpdateOnMove()
        {
            // if (mInput.AttackSingleTargetStarted || mInput.AttackMultipleTargetStarted)
            // {
            //     State = ReadyToAttackState;
            //     return;
            // }

            base.UpdateOnMove();

            Vector2 moveVelocity = MoveVelocity;

            if (Mathf.Abs(moveVelocity.x) > Mathf.Epsilon || Mathf.Abs(moveVelocity.y) > Mathf.Epsilon)
            {
                RotateTowardCamera(Time.deltaTime);
            }
        }

        #endregion

        #region Jump State Events

        protected override void UpdateOnJump()
        {
            base.UpdateOnJump();

            Vector2 moveVelocity = MoveVelocity;

            if (Mathf.Abs(moveVelocity.x) > Mathf.Epsilon || Mathf.Abs(moveVelocity.y) > Mathf.Epsilon)
            {
                RotateTowardCamera(Time.deltaTime);
            }
        }

        #endregion

        #region Ready To Attack State Events

        protected override void StartOnReadyToAttack()
        {
            base.StartOnReadyToAttack();

            AttackWay attackWay;
            if (mInput.AttackSingleTargetStarted)
            {
                mInput.AttackSingleTargetStarted = false;
                attackWay = AttackWay.Single;
            }
            else
            {
                attackWay = AttackWay.Multiple;
            }

            PlayerAttackSystem.GetPlayerAttackMotionArray(
                WeaponRepository.GetInformation(InventoryData.SeekWeaponSlot(0)).WeaponForm, attackWay, 5,
                mAttackMotions, out mAttackMotionCount);
        }

        protected override void UpdateOnReadyToAttack()
        {
            base.UpdateOnReadyToAttack();

            RotateTowardCamera(Time.deltaTime);

            if (mAnimationEvent.IsEndOfAnimation)
            {
                mAnimationEvent.IsEndOfAnimation = false;
                transparentAnimator.gameObject.SetActive(true);
            }
            else if (mTransparentAnimationEvent.IsEndOfTransparentMotions)
            {
                mTransparentAnimationEvent.IsEndOfTransparentMotions = false;
                transparentAnimator.gameObject.SetActive(false);
            }
        }

        #endregion

        #region Attack State Events

        protected override void StartOnAttack()
        {
            base.StartOnAttack();
        }

        protected override void EndOnAttack()
        {
            base.EndOnAttack();
        }

        protected override void UpdateOnAttack()
        {
            base.UpdateOnAttack();
        }

        protected override void FixedUpdateOnAttack()
        {
            base.FixedUpdateOnAttack();
        }

        #endregion

        #region Protected Functions

        protected override void Awake()
        {
            base.Awake();

            InputMaster inputMaster = new InputMaster();
            inputMaster.Enable();

            Status = new PlayerStatus();
            Input = new PlayerInput(inputMaster);

            mCamera = GetComponentInChildren<ThirdPersonCamera>(true);

            mAttackMotions = new PlayerAttackMotion[10];
            mAnimationEvent = GetComponentInChildren<PlayerAnimationEvent>(true);
            mTransparentAnimationEvent = GetComponentInChildren<PlayerTransparentAnimationEvent>(true);
        }

        protected override void Start()
        {
            base.Start();

            // Equip Test Weapon ...
            InventoryData.GetWeaponData(0).IsLocked = false;
            InventoryData.EquipWeapon(0, 0);
        }

        protected override void Update()
        {
            if (StateManager.Pause.State)
            {
                return;
            }

            base.Update();

            mCamera.Rotate(mInput.Rotation * SettingsManager.Sensitivity);
        }

        #endregion

        #region Private Functions

        private void RotateTowardCamera(float deltaTime)
        {
            Vector3 cameraForward = mCamera.Forward;
            cameraForward = new Vector3(cameraForward.x, 0.0f, cameraForward.z);

            Quaternion myRotation = Rotation;
            Quaternion cameraRotation = Quaternion.LookRotation(cameraForward);

            Rotation = Quaternion.RotateTowards(myRotation, cameraRotation, Constant.MaxDegreesDelta * deltaTime);
        }

        #endregion
    }
}