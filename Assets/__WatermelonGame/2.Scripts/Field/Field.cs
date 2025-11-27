using System;
using System.Collections.Generic;
using FrogLibrary;
using UnityEngine;
using UnityEngine.Assertions;

namespace WatermelonGame
{
    public class Field : MonoBehaviour
    {
        #region Option

        [SerializeField] private FieldOption _option = new();
                
        private float HalfWidth => _option.Width * 0.5f;
        
        private float HalfHeight => _option.Height * 0.5f;
        
        #endregion

        #region Getter

        public float GetWidth() => _option.Width;

        public float GetWallThickness() => _option.WallThickness;
        
        public Vector3 GetPosition() => transform.position;

        #endregion

        // ------------------------------------

        #region Values

        private Dictionary<TextAnchor, GameObject> WallDictionary { get; } = new();

        private TextAnchor[] WallAnchors { get; } = new[] { TextAnchor.MiddleLeft, TextAnchor.LowerCenter, TextAnchor.MiddleRight };

        #endregion
    
        // ------------------------------------
        
        #region Unity

        private void Start()
        {
            CreateWalls();
        }

        private void OnEnable()
        {
            _option.OnChangedLength += OnChangedLength;
            _option.OnChangedWallColor += OnChangedWallColor;
            _option.OnChangedIsDrawWall += OnChangedIsDrawWall;
        }

        private void OnDisable()
        {
            _option.OnChangedLength -= OnChangedLength;
            _option.OnChangedWallColor -= OnChangedWallColor;
            _option.OnChangedIsDrawWall -= OnChangedIsDrawWall;
        }

        private void OnValidate()
        {
            _option.OnValidate();
        }

        private void OnDrawGizmosSelected()
        {
            if (Application.isPlaying)
                return;
            
            Gizmos.color = Color.green;
            
            foreach (TextAnchor anchor in WallAnchors)
            {
                GetWallInfo(anchor, out Vector3 position, out Vector3 scale);
                
                Gizmos.DrawCube(position, scale);
            }
        }

        #endregion

        // ------------------------------------
        
        #region OnChangedLength

        private void OnChangedLength()
        {
            if (!Application.isPlaying) return;
            
            if (WallDictionary.Count != 3) return;
            
            UpdateTransformWalls();
        }

        #endregion

        #region OnChangedWallColor

        private void OnChangedWallColor()
        {
            UpdateColorWalls();
        }

        #endregion

        #region OnChangedIsDrawWall

        private void OnChangedIsDrawWall()
        {
            UpdateRenderWalls();
        }

        #endregion
        
        // ------------------------------------
        
        #region GetWallInfo

        private void GetWallPosition(TextAnchor anchor, out Vector3 position)
        {
            Vector3 offset = anchor switch
            {
                TextAnchor.MiddleLeft  => new Vector3(-1, 0, 0),
                TextAnchor.LowerCenter => new Vector3(0, -1, 0),
                TextAnchor.MiddleRight => new Vector3(+1, 0, 0),
                _ => Vector3.zero
            };
            
            offset.y *= HalfHeight;
            offset.x *= HalfWidth;

            var wallThickness = _option.WallThickness;
            
            Vector3 reduceThickness = anchor switch
            {
                TextAnchor.MiddleLeft  => Vector3.right * wallThickness * 0.5f,
                TextAnchor.LowerCenter => Vector3.up    * wallThickness * 0.5f,
                TextAnchor.MiddleRight => Vector3.left  * wallThickness * 0.5f,
                _ => Vector3.zero
            };
            
            offset += reduceThickness;
            
            position = transform.position + offset;
        }

        private void GetWallScale(TextAnchor anchor, out Vector3 scale)
        {
            var wallThickness = _option.WallThickness;
            
             scale = anchor switch
            {
                TextAnchor.MiddleLeft  => new Vector3(wallThickness, _option.Height, 1.0f),
                TextAnchor.LowerCenter => new Vector3(_option.Width, wallThickness , 1.0f),
                TextAnchor.MiddleRight => new Vector3(wallThickness, _option.Height, 1.0f),
                _ => Vector3.zero
            };
        }
        
        private void GetWallInfo(TextAnchor anchor, out Vector3 position, out Vector3 scale)
        {
            GetWallPosition(anchor, out position);
            GetWallScale(anchor, out scale);
        }

        #endregion

        // ------------------------------------
        
        #region CreateWalls

        private void CreateWalls()
        {
            foreach (TextAnchor anchor in WallAnchors)
            {
                CreateWall(anchor);
            }
        }

        private void CreateWall(TextAnchor anchor)
        {
            var goName = $"Wall {anchor}";
            
            var wall = new GameObject(goName, typeof(BoxCollider2D), typeof(SpriteRenderer));
            
            var rend = wall.GetComponent<SpriteRenderer>();

            
            WallDictionary.Add(anchor, wall);
            
            UpdateTransformWall(anchor);
            UpdateRenderWall(anchor);
            UpdateColorWall(anchor);
        }

        #endregion
        
        #region UpdateTransformWalls

        private void UpdateTransformWalls()
        {
            foreach (TextAnchor anchor in WallAnchors)
            {
                UpdateTransformWall(anchor);
            }
        }
        
        private void UpdateTransformWall(TextAnchor anchor)
        {
            GetWallInfo(anchor, out Vector3 position, out Vector3 scale);
            
            GameObject go = WallDictionary[anchor].gameObject;
            
            go.transform.SetParent(transform);
            go.transform.position = position;
            go.transform.localScale = scale;
        }

        #endregion

        #region UpdateColorWalls

        private void UpdateColorWalls()
        {
            if (!_option.isDrawWall)
            {
                return;
            }
            
            foreach (TextAnchor anchor in WallAnchors)
            {
                UpdateColorWall(anchor);
            }
        }
        
        private void UpdateColorWall(TextAnchor anchor)
        {
            GameObject go = WallDictionary[anchor].gameObject;

            if (!go.TryGetComponent(out SpriteRenderer render))
                return;
            
            render.color = _option.WallColor;
        }

        #endregion

        #region UpdateRenderWalls

        private void UpdateRenderWalls()
        {
            foreach (TextAnchor anchor in WallAnchors)
            {
                UpdateRenderWall(anchor);
            }
        }
        
        private void UpdateRenderWall(TextAnchor anchor)
        {
            GameObject go = WallDictionary[anchor].gameObject;

            if (!go.TryGetComponent(out SpriteRenderer render))
                return;

            render.enabled = _option.isDrawWall;
            
            if (!_option.isDrawWall)
            {
                return;
            }

            render.sprite = _option.WallSprite;
        }

        #endregion
        
    }
}
