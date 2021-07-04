using Manager;
using StateBase;
using TriggerHandling;
using UnityEngine;

namespace GTAlpha
{
    public class Actor : StateBase<int>
    {
        #region State Constants

        public const int NormalState = 0;
        public const int HitState = 1;
        public const int DieState = 2;
        public const int AttackState = 3;

        #endregion

        #region Serialized Fields

        [SerializeField] private Transform bodyTransform;
        [SerializeField] private Animator avatarAnimator;
        [SerializeField] private Transform centerTransform;
        [SerializeField] private Transform forwardTransform;
        [SerializeField] private Transform rightTransform;
        
        #endregion

        #region Fields

        #endregion

        #region Properties

        public Animator AvatarAnimator => avatarAnimator;

        public Transform BodyTransform => bodyTransform;
        public Transform CenterTransform => centerTransform;

        public Vector3 Forward => forwardTransform.position - centerTransform.position;

        public Vector3 Right => rightTransform.position - centerTransform.position;

        public Status Status { get; protected set; }

        #endregion

        #region Normal State Events

        protected virtual void StartOnNormal()
        {
            avatarAnimator.SetInteger(Constant.AnimationState, NormalState);
        }

        protected virtual void EndOnNormal()
        {
        }

        protected virtual void UpdateOnNormal()
        {
        }

        protected virtual void FixedUpdateOnNormal()
        {
        }

        #endregion

        #region Hit State Events

        protected virtual void StartOnHit()
        {
            AvatarAnimator.SetInteger(Constant.AnimationState, HitState);
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
            AvatarAnimator.SetInteger(Constant.AnimationState, DieState);
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

        #region Attack State Events

        protected virtual void StartOnAttack()
        {
            AvatarAnimator.SetInteger(Constant.AnimationState, AttackState);
        }

        protected virtual void EndOnAttack()
        {
        }

        protected virtual void UpdateOnAttack()
        {
        }

        protected virtual void FixedUpdateOnAttack()
        {
        }

        #endregion

        #region Protected Functions

        protected override void Awake()
        {
            base.Awake();

            #region Set Normal State

            State<int> normal = new State<int>(NormalState)
            {
                OnStart = StartOnNormal, OnEnd = EndOnNormal, OnUpdate = UpdateOnNormal,
                OnFixedUpdate = FixedUpdateOnNormal
            };
            SetState(normal);

            #endregion

            #region Set Hit State

            State<int> hit = new State<int>(HitState)
            {
                OnStart = StartOnHit, OnEnd = EndOnHit, OnUpdate = UpdateOnHit, OnFixedUpdate = FixedUpdateOnHit
            };
            SetState(hit);

            #endregion

            #region Set Die State

            State<int> die = new State<int>(DieState)
            {
                OnStart = StartOnDie, OnEnd = EndOnDie, OnUpdate = UpdateOnDie, OnFixedUpdate = FixedUpdateOnDie
            };
            SetState(die);

            #endregion

            #region Set Attack State

            State<int> attack = new State<int>(AttackState)
            {
                OnStart = StartOnAttack, OnEnd = EndOnAttack, OnUpdate = UpdateOnAttack,
                OnFixedUpdate = FixedUpdateOnAttack
            };
            SetState(attack);

            #endregion
        }

        protected virtual void Start()
        {
            State = NormalState;
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