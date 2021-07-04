using System;
using UnityEngine;

namespace GTAlpha
{
    [Serializable]
    public class PlayerAttackMotion
    {
        #region Serialized Fields

        [SerializeField] private int key;

        [SerializeField] private int[] connectionKeyArrayIn;
        [SerializeField] private int[] connectionKeyArrayOut;

        [SerializeField] private float nextAttackTime;

        #endregion

        #region Properties

        public int Key => key;

        public int CountOfConnectionKeysIn => connectionKeyArrayIn.Length;
        public int CountOfConnectionKeysOut => connectionKeyArrayOut.Length;
        public float NextAttackTime => nextAttackTime;

        #endregion

        #region Public Functions

        public int GetConnectionTypeIn(int index)
        {
            if (index < 0 || index >= connectionKeyArrayIn.Length)
            {
                Debug.LogErrorFormat("Out of Connection Key In Index - Length : {0}, Index : {1}", connectionKeyArrayIn.Length, index);
                return -1;
            }

            return connectionKeyArrayIn[index];
        }
        
        public int GetConnectionTypeOut(int index)
        {
            if (index < 0 || index >= connectionKeyArrayOut.Length)
            {
                Debug.LogErrorFormat("Out of Connection Key Out Index - Length : {0}, Index : {1}", connectionKeyArrayOut.Length, index);
                return -1;
            }

            return connectionKeyArrayOut[index];
        }

        #endregion
    }
}