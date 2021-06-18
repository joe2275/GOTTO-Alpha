using StateBase;
using UnityEngine;

namespace GTAlpha
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : Actor
    {
        #region State Constants

        public const int JumpState = 100;
        public const int ParkourState = 101;

        #endregion

        #region Serialized Fields

        [SerializeField] private Transform footCenterTransform;
        [SerializeField] private LayerMask groundLayer;

        #endregion

        #region Fields

        private CharacterInput mInput;

        #endregion

        #region Properties

        public Rigidbody Rigidbody { get; private set; }
        
        public new CharacterInput Input
        {
            get => mInput;
            protected set => base.Input = mInput = value;
        }

        public bool IsGround => Physics.Raycast(footCenterTransform.position, Vector3.down, Constant.GroundCriteria, groundLayer);

        #endregion

        #region Move State Events

        protected override void UpdateOnMove()
        {
            if (mInput.JumpStarted)
            {
                State = JumpState;
                return;
            }
            
            base.UpdateOnMove();
        }

        protected override void FixedUpdateOnMove()
        {
            if (!IsGround)
            {
                State = JumpState;
                return;
            }
            
            base.FixedUpdateOnMove();
        }

        #endregion

        #region Jump State Events

        protected virtual void StartOnJump()
        {
            if (mInput.JumpStarted)
            {
                mInput.JumpStarted = false;
                Vector3 velocity = Rigidbody.velocity;
                Rigidbody.velocity = new Vector3(velocity.x, Constant.JumpAcceleration, velocity.z);
            }
            
            AvatarAnimator.SetInteger(Constant.AnimationState, JumpState);
            
            AvatarAnimator.SetFloat(Constant.AnimationUp, Rigidbody.velocity.y);
        }

        protected virtual void EndOnJump()
        {
            
        }

        protected virtual void UpdateOnJump()
        {
            
        }

        protected virtual void FixedUpdateOnJump()
        {
            if (IsGround)
            {
                State = MoveState;
                return;
            }
            
            AvatarAnimator.SetFloat(Constant.AnimationUp, Rigidbody.velocity.y);
            
            AdjustMoveVelocity(Constant.MoveVelocityDeltaOnAir * Time.fixedDeltaTime);

            Vector3 forward = Forward;
            Vector3 right = Right;

            Move((forward * MoveVelocity.y + right * MoveVelocity.x) * (Status.MoveSpeed * Time.fixedDeltaTime), Space.World);
        }

        #endregion

        #region Parkour State Events

        protected virtual void StartOnParkour()
        {
            AvatarAnimator.SetInteger(Constant.AnimationState, ParkourState);
            // AvatarAnimator.SetTrigger(Constant.AnimationChanged);
        }

        protected virtual void EndOnParkour()
        {
            
        }

        protected virtual void UpdateOnParkour()
        {
            
        }

        protected virtual void FixedUpdateOnParkour()
        {
            
        }

        #endregion

        #region Protected Functions

        protected override void Awake()
        {
            base.Awake();

            Rigidbody = GetComponent<Rigidbody>();

            #region Set Jump State

            State<int> jump = new State<int>(JumpState)
            {
                OnStart = StartOnJump, OnEnd = EndOnJump, OnUpdate = UpdateOnJump, OnFixedUpdate = FixedUpdateOnJump
            };
            SetState(jump);

            #endregion

            #region Set Parkour State

            State<int> parkour = new State<int>(ParkourState)
            {
                OnStart = StartOnParkour, OnEnd = EndOnParkour, OnUpdate = UpdateOnParkour,
                OnFixedUpdate = FixedUpdateOnParkour
            };
            SetState(parkour);

            #endregion
        }
        
        #endregion
    }
}