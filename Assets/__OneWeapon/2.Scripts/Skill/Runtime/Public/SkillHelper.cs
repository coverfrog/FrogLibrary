using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillHelper : MonoBehaviour
{
    [SerializeField] private SkillComponent _component = new();
    [SerializeField] private List<SkillAsset> _assetList = new();
    [SerializeField] private List<SkillController> _controllerList = new();
    
    // ---------------------------------------------------------------------------------
    
    private void Awake()
    {
        _assetList.RemoveAll(asset => !asset);
        
        _controllerList.Clear();
        _assetList.ForEach(asset => _controllerList.Add(new SkillController(asset)));
    }

    private void Start()
    {
        // [cskim][25.11.24]
        // > 원래는 스킬 가능 조건을 검사해야 하지만 이 게임에선 필요로 하지 않음
        
        _controllerList.ForEach(ctrl => ctrl.ChangeStateRequest(SkillStateType.Enable));
    }

    private void OnEnable()
    {
        _controllerList.ForEach(ctrl => _component.inputAdapter.OnInput.AddListener(ctrl.OnInput));
    }

    private void OnDisable()
    {
        _controllerList.ForEach(ctrl => _component.inputAdapter.OnInput.RemoveListener(ctrl.OnInput));
    }

    private void Update()
    {
        _controllerList.ForEach(ctrl => ctrl.OnUpdate(this));
    }
}