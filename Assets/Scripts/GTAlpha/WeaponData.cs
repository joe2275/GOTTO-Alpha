using System;
using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// Weapon 관련 정보 중 지속적으로 변하고 저장되어야 하는 정보를 저장한다. 
    /// </summary>
    [Serializable]
    public class WeaponData
    {
        #region Serialized Fields

        [SerializeField] private bool isLocked = true;

        #endregion


        #region Properties

        /// <summary>
        /// 해당 무기가 해금되어 있는 지를 반환
        /// </summary>
        public bool IsLocked
        {
            get => isLocked;
            set => isLocked = value;
        }
        
        #endregion
    }
}