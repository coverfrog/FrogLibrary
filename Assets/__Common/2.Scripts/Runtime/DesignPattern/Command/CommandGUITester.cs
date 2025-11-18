using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandGUITester : GUITester
{
    [Header("# Command")]
    [SerializeField] private GameObject _obj;

    [Header("# Method")]
    [SerializeField] private string _methodName = "Handle";
    [SerializeField] private int _dataIndex;
    [SerializeField] private List<string> dataList = new()
    {
        "Slash",
        "Dash",
        "Parrying"
    };
    
    private void OnGUI()
    {
        for (int i = 0; i < dataList.Count; i++)
        {
            int regionI = i;
            
            SimpleCommand cmd = new SimpleCommand(() =>
            {
                _obj.SendMessage(_methodName, dataList[regionI]);
            });
            
            Draw(i, dataList[i], () =>
            {
                CommandManager.Instance.Execute(cmd);
            });
        }
        
        Draw(dataList.Count + 1, "Record", () =>
        {
            CommandManager.Instance.Record();
        });
        
        Draw(dataList.Count + 2, "Record End", () =>
        {
            CommandManager.Instance.RecordEnd();
        });
        
        Draw(dataList.Count + 3, "Replay", () =>
        {
            CommandManager.Instance.Replay();
        });
        
    }
}