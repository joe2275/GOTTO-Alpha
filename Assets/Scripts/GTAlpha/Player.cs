using Camera3D;
using Manager;
using TriggerHandling;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

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
        private int mAttackMotionIndex;
        private int mAttackTimeMs;
        private int mAttackTimeDiffMs;

        private float mTimer;

        private string mWeaponForm;
        private int mWeaponFormIndex;

        #endregion

        #region Serialized Fields

        #endregion

        #region Properties

        public new PlayerStatus Status
        {
            get => mStatus;
            protected set => base.Status = mStatus = value;
        }
        
        public Vector3 MovementInertia { get; private set; }

        public bool CanMoveInMotion { get; set; }
        public float MoveSpeedRateInMotion { get; set; }
        
        public Transform TargetTransform { get; private set; }

        #endregion

        #region Public Functions

        #endregion

        #region Normal State Events

        protected override void StartOnNormal()
        {
            base.StartOnNormal();
            
            UpdateInertiaAndRotateBasedOnCamera(Constant.MovementInertiaDeltaOnGround, Time.deltaTime);
        }

        protected override void UpdateOnNormal()
        {
            base.UpdateOnNormal();
            
            UpdateInertiaAndRotateBasedOnCamera(Constant.MovementInertiaDeltaOnGround, Time.deltaTime);
        }

        protected override void FixedUpdateOnNormal()
        {
            base.FixedUpdateOnNormal();
            
            transform.Translate(MovementInertia * (Status.MoveSpeed * Time.fixedDeltaTime), Space.World);
        }

        #endregion

        #region Attack State Events

        protected override void StartOnAttack()
        {
            mWeaponForm = WeaponRepository.GetInformation(InventoryData.SeekWeaponSlot(0)).WeaponForm;
            mWeaponFormIndex = Weapon.GetWeaponFormIndex(mWeaponForm);

            PlayerAttackSystem.GetPlayerAttackMotionArray(mWeaponFormIndex, 5, mAttackMotions, out mAttackMotionCount);

            base.StartOnAttack();

            mTimer = 0.0f;
            mAttackMotionIndex = 0;

            mAttackTimeMs = (int) (mAttackMotions[mAttackMotionIndex].NextAttackTime * 1000.0f);
            AvatarAnimator.SetInteger(Constant.AnimationForm, mWeaponFormIndex);
            AvatarAnimator.SetInteger(Constant.AnimationKey, mAttackMotions[mAttackMotionIndex++].Key);
            AvatarAnimator.SetTrigger(Constant.AnimationAttack);
        }

        protected override void EndOnAttack()
        {
            base.EndOnAttack();
            mInput.AttackStarted = false;
        }

        protected override void UpdateOnAttack()
        {
            base.UpdateOnAttack();

            mTimer += Time.deltaTime;

            int timerMs = (int) (mTimer * 1000.0f);
            int curTimeDiffMs = Mathf.Abs(timerMs - mAttackTimeMs);

            // 현재 타이밍과 공격 타이밍의 시간 차이(ms)가 판정 기준 이내에 존재한다면
            if (curTimeDiffMs < Constant.AttackMissMaxTimeMs)
            {
                if (mAttackMotionIndex == mAttackMotionCount)
                {
                    State = NormalState;
                    return;
                }

                if (mInput.AttackStarted)
                {
                    mInput.AttackStarted = false;
                    mAttackTimeDiffMs = curTimeDiffMs;

                    mTimer = 0.0f;
                    mAttackTimeMs = (int) (mAttackMotions[mAttackMotionIndex].NextAttackTime * 1000.0f);
                    AvatarAnimator.SetInteger(Constant.AnimationKey, mAttackMotions[mAttackMotionIndex++].Key);
                    AvatarAnimator.SetTrigger(Constant.AnimationAttack);
                    return;
                }
            }
            // 현재 타이밍이 공격 타이밍의 판정 기준을 지난 경우
            else if (timerMs > curTimeDiffMs)
            {
                State = NormalState;
                return;
            }
            // 현재 타이밍이 공격 타이밍의 판정 기준에 도달하지 못한 경우
            else
            {
                mInput.AttackStarted = false;
            }

            if (CanMoveInMotion)
            {
                UpdateInertiaAndRotateBasedOnCamera(Constant.MovementInertiaDeltaOnGround, Time.deltaTime);
            }
        }

        protected override void FixedUpdateOnAttack()
        {
            base.FixedUpdateOnAttack();

            if (CanMoveInMotion)
            {
                
            }
        }

        #endregion

        #region Protected Functions

        protected override void Awake()
        {
            base.Awake();

            InputMaster inputMaster = new InputMaster();
            inputMaster.Enable();

            Status = new PlayerStatus();
            mInput = new PlayerInput(inputMaster);

            mCamera = GetComponentInChildren<ThirdPersonCamera>(true);

            mAttackMotions = new PlayerAttackMotion[10];
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

            if (mInput.LockOnStarted)
            {
                mInput.LockOnStarted = false;

                if (TargetTransform is null)
                {
                    TargetTransform = mCamera.SearchTarget();
                }
                else
                {
                    TargetTransform = null;
                    mCamera.TargetTransform = null;
                }
            }
        }

        #endregion

        #region Private Functions

        private void UpdateInertiaAndRotateBasedOnCamera(float inertiaDelta, float deltaTime)
        {
            Vector2 movement = mInput.NormalizedMovement;

            Vector3 cameraForward = mCamera.Forward;
            cameraForward = new Vector3(cameraForward.x, 0.0f, cameraForward.z).normalized;
            Vector3 cameraRight = mCamera.Right;
            cameraRight = new Vector3(cameraRight.x, 0.0f, cameraRight.z).normalized;

            Vector3 worldMovement = cameraForward * movement.y + cameraRight * movement.x;

            if (TargetTransform is null)
            {
                if (Mathf.Abs(movement.x) > Mathf.Epsilon || Mathf.Abs(movement.y) > Mathf.Epsilon)
                {
                    BodyTransform.rotation =
                        Quaternion.RotateTowards(BodyTransform.rotation, Quaternion.LookRotation(worldMovement),
                            Constant.MaxRotationDelta * deltaTime);
                }
                
                MovementInertia = Vector3.MoveTowards(MovementInertia, worldMovement, inertiaDelta * deltaTime);
                
                AvatarAnimator.SetFloat(Constant.AnimationFront, MovementInertia.magnitude);
                AvatarAnimator.SetFloat(Constant.AnimationRight, 0.0f);
            }
            else
            {
                Vector3 targetPosition = TargetTransform.position;
                targetPosition = new Vector3(targetPosition.x, 0.0f, targetPosition.z);
                Vector3 myPosition = CenterTransform.position;
                myPosition = new Vector3(myPosition.x, 0.0f, myPosition.z);

                BodyTransform.rotation = Quaternion.RotateTowards(BodyTransform.rotation,
                    Quaternion.LookRotation(targetPosition - myPosition), Constant.MaxRotationDelta * deltaTime);
                
                MovementInertia = Vector3.MoveTowards(MovementInertia, worldMovement, inertiaDelta * deltaTime);

                Vector3 localMovementInertia = Quaternion.FromToRotation(Forward, Vector3.forward) * MovementInertia;
                
                AvatarAnimator.SetFloat(Constant.AnimationFront, localMovementInertia.z);
                AvatarAnimator.SetFloat(Constant.AnimationRight, localMovementInertia.x);
            }
        }

        #endregion
    }
}