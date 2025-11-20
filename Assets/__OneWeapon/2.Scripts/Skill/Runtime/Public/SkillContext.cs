using System;
using UnityEngine;

[Serializable]
public class SkillContext : SimpleStateContext<SkillController>, IContextApplyAsset<SkillAsset>
{
    [Header("# CodeName")]
    [SerializeField, ReadOnly] private string _codeName;

    #region Getter

    public string CodeName => _codeName;

    #endregion
    
    #region Setter

    public void SetCodeName(SkillController _, string codeName) => _codeName = codeName;

    #endregion
    
    [Header("# Type")]
    [SerializeField, ReadOnly] private SkillStateType _stateType;

    #region Getter

    public SkillStateType StateType => _stateType;

    #endregion
    
    #region Setter

    public void SetStateType(SkillController _, SkillStateType stateType) => _stateType = stateType;

    #endregion

    [Header("# Progress")]
    [SerializeField, ReadOnly] private float _progressTime;
    [SerializeField, ReadOnly] private float _progressDuration;

    #region Getter

    public float ProgressTime => _progressTime;
    public float ProgressDuration => _progressDuration;

    #endregion
    
    #region Setter

    public void SetProgressTime(SkillStateProgress _,  float progressTime) => _progressTime = progressTime;
    
    public void SetProgressDuration(SkillStateProgress _, float progressDuration) => _progressDuration = progressDuration;

    #endregion
    
    [Header("# CoolTime")]
    [SerializeField, ReadOnly] private float _coolTime;
    [SerializeField, ReadOnly] private float _coolTimeDuration;

    #region Getter

    public float CoolTime => _coolTime;
    
    public float CoolTimeDuration => _coolTimeDuration;
    
    #endregion

    #region Setter

    public void SetCoolTime(SkillStateCoolTime _, float coolTime) => _coolTime = coolTime;
    
    public void SetCoolTimeDuration(SkillStateCoolTime _, float coolDuration) => _coolTimeDuration = coolDuration;


    #endregion
    
    // ---------------------------------------------------------------------------------
    
    public SkillContext(SkillController controller) : base(controller)
    {
       
    }
    
    // ---------------------------------------------------------------------------------

    public void ApplyAsset(SkillAsset asset)
    {
        _codeName = asset.CodeName;
        _progressDuration = asset.ProgressDuration;
        _coolTimeDuration = asset.CoolTimeDuration;

        _progressTime = 0.0f;
        _coolTimeDuration = 0.0f;
    }
}