using UnityEditor;
using UnityEngine;

namespace GTAlpha.Editor
{
    [CustomEditor(typeof(PlayerAttackSystem))]
    public class PlayerAttackSystemEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            SerializedProperty playerAttackFormArrayProp = serializedObject.FindProperty("playerAttackFormArray");

            for (int i = 0; i < playerAttackFormArrayProp.arraySize; i++)
            {
                SerializedProperty playerAttackFormProp = playerAttackFormArrayProp.GetArrayElementAtIndex(i);
                SerializedProperty singleTargetMotionArrayProp = playerAttackFormProp.FindPropertyRelative("singleTargetMotionArray");
                SerializedProperty multipleTargetMotionArrayProp =
                    playerAttackFormProp.FindPropertyRelative("multipleTargetMotionArray");

                EditorGUILayout.LabelField($"{i+1}. {((WeaponForm)i).ToString()}");

                int attackMotionRemoveIndex = -1;
                EditorGUILayout.LabelField("Single Target Attack");
                for (int j = 0; j < singleTargetMotionArrayProp.arraySize; j++)
                {
                    SerializedProperty singleTargetMotionProp = singleTargetMotionArrayProp.GetArrayElementAtIndex(j);

                    SerializedProperty keyProp = singleTargetMotionProp.FindPropertyRelative("key");
                    SerializedProperty connectionKeyArrayInProp =
                        singleTargetMotionProp.FindPropertyRelative("connectionKeyArrayIn");
                    SerializedProperty connectionKeyArrayOutProp =
                        singleTargetMotionProp.FindPropertyRelative("connectionKeyArrayOut");
                    SerializedProperty fullTimeProp = singleTargetMotionProp.FindPropertyRelative("fullTime");
                    SerializedProperty attackTimeProp = singleTargetMotionProp.FindPropertyRelative("attackTime");

                    EditorGUILayout.BeginHorizontal();
                    keyProp.intValue = Mathf.Max(EditorGUILayout.IntField("Key", keyProp.intValue), 0);
                    if (GUILayout.Button("Remove Attack Motion"))
                    {
                        attackMotionRemoveIndex = j;
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.LabelField("Connection In Keys");

                    int removeIndex = -1;
                    for (int k = 0; k < connectionKeyArrayInProp.arraySize; k++)
                    {
                        SerializedProperty connectionKeyInProp = connectionKeyArrayInProp.GetArrayElementAtIndex(k);

                        EditorGUILayout.BeginHorizontal();
                        connectionKeyInProp.intValue =
                            Mathf.Max(EditorGUILayout.IntField(connectionKeyInProp.intValue), 0);
                        if (GUILayout.Button("Remove"))
                        {
                            removeIndex = k;
                        }
                        EditorGUILayout.EndHorizontal();
                    }

                    if (removeIndex > -1)
                    {
                        connectionKeyArrayInProp.DeleteArrayElementAtIndex(removeIndex);
                        removeIndex = -1;
                    }
                    
                    if (GUILayout.Button("Add Connection In Key"))
                    {
                        connectionKeyArrayInProp.InsertArrayElementAtIndex(connectionKeyArrayInProp.arraySize);
                    }
                    
                    EditorGUILayout.LabelField("Connection Out Keys");
                    
                    for (int k = 0; k < connectionKeyArrayOutProp.arraySize; k++)
                    {
                        SerializedProperty connectionKeyOutProp = connectionKeyArrayOutProp.GetArrayElementAtIndex(k);

                        EditorGUILayout.BeginHorizontal();
                        connectionKeyOutProp.intValue =
                            Mathf.Max(EditorGUILayout.IntField(connectionKeyOutProp.intValue), 0);
                        if (GUILayout.Button("Remove"))
                        {
                            removeIndex = k;
                        }
                        EditorGUILayout.EndHorizontal();
                    }

                    if (removeIndex > -1)
                    {
                        connectionKeyArrayOutProp.DeleteArrayElementAtIndex(removeIndex);
                    }
                    
                    if (GUILayout.Button("Add Connection Out Key"))
                    {
                        connectionKeyArrayOutProp.InsertArrayElementAtIndex(connectionKeyArrayOutProp.arraySize);
                    }

                    EditorGUILayout.Space();

                    fullTimeProp.floatValue =
                        Mathf.Max(EditorGUILayout.FloatField("Full Motion Time", fullTimeProp.floatValue), 0.0f);

                    attackTimeProp.floatValue =
                        Mathf.Clamp(EditorGUILayout.FloatField("Motion's Attack Time", attackTimeProp.floatValue), 0.0f,
                            fullTimeProp.floatValue);
                    
                    EditorGUILayout.Space();
                }

                if (attackMotionRemoveIndex > -1)
                {
                    singleTargetMotionArrayProp.DeleteArrayElementAtIndex(attackMotionRemoveIndex);
                    attackMotionRemoveIndex = -1;
                }
                
                EditorGUILayout.Space();
                EditorGUILayout.Space();

                if (GUILayout.Button("Add Single Target Attack Motion"))
                {
                    singleTargetMotionArrayProp.InsertArrayElementAtIndex(singleTargetMotionArrayProp.arraySize);
                }
                
                EditorGUILayout.Space();
                EditorGUILayout.Space();
                
                EditorGUILayout.LabelField("Multiple Target Attack");
                for (int j = 0; j < multipleTargetMotionArrayProp.arraySize; j++)
                {
                    SerializedProperty multipleTargetMotionProp = multipleTargetMotionArrayProp.GetArrayElementAtIndex(j);

                    SerializedProperty keyProp = multipleTargetMotionProp.FindPropertyRelative("key");
                    SerializedProperty connectionKeyArrayInProp =
                        multipleTargetMotionProp.FindPropertyRelative("connectionKeyArrayIn");
                    SerializedProperty connectionKeyArrayOutProp =
                        multipleTargetMotionProp.FindPropertyRelative("connectionKeyArrayOut");
                    SerializedProperty fullTimeProp = multipleTargetMotionProp.FindPropertyRelative("fullTime");
                    SerializedProperty attackTimeProp = multipleTargetMotionProp.FindPropertyRelative("attackTime");

                    EditorGUILayout.BeginHorizontal();
                    keyProp.intValue = Mathf.Max(EditorGUILayout.IntField("Key", keyProp.intValue), 0);
                    if (GUILayout.Button("Remove Attack Motion"))
                    {
                        attackMotionRemoveIndex = j;
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.LabelField("Connection In Keys");

                    int removeIndex = -1;
                    for (int k = 0; k < connectionKeyArrayInProp.arraySize; k++)
                    {
                        SerializedProperty connectionKeyInProp = connectionKeyArrayInProp.GetArrayElementAtIndex(k);

                        EditorGUILayout.BeginHorizontal();
                        connectionKeyInProp.intValue =
                            Mathf.Max(EditorGUILayout.IntField(connectionKeyInProp.intValue), 0);
                        if (GUILayout.Button("Remove"))
                        {
                            removeIndex = k;
                        }
                        EditorGUILayout.EndHorizontal();
                    }

                    if (removeIndex > -1)
                    {
                        connectionKeyArrayInProp.DeleteArrayElementAtIndex(removeIndex);
                        removeIndex = -1;
                    }
                    
                    if (GUILayout.Button("Add Connection In Key"))
                    {
                        connectionKeyArrayInProp.InsertArrayElementAtIndex(connectionKeyArrayInProp.arraySize);
                    }
                    
                    EditorGUILayout.LabelField("Connection Out Keys");
                    
                    for (int k = 0; k < connectionKeyArrayOutProp.arraySize; k++)
                    {
                        SerializedProperty connectionKeyOutProp = connectionKeyArrayOutProp.GetArrayElementAtIndex(k);

                        EditorGUILayout.BeginHorizontal();
                        connectionKeyOutProp.intValue =
                            Mathf.Max(EditorGUILayout.IntField(connectionKeyOutProp.intValue), 0);
                        if (GUILayout.Button("Remove"))
                        {
                            removeIndex = k;
                        }
                        EditorGUILayout.EndHorizontal();
                    }

                    if (removeIndex > -1)
                    {
                        connectionKeyArrayOutProp.DeleteArrayElementAtIndex(removeIndex);
                    }
                    
                    if (GUILayout.Button("Add Connection Out Key"))
                    {
                        connectionKeyArrayOutProp.InsertArrayElementAtIndex(connectionKeyArrayOutProp.arraySize);
                    }
                    
                    EditorGUILayout.Space();
                    fullTimeProp.floatValue =
                        Mathf.Max(EditorGUILayout.FloatField("Full Motion Time", fullTimeProp.floatValue), 0.0f);

                    attackTimeProp.floatValue =
                        Mathf.Clamp(EditorGUILayout.FloatField("Motion's Attack Time", attackTimeProp.floatValue), 0.0f,
                            fullTimeProp.floatValue);
                    EditorGUILayout.Space();
                }

                if (attackMotionRemoveIndex > -1)
                {
                    multipleTargetMotionArrayProp.DeleteArrayElementAtIndex(attackMotionRemoveIndex);
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();
                
                if (GUILayout.Button("Add Multiple Target Attack Motion"))
                {
                    multipleTargetMotionArrayProp.InsertArrayElementAtIndex(multipleTargetMotionArrayProp.arraySize);
                }
            }

            playerAttackFormArrayProp.arraySize = (int) WeaponForm.Count;

            serializedObject.ApplyModifiedProperties();
        }
    }
}