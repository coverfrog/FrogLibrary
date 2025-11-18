using System;
using System.Collections.Generic;
using UnityEngine;

public class GUITester : MonoBehaviour
{
    [Header("# Offset")]
    [SerializeField] private float _offsetLeft = 20.0f;
    [SerializeField] private float _offsetTop = 20.0f;

    [Header("# Space")]
    [SerializeField] private float _space = 10.0f;
    
    [Header("# Button")]
    [SerializeField] private float _width = 150.0f;
    [SerializeField] private float _height = 50.0f;

    [Header("# Font")]
    [SerializeField] private int _fontSize = 25;

    protected void Draw(int i, string txt, Action action)
    {
        var style = new GUIStyle(GUI.skin.button)
        {
            fontSize = _fontSize,
            alignment = TextAnchor.MiddleCenter,
        };
        
        if (!GUI.Button(new Rect(_offsetLeft, _offsetTop + (_space + _height) * i, _width, _height), txt, style))
        {
            return;
        }
        
        action?.Invoke();
    }
}