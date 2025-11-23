using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StatHelper : MonoBehaviour
{
    [SerializeField] private List<StatAsset> _assetList = new();
    [SerializeField] private List<StatController> _controllerList = new();

    private void Awake()
    {
        _assetList.RemoveAll(asset => asset == null);
        
        _controllerList.Clear();
        _assetList.ForEach(asset => _controllerList.Add(new StatController(asset)));
    }

    public bool GetValue(string codename, out float value)
    {
        var ctrl = _controllerList.FirstOrDefault(x => x.Codename == codename);

        if (ctrl != null) value = ctrl.Value;
        else value = -1;
        
        return ctrl != null;
    }
}