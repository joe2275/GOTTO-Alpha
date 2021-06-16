using System.Collections.Generic;
using UnityEngine;

namespace TriggerHandling
{
    public class TriggerHandler : MonoBehaviour
    {
        #region Fields

        private Dictionary<int, Trigger> mTriggerDict;

        #endregion

        #region Public Methods

        public Trigger GetTrigger(int key)
        {
            return mTriggerDict[key];
        }
        
        #endregion

        #region Private Methods

        private void Awake()
        {
            mTriggerDict = new Dictionary<int, Trigger>();
            Trigger[] triggers = GetComponentsInChildren<Trigger>(true);

            for (int i = 0; i < triggers.Length; i++)
            {
                mTriggerDict.Add(triggers[i].Key, triggers[i]);
            }
        }

        #endregion
    }
}
