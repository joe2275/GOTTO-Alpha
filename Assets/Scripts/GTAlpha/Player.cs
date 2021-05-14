using System;
using UnityEngine;

namespace GTAlpha
{
    public class Player : Character
    {
        #region Fields

        private PlayerStatus mStatus;
        private PlayerInput mInput;

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
            set => base.Input = mInput = value;
        }

        #endregion

        #region Protected Methods

        protected override void Awake()
        {
            base.Awake();

            InputMaster inputMaster = new InputMaster();
            inputMaster.Enable();

            Status = new PlayerStatus();
            Input = new PlayerInput(inputMaster);
        }

        #endregion
    }
}