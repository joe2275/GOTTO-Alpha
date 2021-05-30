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