using System;
using UnityEngine;

namespace GTAlpha
{
    [Serializable]
    public class PlayerAttackForm
    {
        #region Serialized Fields

        [SerializeField] private PlayerAttackMotion[] singleTargetMotionArray;
        [SerializeField] private PlayerAttackMotion[] multipleTargetMotionArray;

        #endregion

        #region Properties
        
        public int CountOfSingleTargetMotions => singleTargetMotionArray.Length;
        public int CountOfMultipleTargetMotions => multipleTargetMotionArray.Length;

        #endregion

        #region Public Functions

        public PlayerAttackMotion GetSingleTargetMotion(int index)
        {
            if (index < 0 || index >= singleTargetMotionArray.Length)
            {
                Debug.LogErrorFormat("Out of Single Target Motion Index - Length : {0}, Index : {1}", singleTargetMotionArray.Length, index);
                return null;
            }

            return singleTargetMotionArray[index];
        }

        public PlayerAttackMotion GetMultipleTargetMotion(int index)
        {
            if (index < 0 || index >= multipleTargetMotionArray.Length)
            {
                Debug.LogErrorFormat("Out of Multiple Target Motion Index - Length : {0}, Index : {1}", multipleTargetMotionArray.Length, index);
                return null;
            }

            return multipleTargetMotionArray[index];
        }
        

        #endregion
    }
}