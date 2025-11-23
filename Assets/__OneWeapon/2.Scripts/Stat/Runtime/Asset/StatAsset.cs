using UnityEngine;

[CreateAssetMenu]
public class StatAsset : IdentifiedObject
{
    [Header("# Stat")]
    [SerializeField] private float _initValue;
    
    public float InitValue => _initValue;
    
    [SerializeField] private bool _isPercentType;
    
    public bool IsPercentType => _isPercentType;
}