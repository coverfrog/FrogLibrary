using System;
using UnityEngine;

[Serializable]
public class SkillContext : SimpleStateContext<SkillController>
{
    // #

    [SerializeField, ReadOnly] private string _codeName;
    
    public void SetCodeName(SkillController _, string codeName) => _codeName = codeName;
    
    // #
    
    [SerializeField, ReadOnly] private SkillStateType _stateType;
    
    public void SetStateType(SkillController _, SkillStateType stateType) => _stateType = stateType;
    
    public SkillStateType StateType => _stateType;
    
    // #

    [SerializeField, ReadOnly] private float _progressTime;
    
    public float ProgressTime => _progressTime;
    
    public void SetProgressTime(SkillStateProgress _,  float progressTime) => _progressTime = progressTime;
    
    // #
    
    [SerializeField, ReadOnly] private float _progressDuration;
    
    public float ProgressDuration => _progressDuration;
    
    public void SetProgressDuration(SkillStateProgress _, float progressDuration) => _progressDuration = progressDuration;
    
    // #
    
    [SerializeField, ReadOnly] private float _coolTime;
    
    public float CoolTime => _coolTime;
    
    public void SetCoolTime(SkillStateCoolTime _, float coolTime) => _coolTime = coolTime;
    
    // #
    
    [SerializeField, ReadOnly] private float _coolTimeDuration;
    
    public float CoolTimeDuration => _coolTimeDuration;
    
    public void SetCoolTimeDuration(SkillStateCoolTime _, float coolDuration) => _coolTimeDuration = coolDuration;
    
    
    // #
    
    public SkillContext(SkillController controller) : base(controller)
    {
       
    }
    
    // #

    public void ApplyAsset(SkillAsset asset, bool isReset = true)
    {
        _codeName = asset.CodeName;
        _progressDuration = asset.ProgressDuration;
        _coolTimeDuration = asset.CoolTimeDuration;

        if (!isReset) return;

        _progressTime = 0.0f;
        _coolTimeDuration = 0.0f;
    }
}