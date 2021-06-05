using System;
using Manager;
using UnityEngine;

namespace GTAlpha
{
    public class GameStarter : MonoBehaviour
    {
        private static bool _isStarted;
        
        [SerializeField] private GlobalScriptableObject[] scriptableObjects;

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
            
            for (int i = 0; i < scriptableObjects.Length; i++)
            {
                scriptableObjects[i].Load();
            }
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}