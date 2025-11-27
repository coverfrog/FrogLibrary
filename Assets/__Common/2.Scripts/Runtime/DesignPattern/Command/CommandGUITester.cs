using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrogLibrary
{
    public class CommandGUITester : GUITester
    {
        private void OnGUI()
        {
            Draw(0, "Record", () =>
            {
                CommandManager.Instance.Record();
            });
        
            Draw(1, "Record End", () =>
            {
                CommandManager.Instance.RecordEnd();
            });
        
            Draw(2, "Replay", () =>
            {
                CommandManager.Instance.Replay();
            });
        }
    }
}
