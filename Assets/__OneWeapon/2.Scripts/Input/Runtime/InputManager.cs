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
        
        _isPerformedDict.Clear();
        _fixedUpdateDict.Clear();
        _durationDict.Clear();

        _actionNames = InputUtil.GetActionNameList(_inputModule.asset, true);
        _actionNames.ForEach(str =>
        {
            _isPerformedDict.Add(str, false);   
            _fixedUpdateDict.Add(str, null);   
            _durationDict.Add(str, 0);
        });

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
            if (!_isPerformedDict.TryGetValue(actionName, out bool performed))
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
    
    public bool ValueClick { get; private set; }

    public void OnClick()
    {
        
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        ValueClick = context.ReadValue<float>() > 0.5f;
        OnActClick?.Invoke(ValueClick);

        OnInput(context);
        OnClick();
    }

    // ---------------------------------------------------------------------------------
    
    public event Action<bool> OnActRightClick;
    
    public bool ValueRightClick { get; private set; }

    public void OnRightClick()
    {
        
    }

    public void OnRightClick(InputAction.CallbackContext context)
    {
        ValueRightClick = context.ReadValue<float>() > 0.5f;
        OnActRightClick?.Invoke(ValueClick);

        OnInput(context);
        OnRightClick();
    }

    // ---------------------------------------------------------------------------------
    
    public event Action<bool> OnActMiddleClick;
    
    public bool ValueMiddleClick { get; private set; }

    public void OnMiddleClick()
    {
        
    }
    
    public void OnMiddleClick(InputAction.CallbackContext context)
    {
        ValueMiddleClick = context.ReadValue<float>() > 0.5f;
        OnActMiddleClick?.Invoke(ValueClick);

        OnInput(context);
        OnMiddleClick();
    }
    
    // ---------------------------------------------------------------------------------
    
    public event Action<bool> OnActJump;
    
    public bool ValueJump { get; private set; }

    public void OnJump()
    {
        
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        ValueJump = context.ReadValue<float>() > 0.5f;
        OnActJump?.Invoke(ValueJump);

        OnInput(context);
        OnJump();
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