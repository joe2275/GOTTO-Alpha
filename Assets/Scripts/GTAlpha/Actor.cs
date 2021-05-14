using Manager;
using StateBase;
using UnityEngine;

namespace GTAlpha
{
    public class Actor : StateBase<int>
    {
        #region SerializedFields

        [SerializeField] private Transform bodyTransform;
        [SerializeField] private Animator bodyAnimator;
        [SerializeField] private Transform centerTransform;
        [SerializeField] private Transform forwardTransform;

        #endregion

        #region Fields

        #endregion

        #region Properties

        public Transform BodyTransform => bodyTransform;
        public Animator BodyAnimator => bodyAnimator;
        public Vector3 Forward => forwardTransform.position - centerTransform.position;
        public Status Status { get; protected set; }
        public ActorInput Input { get; protected set; }

        #endregion

        #region Public Methods

        public void Move(Vector3 movement, Space space = Space.Self)
        {
            transform.Translate(movement, space);
        }

        public void Rotate(Vector3 axis, float angle)
        {
            transform.Rotate(axis, angle);
        }

        public void RotateTo(Vector3 forward)
        {
            transform.rotation = Quaternion.FromToRotation(Forward, forward);
        }

        #endregion

        #region Idle State Events

        protected virtual void StartOnIdle()
        {
            // bodyAnimator.SetInteger(Constant.AnimationState, ActorState.Idle);
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
            // bodyAnimator.SetInteger(Constant.AnimationState, ActorState.Move);
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
            // 이동 입력 회전 각도
            // float movementAngle = Vector2.Angle(Vector2.up, movement);
            
            // 액터 전방 회전 각도
            // Vector3 realForward = Forward;
            // Vector2 forward = new Vector2(realForward.x, realForward.z);
            // float forwardAngle = Vector2.Angle(Vector2.up, forward);
            
            // 액터 전방 기준 이동
            Vector3 realMovement = new Vector3(movement.x, 0.0f, movement.y);
            
            Move(realMovement * (Status.MoveSpeed * Time.fixedDeltaTime));
        }

        #endregion

        #region Hit State Events

        protected virtual void StartOnHit()
        {
            bodyAnimator.SetInteger(Constant.AnimationState, ActorState.Hit);
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
            bodyAnimator.SetInteger(Constant.AnimationState, ActorState.Die);
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
