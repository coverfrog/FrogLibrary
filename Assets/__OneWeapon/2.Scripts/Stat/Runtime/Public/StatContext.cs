using System;
using UnityEngine;

[Serializable]
public class StatContext
{
    [SerializeField, ReadOnly] private float _value;

    [SerializeField, ReadOnly] private bool _isPercentType;

    public StatContext(StatController controller, StatAsset asset)
    {
        _value = asset.InitValue;
        _isPercentType = asset.IsPercentType;
    }
}