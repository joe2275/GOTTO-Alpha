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
                SerializedProperty attackMotionArrayProp =
                    playerAttackFormProp.FindPropertyRelative("attackMotionArray");

                EditorGUILayout.LabelField($"{i + 1}. {Weapon.Forms[i]}");

                int attackMotionRemoveIndex = -1;
                for (int j = 0; j < attackMotionArrayProp.arraySize; j++)
                {
                    SerializedProperty attackMotionProp = attackMotionArrayProp.GetArrayElementAtIndex(j);

                    SerializedProperty keyProp = attackMotionProp.FindPropertyRelative("key");
                    SerializedProperty canBeStartMotionProp = attackMotionProp.FindPropertyRelative("canBeStartMotion");
                    SerializedProperty connectionKeyArrayInProp =
                        attackMotionProp.FindPropertyRelative("connectionKeyArrayIn");
                    SerializedProperty connectionKeyArrayOutProp =
                        attackMotionProp.FindPropertyRelative("connectionKeyArrayOut");
                    SerializedProperty isNextAttackTimeFixedProp = attackMotionProp.FindPropertyRelative(
                        "isNextAttackTimeFixed");
                    SerializedProperty nextAttackTimeProp = attackMotionProp.FindPropertyRelative("nextAttackTime");
                    SerializedProperty minNextAttackTimeProp =
                        attackMotionProp.FindPropertyRelative("minNextAttackTime");
                    SerializedProperty maxNextAttackTimeProp =
                        attackMotionProp.FindPropertyRelative("maxNextAttackTime");

                    canBeStartMotionProp.boolValue =
                        EditorGUILayout.Toggle("Can Be Start Motion", canBeStartMotionProp.boolValue);
                    isNextAttackTimeFixedProp.boolValue = EditorGUILayout.Toggle("Fixed Next Attack Time",
                        isNextAttackTimeFixedProp.boolValue);
                    
                    EditorGUILayout.BeginHorizontal();
                    keyProp.intValue = Mathf.Max(EditorGUILayout.IntField("Key", keyProp.intValue), 0);
                    if (isNextAttackTimeFixedProp.boolValue)
                    {
                        nextAttackTimeProp.floatValue =
                            Mathf.Max(EditorGUILayout.FloatField("Next Time", nextAttackTimeProp.floatValue), 0.0f);
                    }
                    else
                    {
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.BeginHorizontal();
                        minNextAttackTimeProp.floatValue =
                            Mathf.Max(EditorGUILayout.FloatField("Min Time", minNextAttackTimeProp.floatValue), 0.0f);
                        maxNextAttackTimeProp.floatValue = Mathf.Max(EditorGUILayout.FloatField("Max Time",
                            maxNextAttackTimeProp.floatValue), minNextAttackTimeProp.floatValue);
                    }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.BeginVertical();
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

                    EditorGUILayout.EndVertical();

                    EditorGUILayout.BeginVertical();
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

                    EditorGUILayout.EndVertical();
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.Space();

                    if (GUILayout.Button("Remove Attack Motion"))
                    {
                        attackMotionRemoveIndex = j;
                    }
                }

                if (attackMotionRemoveIndex > -1)
                {
                    attackMotionArrayProp.DeleteArrayElementAtIndex(attackMotionRemoveIndex);
                    attackMotionRemoveIndex = -1;
                }

                EditorGUILayout.Space();
                EditorGUILayout.Space();

                if (GUILayout.Button("Add Attack Motion"))
                {
                    attackMotionArrayProp.InsertArrayElementAtIndex(attackMotionArrayProp.arraySize);
                }
            }

            playerAttackFormArrayProp.arraySize = Weapon.Forms.Length;

            serializedObject.ApplyModifiedProperties();
        }
    }
}