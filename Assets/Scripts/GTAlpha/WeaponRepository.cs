using UnityEngine;

namespace GTAlpha
{
    [CreateAssetMenu(fileName = "New Weapon Repository", menuName = "Repository/Weapon", order = 0)]
    public class WeaponRepository : GlobalScriptableObject
    {
        private static WeaponRepository _main;

        #region Serialized Fields

        [SerializeField] private WeaponInfo[] weaponInfoArray = new WeaponInfo[Weapon.Keys.Length];

        #endregion

        public static WeaponInfo GetInformation(int index)
        {
            if (index < 0 || index >= Weapon.Keys.Length)
            {
                Debug.LogErrorFormat("Out of Weapon Information Array - Length : {0}, Index : {1}", Weapon.Keys.Length, index);
                return null;
            }

            return _main.weaponInfoArray[index];
        }

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