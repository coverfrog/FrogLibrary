using System;
using System.Collections.Generic;
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
    
    [Header("# Stat")]
    [SerializeField, ReadOnly] private bool _isStatUse = true;
    [SerializeField, ReadOnly] private string _statCodeNameMoveSpeed = "MoveSpeed";
    [SerializeField, ReadOnly] private string _statCodeNameJumpForce = "JumpForce";
    
    [Header("# Anim")] 
    [SerializeField, ReadOnly] private bool _isAnimUse = true;
    [SerializeField, ReadOnly] private string _keyAnimMove = "IsMove";
    [SerializeField, ReadOnly] private string _keyAnimJump = "Jump";

    #region Getter

    public bool IsAnimUse => _isAnimUse;
    public string KeyAnimMove => _keyAnimMove;
    public string keyAnimJump => _keyAnimJump;

    #endregion

    [Header("# Input")]
    [SerializeField, ReadOnly] private bool _isInputUse;
    
    [Header("# Flag")]
    [SerializeField, ReadOnly] private bool _isMoveAble;
    [SerializeField, ReadOnly] private bool _isJumpAble;
    [SerializeField, ReadOnly] private bool _isGrounded;

    #region Setter

    public void SetIsMoveAble(MoveHelper _, bool isMoveAble) => _isMoveAble = isMoveAble;
    public void SetIsJumpAble(MoveHelper _, bool isJumpAble) => _isJumpAble = isJumpAble;
    public void SetIsGrounded(MoveHelper _, bool isGrounded) => _isGrounded = isGrounded;

    #endregion

    [Header("# Ground")] 
    [SerializeField, ReadOnly] private LayerMask _groundLayer;
    
    #region Getter
    
    public LayerMask GroundLayer => _groundLayer;
    
    #endregion
    
    [Header("# Value")]
    [SerializeField] private Vector2 _direction;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;

    public RaycastHit2D[] GroundHitList = new RaycastHit2D[50];
    
    #region Getter
    
    public Vector2 Direction => _direction;
    
    public float MoveSpeed => _moveSpeed;
    
    public float JumpForce => _jumpForce;
    
    #endregion
    
    // ---------------------------------------------------------------------------------

    public MoveContext Clone() => new MoveContext()
    {
        // # Lock
        _isLockHorizontal = _isLockHorizontal,
        _isLockVertical = _isLockVertical,
        
        // # Stat
        _isStatUse = _isStatUse,
        _statCodeNameMoveSpeed = _statCodeNameMoveSpeed,
        _statCodeNameJumpForce = _statCodeNameJumpForce,
        
        // # Anim
        _isAnimUse = _isAnimUse,
        _keyAnimMove = _keyAnimMove,
        _keyAnimJump = _keyAnimJump,
        
        // # Input
        _isInputUse = _isInputUse,
        
        // # Flag
        _isMoveAble = _isMoveAble,
        _isJumpAble = _isJumpAble,
        _isGrounded = _isGrounded,
        
        // # Ground
        _groundLayer = _groundLayer,
        
        // # Value
        _direction = _direction,
        _moveSpeed = _moveSpeed,
        _jumpForce = _jumpForce,
    };
    
    // ---------------------------------------------------------------------------------

    private readonly Action _moveRequest;
    private readonly Action _jumpRequest;
    private readonly Action<string, Action<float>> _statUpdateRequest;
    
    // ---------------------------------------------------------------------------------
    
    public MoveContext() {}
    
    public MoveContext(Action moveRequest, Action jumpRequest, Action<string, Action<float>> statUpdateRequest)
    {
        _statUpdateRequest = statUpdateRequest;
        _jumpRequest = jumpRequest;
        _moveRequest = moveRequest;
    }
    
    // ---------------------------------------------------------------------------------
    
    public void Dispose()
    {
        
    }
    
    // ---------------------------------------------------------------------------------
    
    public void ApplyAsset(MoveAsset asset)
    {
        // # Lock
        _isLockHorizontal = asset.IsLockHorizontal;
        _isLockVertical = asset.IsLockVertical;
        
        // # Stat
        _moveSpeed = asset.DefaultMoveSpeed;
        _jumpForce = asset.DefaultJumpForce;
        
        _statCodeNameMoveSpeed = asset.StatCodeNameMoveSpeed;
        _statCodeNameJumpForce = asset.StatCodeNameJumpForce;
        
        if (asset.IsStatUse)
        {
            _statUpdateRequest?.Invoke(_statCodeNameMoveSpeed, v => _moveSpeed = v);
            _statUpdateRequest?.Invoke(_statCodeNameJumpForce, v => _jumpForce = v);
        }
        
        // # Anim
        _isAnimUse = asset.IsAnimUse;
        _keyAnimMove = asset.KeyAnimMove;
        _keyAnimJump = asset.KeyAnimJump;
        
        // # Input
        // # 기존에 입력 기능 사용 했다가 사용하지 않는 경우, 삭제 절차 진행
        if (_isInputUse && !asset.IsInputUse)
        {
            InputManager.Instance.OnActMoveFixedUpdate -= OnActMoveFixedUpdate;
            InputManager.Instance.OnActJump -= OnActJump;
        }
        
        // # 입력 기능시, 등록 절차 진행
        if (!_isInputUse && asset.IsInputUse)
        {
            InputManager.Instance.OnActMoveFixedUpdate += OnActMoveFixedUpdate;
            InputManager.Instance.OnActJump += OnActJump;
        }
        
        _isInputUse = asset.IsInputUse;
        
        // # Ground
        _groundLayer = asset.GroundLayer;
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

    private void OnActJump(bool isClick)
    {
        if (!_isJumpAble)
        {
            return;
        }

        if (!_isGrounded)
        {
            return;
        }
        
        _jumpRequest?.Invoke();
    }
}