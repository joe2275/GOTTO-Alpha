using TriggerHandling;
using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// 게임 내에 존재하는 객체 중 물리적인 상호작용을 하는 모든 객체가 상속하는 Character 클래스
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(TriggerHandler))]
    public class Character : Actor
    {
        #region Fields

        private CharacterStatus mStatus;

        #endregion

        #region Properties
        /// <summary>
        /// 객체가 가지는 트리거를 관리한다.
        /// </summary>
        public TriggerHandler TriggerHandler { get; private set; }
        /// <summary>
        /// 객체가 가지는 Rigidbody 객체
        /// </summary>
        public Rigidbody Rigidbody { get; private set; }
        /// <summary>
        /// Character 클래스에서 사용되는 게임적 수치를 저장하고 관리하는 Status
        /// </summary>
        public new CharacterStatus Status
        {
            get => mStatus;
            protected set => base.Status = mStatus = value;
        }
        

        #endregion

        #region Protected Functions

        protected override void Awake()
        {
            base.Awake();

            Rigidbody = GetComponent<Rigidbody>();
            TriggerHandler = GetComponent<TriggerHandler>();
        }
        
        #endregion
    }
}