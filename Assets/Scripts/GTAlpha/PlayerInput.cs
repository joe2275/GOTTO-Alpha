using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// Player 가 유저의 입력에 의해 제어되기 위해 입력값을 Player 클래스가 사용하기 쉽도록 만들기 위한 PlayerInput 클래스
    /// </summary>
    public static class PlayerInput
    {
        #region Fields

        private static InputMaster _inputMaster;

        #endregion

        #region Properties

        /// <summary>
        /// 캐릭터 움직임을 위한 유저의 움직임 입력값
        /// </summary>
        public static Vector2 Movement => _inputMaster.InGame.Move.ReadValue<Vector2>();

        /// <summary>
        /// 캐릭터 움직임을 위한 정규화된 유저의 움직임 입력값
        /// </summary>
        public static Vector2 NormalizedMovement
        {
            get
            {
                Vector2 movement = Movement;
                // 이동 입력 정규화
                float magnitude = movement.magnitude;
                if (magnitude > 1.0f + Mathf.Epsilon)
                {
                    movement /= magnitude;
                }

                return movement;
            }
        }
        /// <summary>
        /// 시점 회전을 위한 유저로 부터 입력된 회전값
        /// </summary>
        public static Vector2 Rotation => _inputMaster.InGame.Rotate.ReadValue<Vector2>();
        /// <summary>
        /// 캐릭터 공격을 위해 유저로 부터 입력된 공격 버튼 입력값으로 눌리는 처음 순간에만 기록된다. 
        /// </summary>
        public static bool AttackStarted { get; set; }
        /// <summary>
        /// 캐릭터의 시점 고정을 위해 유저로 부터 입력된 시점 고정 버튼 입력값으로 눌리는 처음 순간에만 기록된다. 
        /// </summary>
        public static bool LockOnStarted { get; set; }
        /// <summary>
        /// 캐릭터 회피 동작을 위해 유저로 부터 입력된 회피 버튼 입력값으로 눌리는 처음 순간에만 기록된다. 
        /// </summary>
        public static bool EvadeStarted { get; set; }

        #endregion

        #region Public Functions

        public static void Enable()
        {
            if (_inputMaster is null)
            {
                _inputMaster = new InputMaster();
                _inputMaster.InGame.Attack.started += context =>
                {
                    AttackStarted = true;
                };
                _inputMaster.InGame.LockOn.started += context =>
                {
                    LockOnStarted = true;
                };
                _inputMaster.InGame.Evade.started += context =>
                {
                    EvadeStarted = true;
                };
            }
            _inputMaster.Enable();
        }

        public static void Disable()
        {
            _inputMaster?.Disable();
        }

        public static void Reset()
        {
            AttackStarted = false;
            LockOnStarted = false;
        }

        #endregion
    }
}