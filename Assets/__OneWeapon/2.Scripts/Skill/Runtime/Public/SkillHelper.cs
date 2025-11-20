using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillHelper : MonoBehaviour
{
    [Header("# Component")]
    [SerializeField] private SkillComponent _component = new();
    
    [Header("# Init")]
    [SerializeField] private List<SkillAsset> _assetList = new();
    
    [Header("# Runtime")]
    [SerializeField] private List<SkillController> _controllerList = new();
    
    // ---------------------------------------------------------------------------------
    
    private void OnEnable()
    {
        // # remove asset null
        
        _assetList.RemoveAll(asset => asset == null);
        
        // # asset -> controller
        
        _controllerList.Clear();
        _assetList.ForEach(asset =>
        {
            var controller = new SkillController(this); 
            controller.ApplyAsset(asset);
            controller.Transition(SkillStateType.Enable);
            
            _controllerList.Add(controller);
        });
        
        InputManager.Instance.OnActClick += OnActClick;
    }

    private void OnDisable()
    {
        // # clear controller
        
        _controllerList.ForEach(controller => controller.Dispose());
        _controllerList.Clear();
        
        if (InputManager.Instance) InputManager.Instance.OnActClick -= OnActClick;
    }

    private void Update()
    {
        _controllerList.ForEach(controller => controller.OnUpdate());
    }

    // ---------------------------------------------------------------------------------
    
    private void OnActClick(bool icClick)
    {
        if (icClick)
        {
            Debug.Log("Click");
        }
    }
    
    // ---------------------------------------------------------------------------------
    
    
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
        
        var controller = _controllerList.FirstOrDefault(controller => controller.Context.CodeName == assetCodeName);
        if (controller == null)
        {
            return;
        }
        
        // # state
        //
        // 사용에 실패 했다면 사용자에게 알려주어야 함
        
        if (controller.Context.StateType != SkillStateType.Enable)
        {
            return;
        }
        
        // # transition

        // var command = new SkillCommand(controller);
        //
        // CommandManager.Instance.Execute(command);
    }
}