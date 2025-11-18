using System;
using UnityEngine;

[RequireComponent(typeof(SkillHelper))]
public class SkillGUITester : GUITester
{
    private SkillHelper _helper;

    private void Awake()
    {
        _helper = GetComponent<SkillHelper>();
    }

    private void OnGUI()
    {
        Draw(0, "Slash", () =>
        {
            _helper?.Handle("Slash");
        });
        
        Draw(1, "Parrying", () =>
        {
            _helper?.Handle("Parrying");
        });
        
        Draw(2, "Dash", () =>
        {
            _helper?.Handle("Dash");
        });
    }
}