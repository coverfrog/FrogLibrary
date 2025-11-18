using UnityEngine;

[CreateAssetMenu]
public class SkillAsset : IdentifiedObject
{
    [Header("# Target")]
    [SerializeField] private bool _isTarget;

    public bool IsTarget => _isTarget;

    [Header("# Duration")]
    [SerializeField] private float _progressDuration = 0.5f;
    [SerializeField] private float _coolTimeDuration = 1.0f;
    
    public float ProgressDuration => _progressDuration;
    public float CoolTimeDuration => _coolTimeDuration;
    
    [Header("# Animator")]
    [SerializeField] private bool _isAnimate = true;
    [SerializeField] private string _animatorName;
    
    public bool IsAnimate => _isAnimate;
    public string AnimatorName => _animatorName;

    [Header("$ Value")]
    [SerializeField] private bool _isPolygonPhysics = true;
    
    public bool IsPolygonPhysics => _isPolygonPhysics;
}