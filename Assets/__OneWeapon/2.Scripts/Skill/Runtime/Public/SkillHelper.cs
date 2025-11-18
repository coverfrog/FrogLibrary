using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillHelper : MonoBehaviour
{
    [Header("# Component")]
    public SkillComponent component = new();
    
    [Header("# Init")]
    [SerializeField] private List<SkillAsset> _assetList = new();
    
    [Header("# Runtime")]
    [SerializeField] private List<SkillController> _controllerList = new();
    
    // # OnEnable
    
    private void OnEnable()
    {
        // # remove asset null
        
        _assetList.RemoveAll(asset => asset == null);
        
        // # asset -> controller
        
        _controllerList.Clear();
        _assetList.ForEach(asset => _controllerList.Add(new SkillController(this, asset)));
        
        // # register controller list
        //
        // 이벤트 발행을 따로 하는 이유는 최초 상태를 추격하지 않으려 하기 위함이다.
        // Disable 되는 경우 전부 Clear 처리 하기 때문에 따로 이벤트 해제는 하지 않음
        
        _controllerList.ForEach(controller => controller.OnStateChanged  += OnStateChanged);
        _controllerList.ForEach(controller => controller.OnStateProgress += OnStateProgress);
        _controllerList.ForEach(controller => controller.OnStateCoolTime += OnStateCoolTime);
        
        // # transition
        // 
        // 원래라면 조건 ( 레벨, 능력치 ) 에 따라 조건이 변경 되야 하나 현재는 바로 사용 가능 상태로 전환
        // 현재 게임에선 해당 사항이 없으므로 진행되지 않음
        
        _controllerList.ForEach(controller => controller.Transition(SkillStateType.Enable));
    }
    
    // # OnDisable
    
    private void OnDisable()
    {
        // # clear controller
        
        _controllerList.ForEach(controller => controller.Dispose());
        _controllerList.Clear();
    }

    // # Update
    
    private void Update()
    {
        _controllerList.ForEach(controller => controller.OnUpdate());
    }

    // # OnStateChanged
    
    private void OnStateChanged(SkillController controller, ISkillState prevState, ISkillState newState)
    {
   
    }

    // # OnStateProgress
    //
    // 외부에서 현재 진행 중인지 알아야 하므로
    
    private void OnStateProgress(SkillStateProgress _, SkillController controller,  float time, float duration)
    {
        
    }

    // # OnStateCoolTime
    //
    // 쿨타임 정보 역시 외부에서 알아야 하므로
    
    private void OnStateCoolTime(SkillStateCoolTime _, SkillController controller, float time, float duration)
    {
        
    }
    
    // # Handle
    
    public void Handle(string assetCodeName)
    {
        // # find
        //
        // 기술 자체를 못찾은거면 코드 자체가 에러
        
        var controller = _controllerList.FirstOrDefault(controller => controller.CodeName == assetCodeName);
        if (controller == null)
        {
            return;
        }
        
        // # state
        //
        // 사용에 실패 했다면 사용자에게 알려주어야 함
        
        if (controller.StateType != SkillStateType.Enable)
        {
            return;
        }
        
        // # transition
        
        controller.Transition(SkillStateType.Progress);
    }
}