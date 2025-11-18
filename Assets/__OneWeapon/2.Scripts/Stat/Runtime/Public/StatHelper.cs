using System;
using System.Collections.Generic;
using UnityEngine;

public class StatHelper : MonoBehaviour
{
    [Header("# Init")]
    [SerializeField] private List<StatAsset> _assetList = new();
    
    [Header("# Runtime")]
    [SerializeField] private List<StatController> _controllerList = new();

    private void Awake()
    {
        _assetList.RemoveAll(asset => asset == null);
        
        _controllerList.Clear();
        _assetList.ForEach(asset => _controllerList.Add(new StatController(asset)));
    }

    private void OnEnable()
    {
       
    }
}