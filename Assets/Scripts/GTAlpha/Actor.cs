using Manager;
using StateBase;
using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// 게임에서 어떠한 방식으로든 움직이거나 상호작용 할 수 있는 모든 객체들이 상속하는 Actor 클래스
    /// </summary>
    [RequireComponent(typeof(Animator))]
    public class Actor : StateBase<int>
    {
        #region Constant

        public const int NormalState = 0;
        public const int HitState = 1;
        public const int DieState = 2;
        public const int AttackState = 3;

        #endregion
        
        #region Serialized Fields
        
        [SerializeField] private Transform centerTransform;
        [SerializeField] private Transform forwardTransform;
        [SerializeField] private Transform rightTransform;
        
        #endregion

        #region Fields

        #endregion

        #region Properties
        
        /// <summary>
        /// Actor의 최상위 GameObject가 가지는 Animator
        /// </summary>
        public Animator Animator { get; private set; }
        /// <summary>
        /// Actor의 중심이 되는 위치
        /// </summary>
        public Transform CenterTransform => centerTransform;
        /// <summary>
        /// Actor의 앞을 가리키는 정규화된 위치
        /// </summary>
        public Vector3 Forward => forwardTransform.position - centerTransform.position;
        /// <summary>
        /// Actor의 오른쪽을 가리키는 정규화된 위치
        /// </summary>
        public Vector3 Right => rightTransform.position - centerTransform.position;
        /// <summary>
        /// Actor가 사용하는 게임적 수치를 관리하는 Status
        /// </summary>
        public Status Status { get; protected set; }

        #endregion

        #region Normal State Events

        protected virtual void StartOnNormal()
        {
            Animator.SetInteger(Constant.AnimationState, NormalState);
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
            Animator.SetInteger(Constant.AnimationState, HitState);
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
            Animator.SetInteger(Constant.AnimationState, DieState);
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
            Animator.SetInteger(Constant.AnimationState, AttackState);
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
            
            Animator = GetComponent<Animator>();

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