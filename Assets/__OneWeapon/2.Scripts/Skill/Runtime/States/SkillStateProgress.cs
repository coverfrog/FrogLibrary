using System.Collections.Generic;
using UnityEngine;

public class SkillStateProgress : ISkillState
{
    public SkillStateType StateType => SkillStateType.Progress;

    // private readonly List<Vector2> _physicsShapePath = new();
    //
    // private bool _isTransition;

    public void OnEnter(SkillController controller)
    {
        // _isTransition = false;
        //
        // var asset = controller.Asset;
        // var component = controller.Helper.component;
        // var context = controller.Context;
        //
        // context.SetProgressTime(this, context.ProgressDuration);
        //
        // if (asset.IsAnimate)
        // {
        //     component.animator.SetTrigger(asset.AnimatorName);
        //     
        //     AnimDynamicLength.Apply(component.animator, asset.ProgressDuration);
        // }
    }

    public void OnExit(SkillController controller)
    {
        // var component = controller.Helper.component;
        // var context = controller.Context;
        //
        // var col = component.hitBox.col;
        // col.enabled = false;
        //
        // context.SetProgressTime(this, 0);
    }

    public void OnUpdate(SkillController controller)
    {
        // if (_isTransition)
        // {
        //     return;
        // }
        //
        // OnTime(controller, out _isTransition);
        //
        // if (_isTransition)
        // {
        //     controller.Transition(SkillStateType.CoolTime);
        //     return;
        // }
        //
        // if (asset.IsPolygonPhysics)
        // {
        //     OnPolygonPhysics(controller);
        // } 
    }

    private void OnTime(SkillController controller, out bool isTransition)
    {
        var context = controller.Context;

        var time = Mathf.Max(0, context.ProgressTime - Time.deltaTime);
        
        context.SetProgressTime(this, time);
        // controller.HandleOnStateProgress(this, time, context.ProgressDuration);

        isTransition = time == 0;
    }

    private void OnPolygonPhysics(SkillController controller)
    {
        // var component = controller.Helper.component;
        //
        // if (component.hitBox.enabled)
        // {
        //     component.hitBox.col.enabled = false;
        // }
        //
        // var shapeCount = component.render.sprite.GetPhysicsShapeCount();
        //
        // if (shapeCount == 0)
        // {
        //     component.hitBox.spriteCurrent = null;
        //     component.hitBox.render.sprite = null;
        //     
        //     return;
        // }
        //
        // component.render.sprite.GetPhysicsShape(0, _physicsShapePath);
        //
        // for (int i = 0; i < _physicsShapePath.Count; i++)
        // {
        //     Vector2 v = _physicsShapePath[i];
        //     v.x *= (component.render.flipX ? -1 : +1);
        //     
        //     _physicsShapePath[i] = v;
        // }
        //
        // component.hitBox.col.SetPath(0, _physicsShapePath);
        // component.hitBox.col.enabled = true;
        //
        // component.hitBox.render.sprite = component.render.sprite;
    }
}