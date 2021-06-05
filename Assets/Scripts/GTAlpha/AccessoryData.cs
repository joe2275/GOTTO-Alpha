using System;
using UnityEngine;

namespace GTAlpha
{
    /*
     * AccessoryData
     * Accessory 관련 정보 중 서로 다르고 저장되어야 하는 정보를 저장한다. 
     */
    [Serializable]
    public class AccessoryData
    {
        #region Serialized Fields

        [SerializeField] private string key;

        #endregion


        #region Properties

        public string Key => key;

        #endregion

        public AccessoryData(string key)
        {
            this.key = key;
        }
    }
}