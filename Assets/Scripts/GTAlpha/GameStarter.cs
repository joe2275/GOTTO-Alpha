using System;
using Manager;
using UnityEngine;

namespace GTAlpha
{
    /// <summary>
    /// 게임 시작시에 가장 먼저 호출되는 클래스로 초기화를 위한 각종 객체가 등록된다. 
    /// </summary>
    public class GameStarter : MonoBehaviour
    {
        private static bool _isStarted;
        
        [SerializeField] private GlobalScriptableObject[] globalScriptableObjects;

        private void Awake()
        {
            if (_isStarted)
            {
                Destroy(gameObject);
                return;
            }

            _isStarted = true;
            DontDestroyOnLoad(gameObject);

            #region Temporary Region

            SaveData.CurrentKey = 0;

            #endregion
            
            SaveData.Load();

            ScriptManager.Load();
            StateManager.Load();
            SettingsManager.Load();
            
            for (int i = 0; i < globalScriptableObjects.Length; i++)
            {
                globalScriptableObjects[i].Load();
            }
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}