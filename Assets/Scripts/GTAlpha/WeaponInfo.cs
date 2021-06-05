using Manager;
using UnityEngine;

namespace GTAlpha
{
    /*
     * WeaponInfo
     * Weapon 관련 정보 중, 변하지 않는 정보를 저장한다. 
     */
    [CreateAssetMenu(fileName = "New Weapon Information", menuName = "Information/Weapon", order = 0)]
    public class WeaponInfo : ScriptableObject
    {
        #region Serialized Fields
        
        [SerializeField] private Sprite image;
        [SerializeField] private string nameKey;

        #endregion

        #region Properties

        public Sprite Image => image;

        public string NameKey => nameKey;
        public string Name => ScriptManager.HasKey(nameKey) ? ScriptManager.GetScript(nameKey) : "None";


        #endregion
    }
}