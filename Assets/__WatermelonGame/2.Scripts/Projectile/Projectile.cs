using System;
using Unity.Collections;
using UnityEngine;

namespace WatermelonGame
{
    public class Projectile : MonoBehaviour
    {
        [Header("# Value")]
        [SerializeField, ReadOnly] private bool _isDropped;
        [SerializeField, ReadOnly] private bool _isBounced;
        [Space]
        [SerializeField, ReadOnly] private int _levelIndex;
        
        public bool IsDropped => _isDropped;
        public bool IsBounced => _isBounced;
        public int LevelIndex => _levelIndex;
        
        [Header("# Components")]
        [SerializeField] private SpriteRenderer _render;
        [SerializeField] private Rigidbody2D _rb2d;
        [SerializeField] private CircleCollider2D _col;

        // ------------------------------------
        
        public delegate void MergeDelegate(Projectile lhs, Projectile rhs);
        
        
        private MergeDelegate _actMerge;
        

        // ------------------------------------
 
        public void Init(MergeDelegate actMerge, ProjectileOption option, int levelIndex)
        {
            _actMerge = actMerge;
            
            _isDropped = false;
            _isBounced = false;
            
            _render.sprite = option.Sprite;
            _render.sortingOrder = 100;
            _rb2d.gravityScale = 0.0f;
            _col.enabled = false;

            _isDropped = false;
            _levelIndex = levelIndex;
            
            transform.localScale = Vector3.one * option.Scale;
            gameObject.name = $"Projectile {levelIndex + 1}";
        }

        // ------------------------------------

        public float GetScaleX()
        {
            return transform.lossyScale.x;
        }
        
        // ------------------------------------
        
        public void OnDrop()
        {
            _rb2d.gravityScale = 1.0f;
            _render.sortingOrder = 0;
            _col.enabled = true;
            
            _isDropped = true;
        }

        public void OnMerge()
        {
            _rb2d.gravityScale = 1.0f;
            _col.enabled = true;
        }

        public void OnGameOver()
        {
            _rb2d.gravityScale = 0.0f;
            _rb2d.linearVelocity = Vector2.zero;
            _rb2d.angularVelocity = 0;
            
            _col.enabled = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!IsDropped) 
                _isBounced = true;
            
            Projectile lhs = this;
            
            if (!collision.collider.gameObject.TryGetComponent(out Projectile rhs))
                return;
            
            if (lhs._levelIndex != rhs._levelIndex)
                return;

            if (lhs.gameObject.GetInstanceID() < rhs.gameObject.GetInstanceID())
                return;
            
            _actMerge?.Invoke(lhs, rhs);
        }
    }
}