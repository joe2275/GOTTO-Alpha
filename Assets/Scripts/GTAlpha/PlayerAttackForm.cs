using System;
using UnityEngine;

namespace GTAlpha
{
    [Serializable]
    public class PlayerAttackForm
    {
        #region Serialized Fields

        [SerializeField] private PlayerAttackMotion[] attackMotionArray;

        #endregion

        #region Properties
        
        public int CountOfAttackMotions => attackMotionArray.Length;

        #endregion

        #region Public Functions

        public PlayerAttackMotion GetAttackMotion(int index)
        {
            if (index < 0 || index >= attackMotionArray.Length)
            {
                Debug.LogErrorFormat("Out of Attack Motion Index - Length : {0}, Index : {1}", attackMotionArray.Length, index);
                return null;
            }

            return attackMotionArray[index];
        }

        #endregion
    }
}