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

        #endregion

        #region Public Methods

        public void Move(Vector3 movement, Space space)
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

        public virtual void StartOnIdle()
        {
            bodyAnimator.SetInteger(Constant.AnimationState, ActorState.Idle);
        }

        public virtual void EndOnIdle()
        {
            
        }

        public virtual void UpdateOnIdle()
        {
            
        }

        public virtual void FixedUpdateOnIdle()
        {
            
        }

        #endregion

        #region Move State Events

        public virtual void StartOnMove()
        {
            bodyAnimator.SetInteger(Constant.AnimationState, ActorState.Move);
        }

        public virtual void EndOnMove()
        {
            
        }

        public virtual void UpdateOnMove()
        {
            
        }

        public virtual void FixedUpdateOnMove()
        {
            
        }

        #endregion

        #region Hit State Events

        public virtual void StartOnHit()
        {
            bodyAnimator.SetInteger(Constant.AnimationState, ActorState.Hit);
        }

        public virtual void EndOnHit()
        {
            
        }

        public virtual void UpdateOnHit()
        {
            
        }

        public virtual void FixedUpdateOnHit()
        {
            
        }

        #endregion

        #region Die State Events

        public virtual void StartOnDie()
        {
            bodyAnimator.SetInteger(Constant.AnimationState, ActorState.Die);
        }

        public virtual void EndOnDie()
        {
            
        }

        public virtual void UpdateOnDie()
        {
            
        }

        public virtual void FixedUpdateOnDie()
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
