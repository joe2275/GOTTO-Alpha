using System;
using System.Collections.Generic;
using UnityEngine;

namespace TriggerHandling
{
    public class Trigger : MonoBehaviour
    {
        #region Serialized Fields

        [SerializeField] private int key;
        [SerializeField] private bool isFixedSpace;
        [SerializeField] private uint maxCapacity = 50;
        [SerializeField] private bool useTags;
        [SerializeField] private string[] detectableTags;

        #endregion

        #region Fields

        private Collider mTrigger;
        private Collider[] mTriggeredColliderArray;
        private int mTriggeredColliderCountInArray;
        private List<Collider> mTriggeredColliderList;
        private bool mIsUpdated;

        #endregion

        #region Properties

        public int Key => key;

        public event Action<Collider> OnEnter;
        public event Action<Collider> OnExit;

        public bool IsTriggered => mTriggeredColliderList.Count > 0;

        #endregion

        #region Public Methods

        public Collider[] GetTriggeredColliders(out int count)
        {
            if (mIsUpdated)
            {
                mIsUpdated = false;

                int index = 0;
                mTriggeredColliderList.Find(delegate(Collider collision)
                {
                    mTriggeredColliderArray[index++] = collision;
                    return index == mTriggeredColliderArray.Length;
                });

                mTriggeredColliderCountInArray = index;
            }

            count = mTriggeredColliderCountInArray;
            return mTriggeredColliderArray;
        }
        
        public void OnTriggerEnter(Collider collision)
        {
            mIsUpdated = true;
            mTriggeredColliderList.Add(collision);
            OnEnter?.Invoke(collision);
        }

        public void OnTriggerExit(Collider collision)
        {
            mIsUpdated = true;
            mTriggeredColliderList.Remove(collision);
            OnExit?.Invoke(collision);
        }

        #endregion

        #region Private Methods

        private void OnDisable()
        {
            mIsUpdated = true;

            mTriggeredColliderList.ForEach(delegate(Collider collision) { OnExit?.Invoke(collision); });
            mTriggeredColliderList.Clear();
        }

        private void Awake()
        {
            mTrigger = GetComponent<Collider>();
            mTriggeredColliderArray = new Collider[maxCapacity];
            mTriggeredColliderList = new List<Collider>();
        }

        private void Start()
        {
            if (isFixedSpace)
            {
                transform.SetParent(null);
            }
        }

        #endregion
    }
}