using System;
using UnityEngine;

public class SkillHitBox : MonoBehaviour
{
    [Header("# Component")]
    [SerializeField] private PolygonCollider2D _col;
    [SerializeField] private SpriteRenderer _render;
    
    [Header("# Current")]
    [SerializeField, ReadOnly] public Sprite _spriteCurrent;
    [SerializeField, ReadOnly] private Collider2D _result;

    // ---------------------------------------------------------------------------------
    
    private void Awake()
    {
        if (!_col) _col = GetComponent<PolygonCollider2D>();
        if (!_render) _render = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_result == other) return;

        if (_spriteCurrent == _render.sprite) return;
        
        _spriteCurrent = _render.sprite;
    }
}