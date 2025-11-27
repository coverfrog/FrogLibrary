using System;
using FrogLibrary;
using UnityEngine;
using Random = System.Random;

namespace WatermelonGame
{
    public class GameHelper : MonoBehaviour
    {
        [Header("# Option")] 
        [SerializeField] private GameOption _option = new();

        [Header("# Component")]
        [SerializeField] private Field _field;
        [SerializeField] private InputObserver _inputObserver;
        [SerializeField] private ProjectileHandler _projectileHandler;

        [Header("# Value")]
        [SerializeField, ReadOnly] private bool _isGameOver;
        [SerializeField, ReadOnly] private float _gameOverTimer;
        [SerializeField, ReadOnly] private float _score;
        
        // ------------------------------------
        
        #region Unity

        private void Awake()
        {
            EventBus.RegisterEvent<GameEventBusType>();     
        }

        private void OnEnable()
        {
            _projectileHandler.Init(
                OnChangeMaxHeight, 
                OnMerge, 
                _field.GetWidth,
                _field.GetWallThickness,
                _field.GetPosition);
            
            _inputObserver.Init();
            _inputObserver.OnClick += _projectileHandler.OnClick;

            _score = 0;
            _isGameOver = false;
            _gameOverTimer = 0;
        }

        private void OnDisable()
        {
            _projectileHandler.Dispose();
            
            _inputObserver.OnClick -= _projectileHandler.OnClick;
            _inputObserver.Dispose();
        }

        #endregion
        
        // ------------------------------------

        #region OnMerge

        private void OnMerge(int score)
        {
            if (_isGameOver)
                return;

            _score += score;
        }

        #endregion

        #region OnChangeMaxHeight

        private void OnChangeMaxHeight(float newMaxHeight)
        {
            if (_isGameOver)
                return;
            
            if (newMaxHeight < _option.GameOverHeight)
            {
                _gameOverTimer = 0.0f;
                return;
            }
            
            _gameOverTimer += Time.deltaTime;

            if (_gameOverTimer < _option.GameOverDuration)
                return;

            _isGameOver = true;
            
            OnGameOver();
        }

        #endregion

        #region OnGameOver

        private void OnGameOver()
        {
            EventBus.Publish(GameEventBusType.GameOver);            
        }

        #endregion
    }
}
