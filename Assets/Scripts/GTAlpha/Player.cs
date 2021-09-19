using Camera3D;
using Manager;
using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// 게임에서 유저가 제어하는 Player 클래스
    /// </summary>
    public class Player : Character
    {
        #region Fields

        private PlayerStatus mStatus;

        // 공격 관련 변수
        private PlayerAttackMotion mAttackMotion;
        private string mWeaponForm;
        private int mWeaponFormIndex;

        private bool mIsEndOfMotion;
        private bool mCanRotate;
        private bool mCanAttack;
        private int mAttackKey;

        #endregion

        #region Serialized Fields


        #endregion

        #region Properties

        /// <summary>
        /// 플레이어가 사용하는 게임적 수치를 저장하고 관리하는 Status
        /// </summary>
        public new PlayerStatus Status
        {
            get => mStatus;
            protected set => base.Status = mStatus = value;
        }
        /// <summary>
        /// 플레이어 캐릭터가 이동할 때 사용되는 이동 관성
        /// </summary>
        public Vector3 MovementInertia { get; private set; }

        public Transform TargetTransform { get; private set; }

        #endregion

        #region Public Functions

        #endregion

        #region Normal State Events

        protected override void StartOnNormal()
        {
            base.StartOnNormal();

            UpdateInertiaAndRotationBasedOnCamera(Constant.MovementInertiaDeltaOnGround, Time.deltaTime);
        }

        protected override void UpdateOnNormal()
        {
            base.UpdateOnNormal();

            UpdateInertiaAndRotationBasedOnCamera(Constant.MovementInertiaDeltaOnGround, Time.deltaTime);

            if (PlayerInput.AttackStarted)
            {
                State = AttackState;
            }
        }

        protected override void FixedUpdateOnNormal()
        {
            base.FixedUpdateOnNormal();

            // 이동 관성을 이용하여 플레이어 캐릭터를 이동시킨다. 
            transform.Translate(MovementInertia * (Status.MoveSpeed * Time.fixedDeltaTime), Space.World);
        }

        #endregion

        #region Attack State Events

        protected override void StartOnAttack()
        {
            base.StartOnAttack();
            
            // 무기 형태 값 구하기
            mWeaponForm = WeaponInfoTable.GetInformation(InventoryData.SeekWeaponSlot(0)).WeaponForm;
            // 무기 형태 인덱스 값 구하기
            mWeaponFormIndex = Weapon.GetWeaponFormIndex(mWeaponForm);

            // 첫 번째 공격 모션 구하기
            mAttackMotion = PlayerAttackSystem.GetPlayerAttackMotionStart(mWeaponFormIndex);
            
            // 공격에 필요한 각종 변수 초기화
            PlayerInput.AttackStarted = false;
            PlayerAttackTimer.TimerOn(mAttackMotion.NextAttackTime);
            mIsEndOfMotion = false;
            mCanRotate = false;
            mCanAttack = false;
            
            // 애니메이터 파라미터 설정
            Animator.SetInteger(Constant.AnimationForm, mWeaponFormIndex);
            Animator.SetInteger(Constant.AnimationKey, mAttackMotion.Key);
            Animator.SetTrigger(Constant.AnimationAttack);
        }

        protected override void EndOnAttack()
        {
            base.EndOnAttack();
            PlayerInput.AttackStarted = false;
            PlayerAttackTimer.TimerOff();
        }

        protected override void UpdateOnAttack()
        {
            base.UpdateOnAttack();

            if (mIsEndOfMotion)
            {
                State = NormalState;
                return;
            }

            if (mCanRotate)
            {
                UpdateRotationBasedOnCamera(Time.deltaTime);
            }

            if (!PlayerAttackTimer.IsRecorded) return;
            
            mAttackMotion = PlayerAttackSystem.GetPlayerAttackMotionNext(mWeaponFormIndex, mAttackMotion);
            PlayerAttackTimer.TimerOn(mAttackMotion.NextAttackTime);
                
            Animator.SetInteger(Constant.AnimationKey, mAttackMotion.Key);
            Animator.SetTrigger(Constant.AnimationAttack);
        }

        protected override void FixedUpdateOnAttack()
        {
            base.FixedUpdateOnAttack();

            if (mCanAttack && TriggerHandler.GetTrigger(mAttackKey).IsTriggered)
            {
                print("Enemy Triggered!");
            }
        }

        #endregion

        #region Protected Functions

        protected override void Awake()
        {
            base.Awake();

            mStatus = PlayerStatus.Instance;
            PlayerInput.Enable();
            PlayerInput.Reset();
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

            ThirdPersonCamera.Main.Rotate(PlayerInput.Rotation * SettingsManager.Sensitivity);

            if (PlayerInput.LockOnStarted)
            {
                PlayerInput.LockOnStarted = false;

                if (TargetTransform is null)
                {
                    TargetTransform = ThirdPersonCamera.Main.SearchTarget();
                }
                else
                {
                    TargetTransform = null;
                    ThirdPersonCamera.Main.TargetTransform = null;
                }
            }
        }

        #endregion

        #region Private Functions

        /// <summary>
        /// 카메라가 보는 방향을 기준으로 플레이어 캐릭터를 회전하고 이동 관성을 갱신하는 함수
        /// </summary>
        /// <param name="inertiaDelta"></param>
        /// <param name="deltaTime"></param>
        private void UpdateInertiaAndRotationBasedOnCamera(float inertiaDelta, float deltaTime)
        {
            // 유저가 입력한 정규화된 이동 입력 값
            Vector2 movement = PlayerInput.NormalizedMovement;

            // 카메라의 전방, 우측 좌표를 가져온다. 
            Vector3 cameraForward = ThirdPersonCamera.Main.Forward;
            cameraForward = new Vector3(cameraForward.x, 0.0f, cameraForward.z).normalized;
            Vector3 cameraRight = ThirdPersonCamera.Main.Right;
            cameraRight = new Vector3(cameraRight.x, 0.0f, cameraRight.z).normalized;

            // 카메라 좌표계를 이용하여 월드 좌표계에서 이동해야 하는 이동 값을 계산한다. 
            Vector3 worldMovement = cameraForward * movement.y + cameraRight * movement.x;

            // 카메라가 집중하고 있는 대상이 존재하지 않는 경우
            if (TargetTransform is null)
            {
                // 움직임이 갑지된 경우, 플레이어의 회전값을 카메라 좌표계에서 월드 좌표계로 계산한 이동 값으로 플레이어 캐릭터의 전방 축을 회전한다. 
                if (Mathf.Abs(movement.x) > Mathf.Epsilon || Mathf.Abs(movement.y) > Mathf.Epsilon)
                {
                    transform.rotation =
                        Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(worldMovement),
                            Constant.MaxRotationDelta * deltaTime);
                }

                // 이동 관성을 갱신
                MovementInertia = Vector3.MoveTowards(MovementInertia, worldMovement, inertiaDelta * deltaTime);
                
                /*
                 * 애니메이터 파라미터 갱신
                 * 카메라가 집중하는 대상이 없는 경우
                 * 플레이어 캐릭터는 보는 방향으로 이동한다. 
                 */
                Animator.SetFloat(Constant.AnimationFront, MovementInertia.magnitude);
                Animator.SetFloat(Constant.AnimationRight, 0.0f);
            }
            // 카메라가 집중하고 있는 대상이 존재하는 경우
            else
            {
                Vector3 targetPosition = TargetTransform.position;
                targetPosition = new Vector3(targetPosition.x, 0.0f, targetPosition.z);
                Vector3 myPosition = CenterTransform.position;
                myPosition = new Vector3(myPosition.x, 0.0f, myPosition.z);

                // 플레이어 전방 축을 지속적으로 타겟을 향하게 만든다. 
                transform.rotation = Quaternion.RotateTowards(transform.rotation,
                    Quaternion.LookRotation(targetPosition - myPosition), Constant.MaxRotationDelta * deltaTime);
                
                // 이동 관성을 갱신
                MovementInertia = Vector3.MoveTowards(MovementInertia, worldMovement, inertiaDelta * deltaTime);

                Vector3 localMovementInertia = Quaternion.FromToRotation(Forward, Vector3.forward) * MovementInertia;

                /*
                 * 애니메이터 파라미터 갱신
                 * 카메라에 집중하는 대상이 존재하는 경우
                 * 플레이어 캐릭터가 보는 방향은 무조건 집중하는 대상을 향한다.
                 */
                Animator.SetFloat(Constant.AnimationFront, localMovementInertia.z);
                Animator.SetFloat(Constant.AnimationRight, localMovementInertia.x);
            }
        }

        private void UpdateRotationBasedOnCamera(float deltaTime)
        {
            if (TargetTransform is null)
            {
                // 유저가 입력한 정규화된 이동 입력 값
                Vector2 movement = PlayerInput.NormalizedMovement;

                // 움직임이 갑지된 경우, 플레이어의 회전값을 카메라 좌표계에서 월드 좌표계로 계산한 이동 값으로 플레이어 캐릭터의 전방 축을 회전한다. 
                if (Mathf.Abs(movement.x) > Mathf.Epsilon || Mathf.Abs(movement.y) > Mathf.Epsilon)
                {
                    // 카메라의 전방, 우측 좌표를 가져온다. 
                    Vector3 cameraForward = ThirdPersonCamera.Main.Forward;
                    cameraForward = new Vector3(cameraForward.x, 0.0f, cameraForward.z).normalized;
                    Vector3 cameraRight = ThirdPersonCamera.Main.Right;
                    cameraRight = new Vector3(cameraRight.x, 0.0f, cameraRight.z).normalized;

                    // 카메라 좌표계를 이용하여 월드 좌표계에서 이동해야 하는 이동 값을 계산한다. 
                    Vector3 worldMovement = cameraForward * movement.y + cameraRight * movement.x;
                    
                    transform.rotation =
                        Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(worldMovement),
                            Constant.MaxRotationDeltaWhileAttack * deltaTime);
                }
            }
            else
            {
                Vector3 targetPosition = TargetTransform.position;
                targetPosition = new Vector3(targetPosition.x, 0.0f, targetPosition.z);
                Vector3 myPosition = CenterTransform.position;
                myPosition = new Vector3(myPosition.x, 0.0f, myPosition.z);

                // 플레이어 전방 축을 지속적으로 타겟을 향하게 만든다. 
                transform.rotation = Quaternion.RotateTowards(transform.rotation,
                    Quaternion.LookRotation(targetPosition - myPosition), Constant.MaxRotationDelta * deltaTime);
            }
        }

        private void Animation_EndOfMotion()
        {
            mIsEndOfMotion = true;
        }

        private void Animation_CanRotate(int enable)
        {
            mCanRotate = enable != 0;
        }

        private void Animation_CanAttack(int enable)
        {
            mCanAttack = enable != 0;
        }

        private void Animation_SetAttackTriggerKey(int key)
        {
            mAttackKey = key;
        }

        #endregion
    }
}