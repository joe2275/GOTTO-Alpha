using UnityEngine;

namespace GTAlpha
{
    public class PlayerInput : ActorInput
    {
        #region Fields

        private readonly InputMaster mInputMaster;

        #endregion

        #region Properties

        public override Vector2 Movement => mInputMaster.InGame.Move.ReadValue<Vector2>();
        public Vector2 Rotation => mInputMaster.InGame.Rotate.ReadValue<Vector2>();

        #endregion

        public PlayerInput(InputMaster inputMaster)
        {
            mInputMaster = inputMaster;
        }
    }
}