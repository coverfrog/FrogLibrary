using UnityEngine;

public class AnimDynamicLength : StateMachineBehaviour
{
    public static void Apply(Animator animator, float duration)
    {
        var dynamicLength = animator.GetBehaviours<AnimDynamicLength>();
        
        foreach (var b in dynamicLength)
        {
            b.targetDuration = duration;
        }
    }
    
    private static readonly int KeyParameter = Animator.StringToHash("DynamicSpeed");

    [ReadOnly] public float targetDuration = 1.0f;

    private float _originalSpeed = 1f;
    private float _clipLength;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // # get clips all and find this state
        var clips = animator.runtimeAnimatorController.animationClips;
        foreach (var clip in clips)
        {
            if (!stateInfo.IsName(clip.name)) 
                continue;
            
            _clipLength = clip.length;
            
            break;
        }

        // # save original speed
        _originalSpeed = stateInfo.speedMultiplier;
        
        // # speed calc
        //
        // 타겟 클립 길이 / 타겟 길이
        // 예시 : 0.2f / 2.0f = 0.1f
        float speed = _clipLength / targetDuration;

        // # speed apply
        animator.SetFloat(KeyParameter, speed);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // # convert speed
        animator.SetFloat(KeyParameter, _originalSpeed);
    }
}