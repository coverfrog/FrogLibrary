using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveHelper : MonoBehaviour
{
    [SerializeField] private MoveComponent _component = new();
    [SerializeField] private MoveContext _context = new();
    [SerializeField] private MoveAsset _asset;
    
    // ---------------------------------------------------------------------------------
    
    private void OnEnable()
    {
        EventBus.Subscribe(GameEventBusType.Init, Init);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(GameEventBusType.Init, Init);
    }

    private void Update()
    {
        UpdateIsGrounded();
    }

    // ---------------------------------------------------------------------------------

    private void Init()
    {
        _context?.Dispose();
        
        _context = new MoveContext(MoveRequest, JumpRequest, StatUpdateRequest);
        _context.ApplyAsset(_asset);
        _context.SetIsMoveAble(this, true);
        _context.SetIsJumpAble(this, true);
    }
    
    // ---------------------------------------------------------------------------------

    private void StatUpdateRequest(string codename, Action<float> onSetAction)
    {
        if (!_component.statHelper) return;
        
        if (!_component.statHelper.GetValue(codename, out var value)) return;

        onSetAction?.Invoke(value);
    }
    
    // ---------------------------------------------------------------------------------
    
    private void UpdateIsGrounded()
    {
        if (!_component.col) return;

        var bounds = _component.col.bounds;
        
        var origin = new Vector2(bounds.center.x, bounds.min.y);
        var direction = Vector2.down;

        var size = Physics2D.RaycastNonAlloc(origin, direction, _context.GroundHitList, 0.01f, _context.GroundLayer);

        bool isGround = false;
        
        for (int i = 0; i < size; i++)
        {
            var hit = _context.GroundHitList[i];
            
            if (((1 << hit.collider.gameObject.layer) & _context.GroundLayer) == 0) 
                continue;
            
            isGround = true;
            
            break;
        }
        
        _context.SetIsGrounded(this, isGround);
    }
    
    // ---------------------------------------------------------------------------------

    private void JumpRequest()
    {
        var context = _context.Clone();
        var cmd = new MoveJumpCommand(this, context);
        
        CommandManager.Instance.Execute(cmd);
    }

    public void Jump(MoveContext context)
    {
        // # move
        var rb2d = _component.rb2d;

        rb2d.linearVelocityY = context.JumpForce;
    }
    
    // ---------------------------------------------------------------------------------
    
    private void MoveRequest()
    {
        var context = _context.Clone();
        var cmd = new MovePositionCommand(this, context);
        
        CommandManager.Instance.Execute(cmd);
    }
    
    public void Move(MoveContext context)
    {
        // # anim
        if (context.IsAnimUse)
        {
            var animator = _component.animator;

            animator.SetBool(context.KeyAnimMove, context.Direction.sqrMagnitude > 0);
        }

        // # sqr
        if (context.Direction.sqrMagnitude == 0) return;

        // # move
        var rb2d = _component.rb2d;

        var originPosition = rb2d.position;
        var moveVelocity = context.Direction * (context.MoveSpeed * Time.fixedDeltaTime);
        var position = originPosition + moveVelocity;

        // [cskim][25.11.23] 
        // > Jump 추가 되면서 Y 축은 이곳에서 직접 제어하면 안됨
        
        //rb2d.MovePosition(position);
        rb2d.linearVelocityX = context.Direction.x * context.MoveSpeed;
        
        // # flip
        if (context.Direction.x == 0) return;
        
        var render = _component.render;

        render.flipX = context.Direction.x < 0;
    }
}