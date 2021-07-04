using UnityEngine;

namespace GTAlpha
{
    public class PlayerInput
    {
        #region Fields

        private readonly InputMaster mInputMaster;

        #endregion

        #region Properties

        public Vector2 Movement => mInputMaster.InGame.Move.ReadValue<Vector2>();

        public Vector2 NormalizedMovement
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
        public Vector2 Rotation => mInputMaster.InGame.Rotate.ReadValue<Vector2>();
        
        public bool AttackStarted { get; set; }
        
        public bool LockOnStarted { get; set; }

        #endregion

        public PlayerInput(InputMaster inputMaster)
        {
            mInputMaster = inputMaster;
            mInputMaster.InGame.Attack.started += context =>
            {
                AttackStarted = true;
            };
            mInputMaster.InGame.LockOn.started += context =>
            {
                LockOnStarted = true;
            };
        }
    }
}