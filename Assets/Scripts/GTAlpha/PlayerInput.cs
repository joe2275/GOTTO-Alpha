using UnityEngine;

namespace GTAlpha
{
    public class PlayerInput : CharacterInput
    {
        #region Fields

        private readonly InputMaster mInputMaster;

        #endregion

        #region Properties

        public override Vector2 Movement => mInputMaster.InGame.Move.ReadValue<Vector2>();
        public Vector2 Rotation => mInputMaster.InGame.Rotate.ReadValue<Vector2>();
        public bool AttackSingleTargetStarted { get; set; }
        public bool AttackMultipleTargetStarted { get; set; }
        public override bool JumpStarted { get; set; }

        #endregion

        public PlayerInput(InputMaster inputMaster)
        {
            mInputMaster = inputMaster;
            mInputMaster.InGame.AttackSingleTarget.started += context =>
            {
                AttackSingleTargetStarted = true;
            };

            mInputMaster.InGame.AttackMultipleTarget.started += context =>
            {
                AttackMultipleTargetStarted = true;
            };

            mInputMaster.InGame.JumpParkour.started += context =>
            {
                JumpStarted = true;
            };
        }
    }
}