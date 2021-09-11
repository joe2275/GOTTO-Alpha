using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// WeaponInfo 객체들을 일괄적으로 관리하는 클래스
    /// </summary>
    [CreateAssetMenu(fileName = "New Weapon Info Table", menuName = "Table/Weapon Info", order = 0)]
    public class WeaponInfoTable : GlobalScriptableObject
    {
        private static WeaponInfoTable _main;

        #region Serialized Fields
        
        [SerializeField] private WeaponInfo[] weaponInfoArray = new WeaponInfo[Weapon.Keys.Length];

        #endregion

        /// <summary>
        /// 전달된 인덱스에 해당하는 무기의 WeaponInfo 객체를 반환하는 정적 함수
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static WeaponInfo GetInformation(int index)
        {
            if (index < 0 || index >= Weapon.Keys.Length)
            {
                Debug.LogErrorFormat("Out of Weapon Information Array - Length : {0}, Index : {1}", Weapon.Keys.Length, index);
                return null;
            }

            return _main.weaponInfoArray[index];
        }

        /// <summary>
        /// 전달된 Key 값에 해당하는 무기의 WeaponInfo 객체를 반환하는 정적 함수
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static WeaponInfo GetInformation(string key)
        {
            return GetInformation(Weapon.GetWeaponKeyIndex(key));
        }

        public override void Load()
        {
            _main = this;
        }
    }
}