using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GTAlpha
{
    [Serializable]
    public class InventoryData
    {
        private static InventoryData _current;

        public static InventoryData Current
        {
            get => _current;
            set
            {
                _current = value;
                _current.Load();
            }
        }

        #region Serialized Fields

        [SerializeField] private string[] weaponSlotArray = Enumerable.Repeat("", 3).ToArray();
        [SerializeField] private string[] weaponIndexArray;
        [SerializeField] private WeaponData[] weaponDataArray;
        
        [SerializeField] private string[] consumptionSlotArray = Enumerable.Repeat("", 6).ToArray();
        [SerializeField] private string[] consumptionIndexArray;
        [SerializeField] private int[] consumptionCountArray;
        
        // Accessory Slot은 메커니즘이 복잡한 관계로 추후에 구성
        [SerializeField] private List<AccessoryData> accessoryDataList = new List<AccessoryData>();

        #endregion

        #region Properties

        public static int WeaponSlotCount => _current.weaponSlotArray.Length;
        public static int WeaponCount => _current.weaponIndexArray.Length;

        public static int ConsumptionSlotCount => _current.consumptionSlotArray.Length;
        public static int ConsumptionCount => _current.consumptionIndexArray.Length;

        public static int AccessoryCount => _current.accessoryDataList.Count;

        #endregion

        #region Weapon Functions

        public static WeaponData GetWeaponData(string key)
        {
            if (key is null)
            {
                Debug.LogError("Weapon's Name Must Not Be Null!");
                return null;
            }
            
            string[] weaponIndexArray = _current.weaponIndexArray;
            for (int i = 0; i < weaponIndexArray.Length; i++)
            {
                if (weaponIndexArray[i].Equals(key))
                {
                    return _current.weaponDataArray[i];
                }
            }
            
            Debug.LogErrorFormat("Not Exist Weapon's Name! - {0}", key);
            return null;
        }

        public static WeaponData GetWeaponData(int index)
        {
            if (index < 0 || index >= WeaponCount)
            {
                Debug.LogErrorFormat("Out of Weapon Data Index - Count : {0}, Index : {1}", WeaponCount, index);
                return null;
            }
            
            return _current.weaponDataArray[index];
        }

        public static string SeekWeaponSlot(int equipIndex)
        {
            if (equipIndex < 0 || equipIndex >= WeaponSlotCount)
            {
                Debug.LogErrorFormat("Out of Weapon Equip Slot Index - Count : {0}, Index : {1}", WeaponSlotCount, equipIndex);
                return null;
            }

            return _current.weaponSlotArray[equipIndex];
        }

        public static bool EquipWeapon(int dataIndex, int equipIndex)
        {
            if (dataIndex < 0 || dataIndex >= WeaponCount)
            {
                Debug.LogErrorFormat("Out of Weapon Data Index - Count : {0}, Index : {1}", WeaponCount, dataIndex);
                return false;
            }

            if (equipIndex < 0 || equipIndex >= WeaponSlotCount)
            {
                Debug.LogErrorFormat("Out of Weapon Equip Slot Index - Count : {0}, Index : {1}", WeaponSlotCount, equipIndex);
                return false;
            }

            if (_current.weaponDataArray[dataIndex].IsLocked)
            {
                Debug.LogErrorFormat("Locked Weapon tried to equip! - {0}", _current.weaponIndexArray[dataIndex]);
                return false;
            }
            
            string[] weaponSlotArray = _current.weaponSlotArray;
            string weaponKey = _current.weaponIndexArray[dataIndex];
            
            // 장착하려는 무기가 이미 장착된 상태라면, 장착하려는 곳에 있는 무기와 장착되어 있는 무기의 위치를 서로 바꾼다.
            for (int i = 0; i < WeaponSlotCount; i++)
            {
                if (!(weaponSlotArray[i] is null) && weaponKey.Equals(weaponSlotArray[i]))
                {
                    string temp = weaponSlotArray[equipIndex];
                    weaponSlotArray[equipIndex] = weaponKey;
                    weaponSlotArray[i] = temp;
                    return true;
                }
            }

            weaponSlotArray[equipIndex] = weaponKey;
            return true;
        }

        public static bool EquipWeapon(string key, int equipIndex)
        {
            if (equipIndex < 0 || equipIndex >= WeaponSlotCount)
            {
                Debug.LogErrorFormat("Out of Weapon Equip Slot Index - Count : {0}, Index : {1}", WeaponSlotCount, equipIndex);
                return false;
            }

            string[] weaponIndexArray = _current.weaponIndexArray;
            for (int i = 0; i < weaponIndexArray.Length; i++)
            {
                if (weaponIndexArray[i].Equals(key))
                {
                    return EquipWeapon(i, equipIndex);
                }
            }

            Debug.LogErrorFormat("Not Exist Weapon's Name! - {0}", key);
            return false;
        }

        public static bool UnEquipWeapon(int equipIndex)
        {
            if (equipIndex < 0 || equipIndex >= WeaponSlotCount)
            {
                Debug.LogErrorFormat("Out of Weapon Equip Slot Index - Count : {0}, Index : {1}", WeaponSlotCount, equipIndex);
                return false;
            }

            _current.weaponSlotArray[equipIndex] = "";
            return true;
        }

        public static bool UnEquipWeapon(string key)
        {
            string[] weaponSlotArray = _current.weaponSlotArray;

            for (int i = 0; i < weaponSlotArray.Length; i++)
            {
                if (weaponSlotArray[i].Equals(key))
                {
                    weaponSlotArray[i] = "";
                    return true;
                }
            }

            Debug.LogErrorFormat("Not Equipped Weapon! - {0}", key);
            return false;
        }

        #endregion

        #region Consumption Functions

        public static int GetConsumptionCount(string key)
        {
            if (key is null)
            {
                Debug.LogError("Consumption's Name Must Not Be Null!");
                return -1;
            }
            
            string[] consumptionIndexArray = _current.consumptionIndexArray;
            for (int i = 0; i < consumptionIndexArray.Length; i++)
            {
                if (consumptionIndexArray[i].Equals(key))
                {
                    return _current.consumptionCountArray[i];
                }
            }
            
            Debug.LogErrorFormat("Not Exist Consumption's Name! - {0}", key);
            return -1;
        }

        public static int GetConsumptionCount(int index)
        {
            if (index < 0 || index >= ConsumptionCount)
            {
                Debug.LogErrorFormat("Out of Consumption Index - Count : {0}, Index : {1}", ConsumptionCount, index);
                return -1;
            }
            
            return _current.consumptionCountArray[index];
        }

        public static bool SetConsumptionCount(string key, int count)
        {
            if (key is null)
            {
                Debug.LogError("Consumption's Name Must Not Be Null!");
                return false;
            }

            if (count < 0)
            {
                Debug.LogError("Count of Consumption cannot be less than 0!");
                return false;
            }
            
            string[] consumptionIndexArray = _current.consumptionIndexArray;
            for (int i = 0; i < consumptionIndexArray.Length; i++)
            {
                if (consumptionIndexArray[i].Equals(key))
                {
                    _current.consumptionCountArray[i] = count;
                    return true;
                }
            }
            
            Debug.LogErrorFormat("Not Exist Consumption's Name! - {0}", key);
            return false;
        }
        
        public static bool SetConsumptionCount(int index, int count)
        {
            if (index < 0 || index >= ConsumptionCount)
            {
                Debug.LogErrorFormat("Out of Consumption Index - Count : {0}, Index : {1}", ConsumptionCount, index);
                return false;
            }

            if (count < 0)
            {
                Debug.LogError("Count of Consumption cannot be less than 0!");
                return false;
            }
            
            _current.consumptionCountArray[index] = count;
            return true;
        }
        
        public static bool EquipConsumption(int dataIndex, int equipIndex)
        {
            if (dataIndex < 0 || dataIndex >= ConsumptionCount)
            {
                Debug.LogErrorFormat("Out of Consumption Index - Count : {0}, Index : {1}", ConsumptionCount, dataIndex);
                return false;
            }

            if (equipIndex < 0 || equipIndex >= ConsumptionSlotCount)
            {
                Debug.LogErrorFormat("Out of Consumption Slot Index - Count : {0}, Index : {1}", ConsumptionSlotCount, equipIndex);
                return false;
            }

            if (_current.consumptionCountArray[dataIndex] <= 0)
            {
                Debug.LogErrorFormat("Not Exist Consumption! - {0} : {1}", _current.consumptionIndexArray[dataIndex], _current.consumptionCountArray[dataIndex]);
                return false;
            }
            
            string[] consumptionSlotArray = _current.consumptionSlotArray;
            string consumptionName = _current.consumptionIndexArray[dataIndex];
            
            // 장착하려는 소모품이 이미 장착된 상태라면, 장착하려는 곳에 있는 소모품과 장착되어 있는 소모품의 위치를 서로 바꾼다.
            for (int i = 0; i < ConsumptionSlotCount; i++)
            {
                if (!(consumptionSlotArray[i] is null) && consumptionName.Equals(consumptionSlotArray[i]))
                {
                    string temp = consumptionSlotArray[equipIndex];
                    consumptionSlotArray[equipIndex] = consumptionName;
                    consumptionSlotArray[i] = temp;
                    return true;
                }
            }

            consumptionSlotArray[equipIndex] = consumptionName;
            return true;
        }

        public static bool EquipConsumption(string key, int equipIndex)
        {
            if (equipIndex < 0 || equipIndex >= ConsumptionSlotCount)
            {
                Debug.LogErrorFormat("Out of Consumption Slot Index - Count : {0}, Index : {1}", ConsumptionSlotCount, equipIndex);
                return false;
            }
            
            string[] consumptionIndexArray = _current.consumptionIndexArray;
            for (int i = 0; i < consumptionIndexArray.Length; i++)
            {
                if (consumptionIndexArray[i].Equals(key))
                {
                    return EquipConsumption(i, equipIndex);
                }
            }

            Debug.LogErrorFormat("Not Exist Consumption's Name! - {0}", key);
            return false;
        }

        public static bool UnEquipConsumption(int equipIndex)
        {
            if (equipIndex < 0 || equipIndex >= ConsumptionSlotCount)
            {
                Debug.LogErrorFormat("Out of Consumption Slot Index - Count : {0}, Index : {1}", ConsumptionSlotCount, equipIndex);
                return false;
            }

            _current.consumptionSlotArray[equipIndex] = "";
            return true;
        }

        public static bool UnEquipConsumption(string key)
        {
            string[] consumptionSlotArray = _current.consumptionSlotArray;

            for (int i = 0; i < consumptionSlotArray.Length; i++)
            {
                if (consumptionSlotArray[i].Equals(key))
                {
                    consumptionSlotArray[i] = "";
                    return true;
                }
            }

            Debug.LogErrorFormat("Not Equipped Consumption! - {0}", key);
            return false;
        }

        #endregion

        #region Accessory Functions

        

        #endregion

        #region Private Functions

        /*
         * 저장되는 Inventory 관련 정보들을 로드하는 기능
         */
        private void Load()
        {
            #region Weapon 관련 정보 처리

            // 저장된 Weapon 관련 정보가 존재하지 않는 경우
            if (weaponIndexArray is null || weaponDataArray is null)
            {
                weaponIndexArray = Weapon.Keys;
                weaponDataArray = new WeaponData[Weapon.Keys.Length];
                for (int i = 0; i < weaponDataArray.Length; i++)
                {
                    weaponDataArray[i] = new WeaponData();
                }
            }
            // 저장된 Weapon 관련 정보가 존재하는 경우
            else
            {
                // Weapon 관련 정보 갱신
                string[] newWeaponIndexArray = Weapon.Keys;
                WeaponData[] newWeaponDataArray = new WeaponData[Weapon.Keys.Length];

                for (int i = 0; i < newWeaponDataArray.Length; i++)
                {
                    bool isExist = false;
                    for (int j = 0; j < weaponIndexArray.Length; j++)
                    {
                        if (newWeaponIndexArray[i].Equals(weaponIndexArray[j]))
                        {
                            isExist = true;
                            newWeaponDataArray[i] = weaponDataArray[j];
                            break;
                        }
                    }

                    if (!isExist)
                    {
                        newWeaponDataArray[i] = new WeaponData();
                    }
                }

                weaponIndexArray = newWeaponIndexArray;
                weaponDataArray = newWeaponDataArray;

                // Weapon Slot 정보 갱신
                for (int i = 0; i < weaponSlotArray.Length; i++)
                {
                    if (weaponSlotArray[i].Equals(""))
                    {
                        continue;
                    }
                    
                    bool isExist = false;
                    for (int j = 0; j < weaponIndexArray.Length; j++)
                    {
                        if (weaponSlotArray[i].Equals(weaponIndexArray[j]))
                        {
                            isExist = true;
                            break;
                        }
                    }

                    if (!isExist)
                    {
                        weaponSlotArray[i] = "";
                    }
                }
            }
            #endregion

            #region Consumption 관련 정보 처리

            if (consumptionIndexArray is null || consumptionCountArray is null)
            {
                consumptionIndexArray = Consumption.Keys;
                consumptionCountArray = new int[Consumption.Keys.Length];
            }
            else
            {
                string[] newConsumptionIndexArray = Consumption.Keys;
                int[] newConsumptionCountArray = new int[Consumption.Keys.Length];

                for (int i = 0; i < newConsumptionCountArray.Length; i++)
                {
                    for (int j = 0; j < consumptionCountArray.Length; j++)
                    {
                        if (newConsumptionIndexArray[i].Equals(consumptionIndexArray[j]))
                        {
                            newConsumptionCountArray[i] = consumptionCountArray[j];
                            break;
                        }
                    }
                }

                consumptionIndexArray = newConsumptionIndexArray;
                consumptionCountArray = newConsumptionCountArray;

                for (int i = 0; i < consumptionSlotArray.Length; i++)
                {
                    bool isExist = false;
                    for (int j = 0; j < consumptionIndexArray.Length; j++)
                    {
                        if (consumptionSlotArray[i].Equals(consumptionIndexArray[j]))
                        {
                            isExist = true;
                            break;
                        }
                    }

                    if (!isExist)
                    {
                        consumptionSlotArray[i] = "";
                    }
                }
            }
            #endregion

            #region Accessory 관련 정보 처리

            string[] accessoryKeys = Accessory.Keys;
            for (int i = 0; i < accessoryDataList.Count; i++)
            {
                bool isExist = false;
                
                AccessoryData accessoryData = accessoryDataList[i];
                
                for (int j = 0; j < accessoryKeys.Length; j++)
                {
                    if (accessoryData.Key.Equals(accessoryKeys[j]))
                    {
                        isExist = true;
                        break;
                    }
                }

                if (!isExist)
                {
                    accessoryDataList.RemoveAt(i--);
                }
            }

            #endregion
        }

        #endregion
    }
}