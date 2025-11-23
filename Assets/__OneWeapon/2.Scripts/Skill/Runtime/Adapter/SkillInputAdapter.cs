using System;
using UnityEngine;
using UnityEngine.Events;

public class SkillInputAdapter : MonoBehaviour
{
    public UnityEvent<int, bool> OnInput = new();
    
    private void OnEnable()
    {
        InputManager.Instance.OnActClick += OnActClick;
        InputManager.Instance.OnActMiddleClick += OnActMiddleClick;
        InputManager.Instance.OnActRightClick += OnActRightClick;
    }

    private void OnDisable()
    {
        if (InputManager.Instance) InputManager.Instance.OnActClick -= OnActClick;
        if (InputManager.Instance) InputManager.Instance.OnActMiddleClick -= OnActMiddleClick;
        if (InputManager.Instance) InputManager.Instance.OnActRightClick -= OnActRightClick;
    }
    
    private void OnActClick(bool isClick) =>  OnInput?.Invoke(0, isClick);
    private void OnActMiddleClick(bool isClick) => OnInput?.Invoke(1, isClick);
    private void OnActRightClick(bool isClick) =>  OnInput?.Invoke(2, isClick);
}