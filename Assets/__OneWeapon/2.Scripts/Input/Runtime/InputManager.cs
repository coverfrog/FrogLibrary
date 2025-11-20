using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>,
    CInput.IPlayerActions,
    CInput.IUIActions
{
    private Dictionary<string, float> _durationDict = new();
    private Dictionary<string, bool> _isPerformedDict = new();
    
    private Dictionary<string, Action> _fixedUpdateDict = new();
    
    private List<string> _actionNames = new();
    
    private CInput _inputModule;
    
    private void OnEnable()
    {
        _inputModule = new CInput();
        
        _inputModule.Player.SetCallbacks(this);
        _inputModule.Enable();
        
        _inputModule.UI.SetCallbacks(this);
        _inputModule.UI.Enable();
        
        _inputModule.Enable();
        
        // # 전체 액션 맵 이름 기준으로 전체 키 등록
        // Input System 은 이름 앞에 'On'을 자동으로 붙혀짐
        
        _isPerformedDict.Clear();
        _fixedUpdateDict.Clear();
        _durationDict.Clear();
        
        _actionNames.Clear();

        foreach (var ia in _inputModule.asset.actionMaps)
        {
            foreach (var a in ia.actions)
            {
                string targetName = $"On{a.name}";
            
                if (!_isPerformedDict.TryAdd(targetName, false)) {}

                if (!_fixedUpdateDict.TryAdd(targetName, null)) {}
                
                if (!_durationDict.TryAdd(targetName, 0)) {}
                
                _actionNames.Add(targetName);
            }
        }
        
        // # Update 필요 목록은 이 곳에 작성
        //
        // 예시 :
        // _updateActions[nameof(OnMove)] = OnMove;

        _fixedUpdateDict[nameof(OnMove)] = OnMove;
    }

    private void OnDisable()
    {
        _inputModule?.Dispose();
    }

    private void FixedUpdate()
    {
        foreach (var actionName in _actionNames)
        {
            if (!_isPerformedDict.TryGetValue(actionName, out  bool performed))
            {
                return;
            }

            if (performed)
            {
                _durationDict[actionName] += Time.fixedDeltaTime;
                _fixedUpdateDict[actionName]?.Invoke();
            }
                
            else
            {
                _durationDict[actionName] = 0;
            }
        }
    }

    private void OnInput(InputAction.CallbackContext context, [CallerMemberName] string caller = "")
    {
        _isPerformedDict[caller] = context.performed;
    }

    private string ToKey([CallerMemberName] string caller = "") => caller;
    
    // ---------------------------------------------------------------------------------
    
    public event Action<Vector2> OnActMove;
    
    public event Action<Vector2, float> OnActMoveFixedUpdate; 
    
    public Vector2 ValueMove { get; private set; }

    public void OnMove()
    {
        OnActMoveFixedUpdate?.Invoke(ValueMove, _durationDict[ToKey()]);
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        ValueMove = context.ReadValue<Vector2>();
        OnActMove?.Invoke(ValueMove);

        OnInput(context);
        OnMove();
    }
    
    // ---------------------------------------------------------------------------------

    public event Action<bool> OnActClick;
    public event Action<bool, float> OnActClickFixedUpdate; 
    
    public bool ValueClick { get; private set; }

    public void OnClick()
    {
        OnActClickFixedUpdate?.Invoke(ValueClick, _durationDict[ToKey()]);
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        ValueClick = context.ReadValue<float>() > 0.5f;
        OnActClick?.Invoke(ValueClick);

        OnInput(context);
        OnClick();
    }

    // ---------------------------------------------------------------------------------
    
    public void OnRightClick(InputAction.CallbackContext context)
    {
        
    }

    // ---------------------------------------------------------------------------------
    
    public void OnMiddleClick(InputAction.CallbackContext context)
    {
       
    }
    
    // ---------------------------------------------------------------------------------
    
    public void OnLook(InputAction.CallbackContext context)
    {
       
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
       
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
       
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        
    }

    public void OnNavigate(InputAction.CallbackContext context)
    {
        
    }

    public void OnSubmit(InputAction.CallbackContext context)
    {
     
    }

    public void OnCancel(InputAction.CallbackContext context)
    {
       
    }

    public void OnPoint(InputAction.CallbackContext context)
    {
        
    }

    

    public void OnScrollWheel(InputAction.CallbackContext context)
    {
        
    }

    public void OnTrackedDevicePosition(InputAction.CallbackContext context)
    {
       
    }

    public void OnTrackedDeviceOrientation(InputAction.CallbackContext context)
    {
        
    }
}