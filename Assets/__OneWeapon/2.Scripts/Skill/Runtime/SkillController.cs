using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SkillController : IDisposable
{
    // # Event
    
    public delegate void OnStateChangedDelegate(SkillController controller, ISkillState prevState, ISkillState newState);
    
    public delegate void OnProgressDelegate(SkillStateProgress _,  SkillController controller, float time, float duration);
    
    public delegate void OnCoolTimeDelegate(SkillStateCoolTime _, SkillController controller,float time, float duration);

    public event OnStateChangedDelegate OnStateChanged;

    private void HandleOnStateChanged(ISkillState prevState, ISkillState newState)
    {
        OnStateChanged?.Invoke(this, prevState, newState);
    }


    public event OnProgressDelegate OnStateProgress;

    public void HandleOnStateProgress(SkillStateProgress progress, float time, float duration)
    {
        OnStateProgress?.Invoke(progress, this, time, duration);
    }

    public event OnCoolTimeDelegate OnStateCoolTime;
    
    public void HandleOnStateCoolTime(SkillStateCoolTime progress, float time, float duration)
    {
        OnStateCoolTime?.Invoke(progress, this, time, duration);
    }

    // #
    
    private Dictionary<SkillStateType, ISkillState> _stateDict = new()
    {
        { SkillStateType.Inactive , new SkillStateInactive()},
        { SkillStateType.Locked   , new SkillStateLocked()  },
        { SkillStateType.Enable   , new SkillStateEnable()  },
        { SkillStateType.Disable  , new SkillStateDisable() },
        { SkillStateType.Progress , new SkillStateProgress()},
        { SkillStateType.CoolTime , new SkillStateCoolTime()},
    };
    
    // #

    [SerializeField] private string _codeName;
    
    public string CodeName => _codeName;
    
    [SerializeField] private SkillContext _context;

    public SkillContext Context => _context;
    
    [SerializeField, ReadOnly] private SkillAsset _asset;
    
    public SkillAsset Asset => _asset;
    
    public SkillHelper Helper { get; }
    
    public SkillController(SkillHelper helper, SkillAsset asset)
    {
        Helper = helper;
        
        _context = new SkillContext(this);
        _context.ApplyAsset(asset);
        
        _asset = asset;
        _codeName = _asset.CodeName;
        
        _context.OnStateChanged += (prevState, currentState) =>
        {
            HandleOnStateChanged((ISkillState)prevState, (ISkillState)currentState);
        };
    }
    
    public void Dispose()
    {
        
    }
    
    public void OnUpdate()
    {
        _context.CurrentState?.OnUpdate(this);
    }

    public void Transition(SkillStateType stateType)
    {
        _context.SetStateType(this, stateType);
        _context.Transition(_stateDict[stateType]);
    }


    public SkillStateType StateType => _context.StateType;

}