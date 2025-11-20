using System;
using System.Collections.Generic;
using UnityEngine;

public class MoveHelper : MonoBehaviour
{
    [Header("# Component")]
    [SerializeField] private MoveComponent _component;

    [Header("# Context")] 
    [SerializeField] private MoveContext _context;
    
    [Header("# Asset")]
    [SerializeField] private MoveAsset _asset;
    
    // ---------------------------------------------------------------------------------
    
    private void OnEnable()
    {
        EventBus.Subscribe(GameEventBusType.Init, Init);
        EventBus.Subscribe(GameEventBusType.Play, Play);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(GameEventBusType.Init, Init);
        EventBus.Unsubscribe(GameEventBusType.Play, Play);
    }

    // ---------------------------------------------------------------------------------

    private void Init()
    {
        _context?.Dispose();
        
        _context = new MoveContext(MoveRequest);
        _context.ApplyAsset(_asset);
    }

    private void Play()
    {
        _context.SetIsMoveAble(this, true);
    }
    
    // ---------------------------------------------------------------------------------
    
    private void MoveRequest()
    {
        var context = _context.Clone();
        var cmd = new MoveCommand(this, context);
        
        CommandManager.Instance.Execute(cmd);
    }
    
    public void Move(MoveContext context)
    {
        // # anim
        if (context.IsAnimUse)
        {
            var animator = _component.animator;

            animator.SetBool(context.KeyAnimName, context.Direction.sqrMagnitude > 0);
        }

        // # sqr
        if (context.Direction.sqrMagnitude == 0) return;

        // # move
        var rb2d = _component.rb2d;

        var originPosition = rb2d.position;
        var addPosition = context.Direction * (context.Speed * Time.fixedDeltaTime);
        var position = originPosition + addPosition;

        rb2d.MovePosition(position);

        // # flip
        if (context.Direction.x != 0)
        {
            var render = _component.render;

            render.flipX = context.Direction.x < 0;
        }
    }
}