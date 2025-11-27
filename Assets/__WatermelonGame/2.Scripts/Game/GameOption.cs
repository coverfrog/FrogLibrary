using System;
using UnityEngine;

namespace WatermelonGame
{
    [Serializable]
    public class GameOption
    {
        [SerializeField] private float _gameOverHeight = 1.0f;
        [SerializeField] private float _gameOverDuration = 2.0f;
        
        public float GameOverHeight => _gameOverHeight;
        
        public float GameOverDuration => _gameOverDuration;
    }
}