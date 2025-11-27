using FrogLibrary;
using UnityEngine;

namespace WatermelonGame
{
    [CreateAssetMenu(menuName = "WatermelonGame/Projectile/Option")]
    public class ProjectileOption : IdentifiedObject
    {
        [Header("# Projectile")]
        [SerializeField] private Sprite _sprite;
        [SerializeField, Min(0.01f)] private float _scale = 0.1f;
        [SerializeField, Min(1)] private int _score = 4;
        
        public Sprite Sprite => _sprite;
        public float Scale => _scale;
        
        public int Score => _score;
    }
}