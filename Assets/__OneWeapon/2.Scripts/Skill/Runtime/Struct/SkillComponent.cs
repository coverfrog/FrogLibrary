using System;
using UnityEngine;

[Serializable]
public class SkillComponent
{
    public Rigidbody2D rb2d;
    public Animator animator;
    public SpriteRenderer render;
    public SkillHitBox hitBox;
}