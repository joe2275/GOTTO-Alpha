using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// 플레이어가 소유하는 모든 정보를 저장하는 InventoryData 클래스
    /// </summary>
    [Serializable]
    public class InventoryData
    {
        private static InventoryData _current;

        /// <summary>
        /// 현재 사용되고 있는 InventoryData 객체
        /// </summary>
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

        /// <summary>
        /// 장착하고 있는 무기의 Key를 담아두고 있는 배열
        /// </summary>
        [SerializeField] private string[] weaponSlotArray = Enumerable.Repeat("", 3).ToArray();
        /// <summary>
        /// 기존에 있던 무기가 사라지거나 무기가 새로 추가되어 무기의 인덱스가 섞이는 경우, 무기의 변화하는 수치들을 유지하기 위해 선언된 이전에 있었던 각 무기의 Key 배열 (인덱스 값 관리)
        /// 무기가 사라지거나 추가되어 인덱스가 변화되었을 때, 무기의 변화되는 정보를 유지하면서 새로운 인덱스로 갱신
        /// </summary>
        [SerializeField] private string[] weaponIndexArray;
        /// <summary>
        /// 무기의 변화하는 정보를 저장하기 위해 선언된 WeaponData 배열
        /// </summary>
        [SerializeField] private WeaponData[] weaponDataArray;
        
        /// <summary>
        /// 유저가 퀵슬롯에 설정해놓은 정보를 유지하기 위해 선언된 소비 아이템 Key 배열
        /// </summary>
        [SerializeField] private string[] consumptionSlotArray = Enumerable.Repeat("", 6).ToArray();
        /// <summary>
        /// 기존에 있던 소비 아이템이 사라지거나 소비 아이템이 새로 추가되어 소비 아이템의 인덱스가 섞이는 경우, 소비 아이템의 소유 개수를 유지하기 위해 이전에 있었던 각 소비 아이템의 Key 배열 (인덱스 값 관리)
        /// 소비 아이템이 사라지거나 추가되어 인덱스가 변화되었을 때, 소비 아이템의 개수 정보를 유지하면서 새로운 인덱스로 갱신
        /// </summary>
        [SerializeField] private string[] consumptionIndexArray;
        /// <summary>
        /// 소비 아이템의 소유 개수를 저장하기 위해 선언된 정수형 배열
        /// </summary>
        [SerializeField] private int[] consumptionCountArray;
        
        /// <summary>
        /// Accessory Slot은 메커니즘이 복잡한 관계로 추후에 구성
        /// </summary>
        [SerializeField] private List<AccessoryData> accessoryDataList = new List<AccessoryData>();

        #endregion

        #region Properties

        /// <summary>
        /// 무기를 장착할 수 있는 슬롯의 개수
        /// </summary>
        public static int WeaponSlotCount => _current.weaponSlotArray.Length;
        /// <summary>
        /// 게임에 존재하는 무기의 총 개수
        /// </summary>
        public static int WeaponCount => _current.weaponIndexArray.Length;

        /// <summary>
        /// 소비 아이템 퀵슬롯 개수
        /// </summary>
        public static int ConsumptionSlotCount => _current.consumptionSlotArray.Length;
        /// <summary>
        /// 게임에 존재하는 소비 아이템의 총 개수
        /// </summary>
        public static int ConsumptionCount => _current.consumptionIndexArray.Length;

        /// <summary>
        /// 게임에 존재하는 부속품의 총 개수
        /// </summary>
        public static int AccessoryCount => _current.accessoryDataList.Count;

        #endregion

        #region Weapon Functions

        /// <summary>
        /// 전달된 무기의 Key 값에 대응하는 무기의 WeaponData (유동적 무기 정보) 객체를 반환하는 정적 함수
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 전달된 무기의 인덱스 값에 대응하는 무기의 WeaponData (유동적 무기 정보) 객체를 반환하는 정적 함수
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static WeaponData GetWeaponData(int index)
        {
            if (index < 0 || index >= WeaponCount)
            {
                Debug.LogErrorFormat("Out of Weapon Data Index - Count : {0}, Index : {1}", WeaponCount, index);
                return null;
            }
            
            return _current.weaponDataArray[index];
        }

        /// <summary>
        /// 전달된 무기 장착 슬롯 인덱스에 장착된 무기의 Key 값을 반환하는 정적 함수
        /// </summary>
        /// <param name="equipIndex"></param>
        /// <returns></returns>
        public static string SeekWeaponSlot(int equipIndex)
        {
            if (equipIndex < 0 || equipIndex >= WeaponSlotCount)
            {
                Debug.LogErrorFormat("Out of Weapon Equip Slot Index - Count : {0}, Index : {1}", WeaponSlotCount, equipIndex);
                return null;
            }

            return _current.weaponSlotArray[equipIndex];
        }

        /// <summary>
        /// 전달된 무기 인덱스 값에 존재하는 무기를 장착 슬롯 인덱스에 장착하는 정적 함수
        /// </summary>
        /// <param name="dataIndex"></param>
        /// <param name="equipIndex"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 전달된 무기의 Key 값에 해당하는 무기를 equipIndex 슬롯에 장착하는 정적 함수
        /// </summary>
        /// <param name="key"></param>
        /// <param name="equipIndex"></param>
        /// <returns></returns>
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

        /// <summary>
        /// equipIndex에 장착된 무기를 해제하는 정적 함수
        /// </summary>
        /// <param name="equipIndex"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 전달된 Key 값에 해당하는 무기를 장착 슬롯에서 해제하는 정적 함수
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 전달된 Key 값에 해당하는 소비 아이템을 소지하고 있는 개수를 반환하는 정적 함수
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 전달된 인덱스 값에 해당하는 소비 아이템을 소지하고 있는 개수를 반환하는 정적 함수
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static int GetConsumptionCount(int index)
        {
            if (index < 0 || index >= ConsumptionCount)
            {
                Debug.LogErrorFormat("Out of Consumption Index - Count : {0}, Index : {1}", ConsumptionCount, index);
                return -1;
            }
            
            return _current.consumptionCountArray[index];
        }

        /// <summary>
        /// 전달된 Key 값에 해당하는 소비 아이템의 소지 개수를 count 로 설정하는 정적 함수
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// 전달된 인덱스에 해당하는 소비 아이템의 소지 개수를 count로 설정하는 정적 함수
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// dataIndex에 해당하는 소비 아이템을 퀵슬롯 equipIndex 위치에 장착하는 정적 함수
        /// </summary>
        /// <param name="dataIndex"></param>
        /// <param name="equipIndex"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 전달된 Key 값에 해당하는 소비 아이템을 퀵슬롯의 equipIndex 위치에 장착하는 정적 함수
        /// </summary>
        /// <param name="key"></param>
        /// <param name="equipIndex"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 소비 아이템 퀵 슬롯의 equipIndex 위치에 등록된 소비 아이템을 등록 해제하는 정적 함수
        /// </summary>
        /// <param name="equipIndex"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 전달된 Key 값에 해당하는 소비 아이템을 퀵 슬롯에서 등록 해제하는 정적 함수
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 저장된 Inventory 관련 정보들을 로드하는 함수
        /// 만약 무기의 Index, 소비 아이템의 Index, 부속품의 Index가 섞이는 경우 변경된 배열의 인덱스로 갱신한다. 
        /// </summary>
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