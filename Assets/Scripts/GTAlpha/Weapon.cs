using UnityEngine;

namespace GTAlpha
{
    public class Weapon : MonoBehaviour
    {
        public static readonly string[] Keys = 
        {
            "Testing Sword"
        };

        public static readonly string[] Forms =
        {
            "Testing Form"
        };

        public static int GetWeaponKeyIndex(string key)
        {
            for (int i = 0; i < Keys.Length; i++)
            {
                if (Keys[i].Equals(key))
                {
                    return i;
                }
            }
            
            Debug.LogError($"Not Exist Weapon Key : {key}");
            return -1;
        }

        public static int GetWeaponFormIndex(string weaponForm)
        {
            for (int i = 0; i < Forms.Length; i++)
            {
                if (Forms[i].Equals(weaponForm))
                {
                    return i;
                }
            }
            
            Debug.LogError($"Not Exist Weapon Form : {weaponForm}");
            return -1;
        }
    }
}