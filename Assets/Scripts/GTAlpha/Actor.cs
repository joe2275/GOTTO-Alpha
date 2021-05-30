using Manager;
using StateBase;
using UnityEngine;

namespace GTAlpha
{
    public class Actor : StateBase<int>
    {
        #region SerializedFields

        [SerializeField] private Transform bodyTransform;
        [SerializeField] private Animator avatarAnimator;
        [SerializeField] private Transform centerTransform;
        [SerializeField] private Transform forwardTransform;

        #endregion

        #region Fields

        #endregion

        #region Properties

        public Transform BodyTransform => bodyTransform;
        public Animator AvatarAnimator => avatarAnimator;
        public Vector3 Forward => forwardTransform.position - centerTransform.position;

        public Vector3 Right
        {
            get
            {
                Vector3 forward = Forward;
                return new Vector3(forward.z, 0.0f, -forward.x);
            }
        }

        public Quaternion Rotation
        {
            get => bodyTransform.rotation;
            set => bodyTransform.rotation = value;
        }
        public Status Status { get; protected set; }
        public ActorInput Input { get; protected set; }

        #endregion

        #region Public Methods

        public void Move(Vector3 movement, Space space = Space.Self)
        {
            transform.Translate(movement, space);
        }

        public void Rotate(float angle)
        {
            bodyTransform.Rotate(Vector3.up, angle);
        }

        public void RotateTo(Vector3 forward)
        {
            bodyTransform.rotation = Quaternion.FromToRotation(Forward, forward);
        }

        #endregion

        #region Idle State Events

        protected virtual void StartOnIdle()
        {
            avatarAnimator.SetInteger(Constant.AnimationState, ActorState.Idle);
            avatarAnimator.SetTrigger(Constant.AnimationChanged);
        }

        protected virtual void EndOnIdle()
        {
            
        }

        protected virtual void UpdateOnIdle()
        {
            Vector2 movement = Input.Movement;
            if (Mathf.Abs(movement.x) > Mathf.Epsilon || Mathf.Abs(movement.y) > Mathf.Epsilon)
            {
                State = ActorState.Move;
            }
        }

        protected virtual void FixedUpdateOnIdle()
        {
            
        }

        #endregion

        #region Move State Events

        protected virtual void StartOnMove()
        {
            avatarAnimator.SetInteger(Constant.AnimationState, ActorState.Move);
            avatarAnimator.SetTrigger(Constant.AnimationChanged);
            avatarAnimator.SetFloat(Constant.AnimationFront, 0.0f);
            avatarAnimator.SetFloat(Constant.AnimationRight, 0.0f);
        }

        protected virtual void EndOnMove()
        {
            
        }

        protected virtual void UpdateOnMove()
        {
            Vector2 movement = Input.Movement;

            if (Mathf.Abs(movement.x) < Mathf.Epsilon && Mathf.Abs(movement.y) < Mathf.Epsilon)
            {
                State = ActorState.Idle;
            }
            
            avatarAnimator.SetFloat(Constant.AnimationFront, movement.y);
            avatarAnimator.SetFloat(Constant.AnimationRight, movement.x);
        }

        protected virtual void FixedUpdateOnMove()
        {
            Vector2 movement = Input.Movement;
            // 이동 입력 정규화
            float magnitude = movement.magnitude;
            if (magnitude > 1.0f + Mathf.Epsilon)
            {
                movement /= magnitude;
            }

            Vector3 forward = Forward;
            Vector3 right = Right;
            
            Move((forward * movement.y + right * movement.x) * (Status.MoveSpeed * Time.fixedDeltaTime), Space.World);
        }

        #endregion

        #region Hit State Events

        protected virtual void StartOnHit()
        {
            avatarAnimator.SetInteger(Constant.AnimationState, ActorState.Hit);
        }

        protected virtual void EndOnHit()
        {
            
        }

        protected virtual void UpdateOnHit()
        {
            
        }

        protected virtual void FixedUpdateOnHit()
        {
            
        }

        #endregion

        #region Die State Events

        protected virtual void StartOnDie()
        {
            avatarAnimator.SetInteger(Constant.AnimationState, ActorState.Die);
        }

        protected virtual void EndOnDie()
        {
            
        }

        protected virtual void UpdateOnDie()
        {
            
        }

        protected virtual void FixedUpdateOnDie()
        {
            
        }

        #endregion

        #region Protected Methods

        protected override void Awake()
        {
            base.Awake();

            #region Set Idle State

            ActorState idle = new ActorState(ActorState.Idle)
            {
                OnStart = StartOnIdle, OnEnd = EndOnIdle, OnUpdate = UpdateOnIdle, OnFixedUpdate = FixedUpdateOnIdle
            };
            SetState(idle);

            #endregion

            #region Set Move State

            ActorState move = new ActorState(ActorState.Move)
            {
                OnStart = StartOnMove, OnEnd = EndOnMove, OnUpdate = UpdateOnMove, OnFixedUpdate = FixedUpdateOnMove
            };
            SetState(move);

            #endregion

            #region Set Hit State

            ActorState hit = new ActorState(ActorState.Hit)
            {
                OnStart = StartOnHit, OnEnd = EndOnHit, OnUpdate = UpdateOnHit, OnFixedUpdate = FixedUpdateOnHit
            };
            SetState(hit);

            #endregion

            #region Set Die State

            ActorState die = new ActorState(ActorState.Die)
            {
                OnStart = StartOnDie, OnEnd = EndOnDie, OnUpdate = UpdateOnDie, OnFixedUpdate = FixedUpdateOnDie
            };
            SetState(die);

            #endregion
        }

        protected virtual void Start()
        {
            State = ActorState.Idle;
        }

        protected override void Update()
        {
            if (StateManager.Pause.State)
            {
                return;
            }
            
            base.Update();
        }

        protected override void FixedUpdate()
        {
            if (StateManager.Pause.State)
            {
                return;
            }
            
            base.FixedUpdate();
        }

        #endregion
    }
}