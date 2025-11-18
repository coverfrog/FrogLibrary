using System;
using UnityEngine;

public class SkillHitBox : MonoBehaviour
{
    [Header("# Component")]
    public PolygonCollider2D col;
    public SpriteRenderer render;
    
    [Header("# Current")]
    [ReadOnly] public Sprite spriteCurrent;
    
    private Collider2D _result;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_result == other) return;

        if (spriteCurrent == render.sprite) return;
        
        spriteCurrent = render.sprite;
        
        Debug.Log(other.gameObject.name);
    }
}