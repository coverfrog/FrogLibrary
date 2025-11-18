#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ReadOnlyAttribute readOnly = (ReadOnlyAttribute)attribute;

        bool previousState = GUI.enabled;

        // "onlyInPlayMode" 옵션이 true면, 플레이 중일 때만 비활성화
        if (readOnly.onlyInPlayMode)
            GUI.enabled = !Application.isPlaying;
        else
            GUI.enabled = false;

        EditorGUI.PropertyField(position, property, label);

        GUI.enabled = previousState;
    }
}
#endif