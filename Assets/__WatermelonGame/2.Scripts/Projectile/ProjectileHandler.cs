using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FrogLibrary;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace WatermelonGame
{
    public class ProjectileHandler : MonoBehaviour, IDisposable
    {
        [Header("# Option")]
        [SerializeField] private float _spawnHeight = 3.0f;
        
        [Header("# Index")]
        [SerializeField] private int _levelIndex;
        [SerializeField, ReadOnly] private int _levelIndexMax;

        [Header("# Value")]
        [SerializeField, ReadOnly] private Projectile _projectileNow;
        [SerializeField, ReadOnly] private float _maxHeight;
        [SerializeField, ReadOnly] private bool _isGameOver;
        
        private Projectile _origin;
        private ProjectileOption[] _optionList;

        private IObjectPool<Projectile> _pool;
        private List<Projectile> _activeList = new();

        private Camera _camera;

        private Action<float> _actChangeMaxHeight;
        private Action<int> _actMerge;
        
        private Func<float> _fieldWidthFunc;
        private Func<float> _wallThicknessFunc;
        private Func<Vector3> _fieldPositionFunc;

        // ------------------------------------
        
        #region Unity

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
#if UNITY_STANDALONE
            float x = _camera.ScreenToWorldPoint(Input.mousePosition).x;
            
            MoveHorizontal(x);
#endif
            
            UpdateMaxHeight();
        }

        private void OnEnable()
        {
            EventBus.Subscribe(GameEventBusType.GameOver, OnGameOver);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(GameEventBusType.GameOver, OnGameOver);
        }

        #endregion

        // ------------------------------------
        
        #region Init

        public void Init(
            Action<float> actChangeMaxHeight, 
            Action<int> actMerge, 
            Func<float> fieldWidthFunc, 
            Func<float> wallThicknessFunc,
            Func<Vector3> fieldPositionFunc)
        {
            _actChangeMaxHeight = actChangeMaxHeight;
            _actMerge           = actMerge;
            
            _fieldWidthFunc    = fieldWidthFunc;
            _wallThicknessFunc = wallThicknessFunc;
            _fieldPositionFunc = fieldPositionFunc;
            
            if (_origin == null)
            {
                _origin = Resources.Load<Projectile>("Projectile/Origin/Projectile");
            }

            if (_optionList == null)
            {
                _optionList = Resources.LoadAll<ProjectileOption>("Projectile/Options");
                _levelIndexMax = _optionList.Length;
            }

            if (_pool == null)
            {
                CreatePool();
            }
           
            var position = Vector3.up * _spawnHeight;
            
            SpawnRand(position, true);
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            
        }

        #endregion

        // ------------------------------------
        
        #region CreatePool
        
        private void CreatePool()
        {
            _pool = new ObjectPool<Projectile>(
                createFunc: () =>
                {
                    Projectile p = Instantiate(_origin, transform);

                    return p;
                }, 
                actionOnGet: p =>
                {
                   p.gameObject.SetActive(true);
                   
                   _activeList.Add(p);
                    
                }, 
                actionOnRelease: p =>
                {
                    p.gameObject.SetActive(false);
                    
                   _activeList.Remove(p);
                }, 
                actionOnDestroy: p =>
                {
                    
                });
        }

        #endregion

        // ------------------------------------
        
        #region OnClick

        public void OnClick(bool isClick)
        {
            if (isClick)
            {
                Drop();
            }
        }

        #endregion

        #region OnGameOver

        private void OnGameOver()
        {
            _isGameOver = true;
            
            for (var i = _activeList.Count - 1; i >= 0; i--)
            {
                _activeList[i].OnGameOver();
            }
        }

        #endregion

        // ------------------------------------
        
        #region Merge

        private void Merge(Projectile lhs, Projectile rhs)
        {
            StartCoroutine(CoMerge(lhs, rhs));
        }

        #endregion
        
        #region CoMerge

        private IEnumerator CoMerge(Projectile lhs, Projectile rhs)
        {
            yield return FrameStatic.EndOfFrame;
            
            var levelIndex = Mathf.Clamp(lhs.LevelIndex + 1, 0, _levelIndexMax);

            if (levelIndex != _levelIndexMax)
            {
                var position = lhs.transform.position + rhs.transform.position;
                position *= 0.5f;
            
                Projectile p = Spawn(levelIndex, position, false);
                p.OnMerge();
            }

            try
            {
                _pool.Release(lhs);
                _pool.Release(rhs);
            }
            
            catch (InvalidOperationException e) {}

            var score = _optionList[lhs.LevelIndex].Score;
            
            _actMerge?.Invoke(score);
        }

        #endregion
        
        // ------------------------------------
        
        #region Spawn

        private Projectile Spawn(int levelIndex, Vector3 position, bool isNowProjectile)
        {
            levelIndex = Mathf.Clamp(levelIndex, 0, _levelIndexMax);
            
            var p = _pool.Get();
            var option = _optionList[levelIndex];
            
            p.Init(Merge, option, levelIndex);
            p.transform.position = position;
            
            if (isNowProjectile) _projectileNow = p;
            
            return p;
        }

        #endregion
        
        #region SpawnRand

        private Projectile SpawnRand(Vector3 position, bool isNowProjectile)
        {
            var levelIndex = Random.Range(0, 3);
            
            return Spawn(levelIndex, position, isNowProjectile);
        }

        #endregion
        
        #region Drop

        private void Drop()
        {
            if (_isGameOver)
            {
                return;
            }
            
            _projectileNow?.OnDrop();

            var position = Vector3.up * _spawnHeight;
            
            SpawnRand(position, true);
        }

        #endregion
        
        #region MoveHorizontal

        private void MoveHorizontal(float x)
        {
            if (_isGameOver)
            {
                return;
            }

            var halfWidth = _fieldWidthFunc() * 0.5f;
            halfWidth -= _projectileNow.GetScaleX();
            halfWidth -= _wallThicknessFunc();
            
            x = Mathf.Clamp(x, -halfWidth, +halfWidth);
            
            if (_projectileNow is  { IsDropped: true })
            {
                return;
            }
            
            var position = _fieldPositionFunc() + new Vector3(x, _spawnHeight);
            _projectileNow.transform.position = position;
        }

        #endregion

        #region UpdateMaxHeight

        private void UpdateMaxHeight()
        {
            if (_isGameOver)
            {
                return;
            }
            
            float maxHeight = float.MinValue;
            
            foreach (Projectile p in _activeList)
            {
                if (!p.IsBounced)
                    continue;
                
                maxHeight = Mathf.Max(maxHeight, p.transform.position.y);
            }
            
            if (Mathf.Approximately(maxHeight, float.MinValue))
                return;

            maxHeight = Mathf.Round(maxHeight * 100f) / 100f;
  
            _maxHeight = maxHeight;
            _actChangeMaxHeight?.Invoke(_maxHeight);
        }

        #endregion
    }
}