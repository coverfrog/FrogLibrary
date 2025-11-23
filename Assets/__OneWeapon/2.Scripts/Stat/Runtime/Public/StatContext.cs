using System;
using UnityEngine;

[Serializable]
public class StatContext
{
    [SerializeField, ReadOnly] private string _codename;

    #region Getter

    public string Codename => _codename;
    
    #endregion
    
    [SerializeField, ReadOnly] private float _value;
    
    #region Getter

    public float Value => _value;

    #endregion
    
    [SerializeField, ReadOnly] private bool _isPercentType;

    // ---------------------------------------------------------------------------------
    
    public StatContext(StatController controller, StatAsset asset)
    {
        _codename = asset.CodeName;
        _value = asset.InitValue;
        
        _isPercentType = asset.IsPercentType;
    }
}