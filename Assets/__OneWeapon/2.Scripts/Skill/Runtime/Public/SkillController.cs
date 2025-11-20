using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SkillController : IDisposable, IContextApplyAsset<SkillAsset>
{
    private Dictionary<SkillStateType, ISkillState> _stateDict = new()
    {
        { SkillStateType.Inactive , new SkillStateInactive()},
        { SkillStateType.Locked   , new SkillStateLocked()  },
        { SkillStateType.Enable   , new SkillStateEnable()  },
        { SkillStateType.Disable  , new SkillStateDisable() },
        { SkillStateType.Progress , new SkillStateProgress()},
        { SkillStateType.CoolTime , new SkillStateCoolTime()},
    };
    
    // ---------------------------------------------------------------------------------

    [SerializeField] private SkillContext _context;

    #region Getter

    public SkillContext Context => _context;

    #endregion
    
    // ---------------------------------------------------------------------------------
    
    public SkillController(SkillHelper helper)
    {
        _context = new SkillContext(this);
    }
    
    public void ApplyAsset(SkillAsset asset)
    {
        _context.ApplyAsset(asset);
    }
    
    // ---------------------------------------------------------------------------------
    
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
}