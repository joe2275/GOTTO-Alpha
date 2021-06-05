using System;
using UnityEngine;

namespace GTAlpha
{
    /*
     * WeaponData
     * Weapon 관련 정보 중 지속적으로 변하고 저장되어야 하는 정보를 저장한다. 
     */
    [Serializable]
    public class WeaponData
    {
        #region Serialized Fields

        [SerializeField] private bool isLocked = true;

        #endregion


        #region Properties

        public bool IsLocked
        {
            get => isLocked;
            set => isLocked = value;
        }
        
        #endregion
    }
}