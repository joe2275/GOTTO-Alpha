﻿using UnityEditor;
using UnityEngine;

namespace GTAlpha.Editor
{
    [CustomEditor(typeof(WeaponRepository))]
    public class WeaponRepositoryEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SerializedProperty weaponInfoArrayProp = serializedObject.FindProperty("weaponInfoArray");

            for (int i = 0; i < weaponInfoArrayProp.arraySize && i < Weapon.Keys.Length; i++)
            {
                SerializedProperty weaponInfoProp = weaponInfoArrayProp.GetArrayElementAtIndex(i);

                weaponInfoProp.objectReferenceValue = EditorGUILayout.ObjectField(
                    new GUIContent($"{i + 1}. {Weapon.Keys[i]}"),
                    weaponInfoProp.objectReferenceValue, typeof(WeaponInfo), false);
            }

            weaponInfoArrayProp.arraySize = Weapon.Keys.Length;

            serializedObject.ApplyModifiedProperties();
        }
    }
}