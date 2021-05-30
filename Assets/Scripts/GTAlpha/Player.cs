using System;
using Camera3D;
using UnityEngine;

namespace GTAlpha
{
    public class Player : Character
    {
        #region Fields

        private PlayerStatus mStatus;
        private PlayerInput mInput;
        private ThirdPersonCamera mCamera;

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

        #region Move State Events

        protected override void UpdateOnMove()
        {
            base.UpdateOnMove();
            
            Vector3 cameraForward = mCamera.Forward;
            cameraForward = new Vector3(cameraForward.x, 0.0f, cameraForward.z);

            Quaternion myRotation = Rotation;
            Quaternion cameraRotation = Quaternion.LookRotation(cameraForward);
            

            Rotation = Quaternion.RotateTowards(myRotation, cameraRotation, Constant.MaxDegreesDelta * Time.deltaTime);
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

            mCamera = GetComponentInChildren<ThirdPersonCamera>();
        }

        #endregion
    }
}