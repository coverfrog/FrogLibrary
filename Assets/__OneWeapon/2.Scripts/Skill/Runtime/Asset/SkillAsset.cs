using UnityEngine;

[CreateAssetMenu]
public class SkillAsset : IdentifiedObject
{
    [Header("# Target")]
    [SerializeField] private bool _isTarget;

    #region Getter
    
    public bool IsTarget => _isTarget;

    #endregion

    [Header("# Duration")]
    [SerializeField] private float _progressDuration = 0.5f;
    [SerializeField] private float _coolTimeDuration = 1.0f;
    
    #region Getter

    public float ProgressDuration => _progressDuration;
    public float CoolTimeDuration => _coolTimeDuration;

    #endregion
    
    [Header("# Animator")]
    [SerializeField] private bool _isAnimate = true;
    [SerializeField] private string _animatorName;
    
    #region Getter

    public bool IsAnimate => _isAnimate;
    public string AnimatorName => _animatorName;

    #endregion

    [Header("$ Value")]
    [SerializeField] private bool _isPolygonPhysics = true;
    
    #region Getter

    public bool IsPolygonPhysics => _isPolygonPhysics;

    #endregion
}