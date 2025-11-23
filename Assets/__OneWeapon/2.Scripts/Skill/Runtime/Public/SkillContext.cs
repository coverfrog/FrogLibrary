using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillContext
{
    [Header("# String")]
    [SerializeField, ReadOnly] private string _codename;
    
    #region Getter
    
    public string CodeName => _codename;
    
    #endregion
    
    [Header("# State")]
    [SerializeField, ReadOnly] private SkillStateType _currentStateType;
    
    #region Getter
    
    public SkillStateType CurrentStateType => _currentStateType;
    
    #endregion
    
    private Dictionary<SkillStateType, ISkillState> _stateDictionary = new Dictionary<SkillStateType, ISkillState>()
    {
        { SkillStateType.Null       , null                  },
        { SkillStateType.Inactive   , new SkillInactive()   },
        { SkillStateType.Locked     , new SkillLocked()     },
        { SkillStateType.Enable     , new SkillEnable()     },
        { SkillStateType.InProgress , new SkillInProgress() },
        { SkillStateType.CoolTime   , new SkillCoolTime()   },
    };
    
    private Dictionary<SkillStateType, ISkillState> StateDictionary => _stateDictionary;

    public ISkillState PrevState { get; private set; }
    
    public ISkillState CurrentState { get; private set; }
    
    [Header("# Type")]
    [SerializeField, ReadOnly] private SkillControlType _controlType;
    [SerializeField, ReadOnly] private SkillInteractType _interactType;
    
    #region Getter
    
    public SkillControlType ControlType => _controlType;
    
    public SkillInteractType InteractType => _interactType;
    
    #endregion
    
    [Header("# Input")]
    [SerializeField, ReadOnly] private bool _isInputUse;
    [SerializeField, ReadOnly] private int _slotIndex = -1;

    #region Getter

    public bool IsInputUse => _isInputUse;
    
    public int SlotIndex => _slotIndex;
    
    #endregion
    
    // ---------------------------------------------------------------------------------

    public SkillContext Clone() => new SkillContext()
    {
        // # String
        _codename = _codename,
        
        // # State
        _currentStateType = _currentStateType,
        _stateDictionary = _stateDictionary,
        
        PrevState = PrevState,
        CurrentState = CurrentState,
        
        // # Type
        _controlType = _controlType,
        _interactType = _interactType,
        
        // # Input
        _isInputUse = _isInputUse,
        _slotIndex = _slotIndex
    };
    
    // ---------------------------------------------------------------------------------
    
    public delegate void ChangeStateDelegate();
    
    // ---------------------------------------------------------------------------------

    private event ChangeStateDelegate _changeStateRequest;
    
    // ---------------------------------------------------------------------------------
    
    public SkillContext() {}
    
    public SkillContext(SkillAsset asset, ChangeStateDelegate changeStateRequest)
    {
        _changeStateRequest = changeStateRequest;
        
        // # String
        _codename = asset.CodeName;
        
        // # Type
        _controlType = asset.ControlType;
        _interactType = asset.InteractType;
        
        // # Input
        _isInputUse = asset.IsInputUse;
        _slotIndex = asset.SlotIndex;;
    }
    
    // ---------------------------------------------------------------------------------
    
    public void OnInput(int slot, bool isClick)
    {
        if (!IsInputUse) return;
        
        if (_slotIndex != slot) return;
        
        if (ControlType == SkillControlType.Passive) return;

        if (CurrentStateType != SkillStateType.Enable) return;
        
        if (InteractType == SkillInteractType.ClickDown && isClick) HandleStateChange(SkillStateType.InProgress);
    }
    
    // ---------------------------------------------------------------------------------

    public void HandleStateChange(SkillStateType currentStateType)
    {
        if (_currentStateType == currentStateType) return;

        var prevStateType = _currentStateType;
        
        Debug.Log($"Handle : ");
        
        PrevState = StateDictionary[prevStateType];
        CurrentState = StateDictionary[currentStateType];
        
        _currentStateType = currentStateType;
        
        _changeStateRequest?.Invoke();
    }
}