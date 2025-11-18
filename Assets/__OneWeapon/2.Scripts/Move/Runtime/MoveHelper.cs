using System;
using UnityEngine;

public class MoveHelper : MonoBehaviour
{
    [Header("# Component")]
    public MoveComponent component;

    [Header("# Context")] 
    [SerializeField] private MoveContext _context;

    [Header("# Asset")]
    [SerializeField] private MoveAsset _asset;
    
    private void OnEnable()
    {
        _context = new MoveContext(this);
        _context.ApplyAsset(_asset);
    }

    private void OnDisable()
    {
        _context?.Dispose();
    }
}