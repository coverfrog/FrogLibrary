using System;
using UnityEngine;

[Serializable]
public class MoveComponent
{
    public Rigidbody2D rb2d;
    public Collider2D col;
    public SpriteRenderer render;
    public Animator animator;
    public StatHelper statHelper;
}