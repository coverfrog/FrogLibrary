using UnityEngine;

public class AnimMoveLock : StateMachineBehaviour
{
    // private MoveHelper _moveHelper;
    //
    // public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     base.OnStateEnter(animator, stateInfo, layerIndex);
    //
    //     if (_moveHelper == null) _moveHelper = animator.GetComponent<MoveHelper>();
    //     
    //     _moveHelper?.Lock();
    // }
    //
    // public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
    //     base.OnStateExit(animator, stateInfo, layerIndex);
    //     
    //     _moveHelper?.UnLock();
    // }
}