using UnityEngine;

namespace GTAlpha
{
    [RequireComponent(typeof(Rigidbody))]
    public class Character : Actor
    {
        #region Properties

        public Rigidbody Rigidbody { get; private set; }

        #endregion

        #region Protected Functions

        protected override void Awake()
        {
            base.Awake();

            Rigidbody = GetComponent<Rigidbody>();
        }

        #endregion
    }
}