using UnityEngine;

public class SkillStateCoolTime : ISkillState
{
    public SkillStateType StateType => SkillStateType.CoolTime;
    
    private bool _isTransition;
    
    public void OnEnter(SkillController controller)
    {
        _isTransition = false;
        
        var asset = controller.Asset;
        var context = controller.Context;
            
        context.SetCoolTime(this, asset.CoolTimeDuration);
        context.SetCoolTimeDuration(this, asset.CoolTimeDuration);
    }
    
    public void OnExit(SkillController controller)
    {
        
    }

    public void OnUpdate(SkillController controller)
    {
        if (_isTransition)
        {
            return;
        }
        
        OnTime(controller, out _isTransition);
        
        if (_isTransition)
        {
            controller.Transition(SkillStateType.Enable);
            return;
        }
    }

    private void OnTime(SkillController controller, out bool isTransition)
    {
        var context = controller.Context;
        
        var time = Mathf.Max(0, context.CoolTime - Time.deltaTime);
        
        context.SetCoolTime(this, time);
        controller.HandleOnStateCoolTime(this, time, context.CoolTimeDuration);
        
        isTransition = time == 0;
    }
}