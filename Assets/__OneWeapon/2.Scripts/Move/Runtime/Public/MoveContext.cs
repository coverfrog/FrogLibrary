using System;
using UnityEngine;

[Serializable]
public class MoveContext : IDisposable, IContextApplyAsset<MoveAsset>
{
    [Header("# Lock")]
    [SerializeField, ReadOnly] private bool _isLockHorizontal;
    [SerializeField, ReadOnly] private bool _isLockVertical;

    #region Getter

    public bool IsLockHorizontal => _isLockHorizontal;
    public bool IsLockVertical => _isLockVertical;

    #endregion
    
    [Header("# Anim")] 
    [SerializeField, ReadOnly] private bool _isAnimUse = true;
    [SerializeField, ReadOnly] private string _keyAnimName = "IsMove";

    #region Getter

    public bool IsAnimUse => _isAnimUse;
    public string KeyAnimName => _keyAnimName;

    #endregion
    
    [Header("# Flag")]
    [SerializeField, ReadOnly] private bool _isInputUse;
    [SerializeField, ReadOnly] private bool _isMoveAble;

    #region Setter

    public void SetIsMoveAble(MoveHelper _, bool isMoveAble) => _isMoveAble = isMoveAble;

    #endregion
    
    [Header("# Value")]
    [SerializeField, ReadOnly] private Vector2 _direction;
    [SerializeField, ReadOnly] private float _speed;
    
    #region Getter
    
    public Vector2 Direction => _direction;
    
    public float Speed => _speed;
    
    #endregion
    
    // ---------------------------------------------------------------------------------

    public MoveContext Clone() => new MoveContext()
    {
        // # Lock
        _isLockHorizontal = _isLockHorizontal,
        _isLockVertical = _isLockVertical,
        
        // # Anim
        _isAnimUse = _isAnimUse,
        _keyAnimName = _keyAnimName,
        
        // # Flag
        _isInputUse = _isInputUse,
        _isMoveAble = _isMoveAble,
        
        // # Value
        _direction = _direction,
        _speed = _speed,
    };
    
    // ---------------------------------------------------------------------------------

    private readonly Action _moveRequest;
    
    // ---------------------------------------------------------------------------------
    
    public MoveContext() {}
    
    public MoveContext(Action moveRequest)
    {
        _moveRequest = moveRequest;
    }
    
    // ---------------------------------------------------------------------------------
    
    public void Dispose()
    {
        
    }
    
    public void ApplyAsset(MoveAsset asset)
    {
        // # 기존에 입력 기능 사용 했다가 사용하지 않는 경우, 삭제 절차 진행
        if (_isInputUse && !asset.IsInputUse)
        {
            InputManager.Instance.OnActMoveFixedUpdate -= OnActMoveFixedUpdate;
        }
        
        // # 입력 기능시, 등록 절차 진행
        if (!_isInputUse && asset.IsInputUse)
        {
            InputManager.Instance.OnActMoveFixedUpdate += OnActMoveFixedUpdate;
        }
        
        _isInputUse = asset.IsInputUse;
        
        _isLockHorizontal = asset.IsLockHorizontal;
        _isLockVertical = asset.IsLockVertical;
        
        _isAnimUse = asset.IsAnimUse;
        _keyAnimName = asset.KeyAnimName;

        if (asset.IsStatUse)
        {
            
        }
        
        else
        {
            _speed = asset.DefaultSpeed;
        }
    }

    private void OnActMoveFixedUpdate(Vector2 direction, float duration)
    {
        if (!_isMoveAble)
        {
            return;
        }
        
        _direction = direction;

        if (_isLockHorizontal)
        {
            _direction.x = 0;
            _direction.y = _direction.y > 0 ? 1 : _direction.y < 0 ? -1 : 0;
        }
        
        if (_isLockVertical)
        {
            _direction.y = 0;
            _direction.x = _direction.x > 0 ? 1 : _direction.x < 0 ? -1 : 0;
        }

        _moveRequest?.Invoke();
    }
}