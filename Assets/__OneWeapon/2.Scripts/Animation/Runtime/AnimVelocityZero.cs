using UnityEngine;

public class AnimVelocityZero : StateMachineBehaviour
{
    public bool isStart;
    public bool isExit = true;
    
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        
        if (!isStart) return;
        
        ToZero(animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        
        if (!isExit) return;
        
        ToZero(animator);
    }

    private void ToZero(Animator animator)
    {
        if (!animator.TryGetComponent(out Rigidbody2D rb2d))
        {
            return;
        }
        
        rb2d.linearVelocity = Vector2.zero;
    }
}