using System;
using UnityEngine;

[Serializable]
public class SkillController
{
    [SerializeField] private SkillContext _context;

    // ---------------------------------------------------------------------------------

    public SkillController() {}

    public SkillController(SkillAsset asset)
    {
        _context = new SkillContext(asset, ChangeStateRequest);
    }
    
    // ---------------------------------------------------------------------------------

    public void OnInput(int slot, bool isClick)
    {
        _context.OnInput(slot, isClick);
    }

    public void OnUpdate(SkillHelper _)
    {
        _context.CurrentState?.OnUpdate(this);
    }
    
    // ---------------------------------------------------------------------------------

    public void ChangeStateRequest(SkillStateType type)
    {
        _context.HandleStateChange(type);

        ChangeStateRequest();
    }
    
    public void ChangeStateRequest()
    {
        var context = _context.Clone();
        var cmd = new SkillUseCommand(this, context);
        
        CommandManager.Instance.Execute(cmd);
    }

    public void ChangeState(SkillContext context)
    {
        context.PrevState?.OnExit(this);
        context.CurrentState?.OnEnter(this);
    }
}