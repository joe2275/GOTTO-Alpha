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

        [SerializeField] private string name;

        #endregion


        #region Properties

        public string Name => name;

        #endregion

        public AccessoryData(string name)
        {
            this.name = name;
        }
    }
}