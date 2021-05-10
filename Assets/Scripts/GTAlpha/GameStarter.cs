using System;
using UnityEngine;

namespace GTAlpha
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private GlobalScriptableObject[] scriptableObjects;

        private void Awake()
        {
            for (int i = 0; i < scriptableObjects.Length; i++)
            {
                scriptableObjects[i].Load();
            }
        }
    }
}