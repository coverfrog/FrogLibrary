using System.Collections.Generic;
using UnityEngine.InputSystem;

public static class InputUtil
{
    public static List<string> GetActionNameList(InputActionAsset asset, bool addOnString)
    {
        List<string> result = new List<string>();
        
        foreach (var ia in asset.actionMaps)
        {
            foreach (var a in ia.actions)
            {
                string str = addOnString ? "On" : "";
                str += $"{a.name}";
                
                result.Add(str);
            }
        }
        
        return result;
    }
}