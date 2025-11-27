using System;
using FrogLibrary;
using UnityEngine;

namespace WatermelonGame
{
    [Serializable]
    public class FieldOption
    {
        [Header("# Flag")] 
        [SerializeField] private bool _isDrawWall = true;

        private bool _isDrawWallPrev;
        
        #region Property

        public bool isDrawWall
        {
            get => _isDrawWall;
            set
            {
                _isDrawWall = value;
                OnChangedIsDrawWall?.Invoke();
            }
        }
        
        #endregion

        #region Event

        public delegate void ChangedIsDrawWallDelegate();
        
        public event ChangedIsDrawWallDelegate OnChangedIsDrawWall;

        #endregion
        
        // ------------------------------------
        
        [Header("# Length")] 
        [SerializeField, Min(1)] private float _width = 20;
        [SerializeField, Min(1)] private float _height = 20;
        [SerializeField, Min(0.01f)] private float _wallThickness = 0.1f;

        private float _widthPrev;
        private float _heightPrev;
        private float _wallThicknessPrev;

        #region Property

        public float Width
        {
            get => _width;
            set
            {
                _width = value;
                OnChangedLength?.Invoke();
            }
        }

        public float Height
        {
            get => _height;
            set
            {
                _height = value;
                OnChangedLength?.Invoke();
            }
        }

        public float WallThickness
        {
            get => _wallThickness;
            set
            {
                _wallThickness = value;
                OnChangedLength?.Invoke();
            }
        }

        #endregion

        #region Event

        public delegate void ChangedLengthDelegate();
        
        public ChangedLengthDelegate OnChangedLength;

        #endregion
     
        // ------------------------------------
        
        [Header("# Sprite")] 
        [SerializeField] private Color _wallColor = Color.white;
        [SerializeField, ReadOnly] private Sprite _wallSprite;

        private Color _wallColorPrev;
        
        #region Property

        public Color WallColor
        {
            get => _wallColor;
            set
            {
                _wallColor = value;
                OnChangedWallColor?.Invoke();
            }
        }
        
        public Sprite WallSprite
        {
            get
            {
                if (!Application.isPlaying)
                    return null;
                
                if (_wallSprite != null) 
                    return _wallSprite;
                
                _wallSprite = SpriteUtil.CreateSolidSprite(_wallColor);
                _wallSprite.name = "Created By Field";

                return _wallSprite;
            }
        }

        #endregion

        #region Event

        public delegate void ChangedWallColorDelegate();
        
        public event ChangedWallColorDelegate OnChangedWallColor;

        #endregion
        
        // ------------------------------------

        #region OnValidate

        public void OnValidate()
        {
            if (!Mathf.Approximately(_widthPrev, _width))
            {
                _widthPrev = _width;
                OnChangedLength?.Invoke();
            }
            
            if (!Mathf.Approximately(_heightPrev, _height))
            {
                _heightPrev = _height;
                OnChangedLength?.Invoke();
            }
            
            if (!Mathf.Approximately(_wallThicknessPrev, _wallThickness))
            {
                _wallThicknessPrev = _wallThickness;
                OnChangedLength?.Invoke();
            }

            if (_wallColorPrev != _wallColor)
            {
                _wallColorPrev = _wallColor;
                OnChangedWallColor?.Invoke();
            }

            if (_isDrawWallPrev != _isDrawWall)
            {
                _isDrawWallPrev = _isDrawWall;
                OnChangedIsDrawWall?.Invoke();
            }
        }

        #endregion
    }
}