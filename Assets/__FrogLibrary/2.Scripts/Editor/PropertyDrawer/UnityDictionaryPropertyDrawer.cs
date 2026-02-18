using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace FrogLibrary.Editor
{
    [CustomPropertyDrawer(typeof(UnityDictionary<,>), true)]
    public class UnityDictionaryDrawer : PropertyDrawer
    {
        private const float Spacing = 2f;
        private const float HeaderHeight = 22f;
        private const float KeyValueSplit = 0.4f; // 키와 값의 너비 비율 (40% : 60%)

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty keysProp = property.FindPropertyRelative("m_keys");
            SerializedProperty valuesProp = property.FindPropertyRelative("m_values");

            // 1. 전체 배경 박스 (Header)
            Rect headerRect = new Rect(position.x, position.y, position.width, HeaderHeight);
            GUI.Box(headerRect, "", (GUIStyle)"RL Header");
            
            // 폴드아웃 화살표 및 타이틀
            Rect foldoutRect = new Rect(position.x + 15, position.y + 2, position.width - 100, HeaderHeight);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, label, true);

            // 사이즈 필드 (우측 상단 배치)
            Rect sizeRect = new Rect(position.x + position.width - 80, position.y + 2, 75, 16);
            int newSize = EditorGUI.IntField(sizeRect, keysProp.arraySize);
            if (newSize != keysProp.arraySize)
            {
                keysProp.arraySize = newSize;
                valuesProp.arraySize = newSize;
            }

            if (property.isExpanded)
            {
                // 2. 바디 배경 박스
                Rect bodyRect = new Rect(position.x, position.y + HeaderHeight, position.width, position.height - HeaderHeight);
                GUI.Box(bodyRect, "", (GUIStyle)"RL Background");

                float currentY = position.y + HeaderHeight + Spacing;
                HashSet<object> keyCheck = new HashSet<object>();

                for (int i = 0; i < keysProp.arraySize; i++)
                {
                    SerializedProperty keyProp = keysProp.GetArrayElementAtIndex(i);
                    SerializedProperty valProp = valuesProp.GetArrayElementAtIndex(i);

                    float rowHeight = Mathf.Max(EditorGUI.GetPropertyHeight(keyProp), EditorGUI.GetPropertyHeight(valProp));
                    Rect rowRect = new Rect(position.x + 5, currentY, position.width - 10, rowHeight);

                    // --- 그리기 시작 ---
                    float keyWidth = rowRect.width * KeyValueSplit;
                    float valWidth = rowRect.width - keyWidth - 25;

                    Rect kRect = new Rect(rowRect.x, rowRect.y, keyWidth, rowHeight);
                    Rect vRect = new Rect(rowRect.x + keyWidth + 5, rowRect.y, valWidth, rowHeight);
                    Rect btnRect = new Rect(rowRect.x + rowRect.width - 18, rowRect.y, 18, 18);

                    // 중복 키 하이라이트 (에러 표시)
                    CheckDuplicate(keyProp, keyCheck, kRect);

                    // 필드 렌더링
                    EditorGUI.PropertyField(kRect, keyProp, GUIContent.none);
                    EditorGUI.PropertyField(vRect, valProp, GUIContent.none);

                    // 개별 삭제 버튼 (-)
                    if (GUI.Button(btnRect, "x", (GUIStyle)"OL Minus"))
                    {
                        keysProp.DeleteArrayElementAtIndex(i);
                        valuesProp.DeleteArrayElementAtIndex(i);
                        break;
                    }

                    currentY += rowHeight + Spacing + 2;
                }

                // 하단 추가 버튼 (+)
                Rect addBtnRect = new Rect(position.x + position.width - 30, currentY, 25, 15);
                if (GUI.Button(addBtnRect, "+", (GUIStyle)"RL FooterButton"))
                {
                    keysProp.arraySize++;
                    valuesProp.arraySize++;
                }
            }

            EditorGUI.EndProperty();
        }

        private void CheckDuplicate(SerializedProperty keyProp, HashSet<object> keyCheck, Rect rect)
        {
            object keyVal = GetPropertyValue(keyProp);
            if (keyVal != null)
            {
                if (keyCheck.Contains(keyVal))
                {
                    EditorGUI.DrawRect(rect, new Color(1, 0, 0, 0.2f)); // 중복 시 붉은 배경
                }
                else
                {
                    keyCheck.Add(keyVal);
                }
            }
        }

        private object GetPropertyValue(SerializedProperty prop)
        {
            return prop.propertyType switch
            {
                SerializedPropertyType.Integer => prop.intValue,
                SerializedPropertyType.String => prop.stringValue,
                SerializedPropertyType.ObjectReference => prop.objectReferenceValue,
                SerializedPropertyType.Enum => prop.enumValueIndex,
                _ => null
            };
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!property.isExpanded) return HeaderHeight;

            SerializedProperty keysProp = property.FindPropertyRelative("m_keys");
            SerializedProperty valuesProp = property.FindPropertyRelative("m_values");

            float height = HeaderHeight + 25; // Header + Footer space

            for (int i = 0; i < keysProp.arraySize; i++)
            {
                height += Mathf.Max(EditorGUI.GetPropertyHeight(keysProp.GetArrayElementAtIndex(i)), 
                                   EditorGUI.GetPropertyHeight(valuesProp.GetArrayElementAtIndex(i))) + Spacing + 2;
            }

            return height;
        }
    }
}