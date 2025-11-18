using System;
using UnityEngine;

[Serializable]
public class StatController
{
    [SerializeField, ReadOnly] private string _codeName;

    [SerializeField] private StatContext _context;
    
    [SerializeField, ReadOnly] private StatAsset _asset;
    
    public StatController(StatAsset asset)
    {
        _codeName = asset.CodeName;
        _asset = asset;

        _context = new StatContext(this, asset);
    }
}