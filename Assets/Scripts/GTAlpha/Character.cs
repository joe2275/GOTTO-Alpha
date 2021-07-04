using StateBase;
using TriggerHandling;
using UnityEngine;

namespace GTAlpha
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : Actor
    {
        #region Fields

        private CharacterStatus mStatus;

        #endregion

        #region Properties

        public TriggerHandler TriggerHandler { get; private set; }
        public Rigidbody Rigidbody { get; private set; }
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