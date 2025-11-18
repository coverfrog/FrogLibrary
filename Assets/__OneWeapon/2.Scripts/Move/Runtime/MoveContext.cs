using System;
using UnityEngine;

[Serializable]
public class MoveContext : IDisposable
{
    // #

    [SerializeField, ReadOnly] private bool _isMoveAble;
    
    public void SetIsMoveAble(MoveHelper _, bool isMoveAble) => _isMoveAble = isMoveAble;
    
    // #
    
    [SerializeField, ReadOnly] private bool _isInputUse;
    
    // #

    [SerializeField, ReadOnly] private bool _isLockHorizontal;
    
    // #
    
    [SerializeField, ReadOnly] private bool _isLockVertical;
    
    // #
    
    [SerializeField, ReadOnly] private Vector2 _direction;
    
    public void SetDirection(MoveHelper _, Vector2 direction) => _direction = direction;
    
    // #
    
    [SerializeField, ReadOnly] private float _duration;
    
    public void SetDuration(float duration) => _duration = duration;
    
    // #
    
    private MoveHelper Helper { get; }
    
    // #
    
    public MoveContext()
    {
        
    }

    public MoveContext(MoveHelper helper)
    {
        Helper = helper;
    }
    
    // #
    
    public void Dispose()
    {
        
    }
    
    // #

    public void ApplyAsset(MoveAsset asset)
    {
        // # 기존에 입력 기능 사용 했다가 사용하지 않는 경우, 삭제 절차 진행
        if (_isInputUse && !asset.IsInputUse)
        {
            InputManager.Instance.OnInputMove -= OnInput;
        }
        
        // # 입력 기능시, 등록 절차 진행
        if (!_isInputUse && asset.IsInputUse)
        {
            InputManager.Instance.OnInputMove += OnInput;
        }
        
        _isInputUse = asset.IsInputUse;
        
        _isLockHorizontal = asset.IsLockHorizontal;
        _isLockVertical = asset.IsLockVertical;
    }

    private void OnInput(Vector2 direction, float duration)
    {
        if (!_isMoveAble)
        {
            return;
        }
        
        _direction = direction;
        _duration = duration;

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
        
        // # 커맨드 전달
        
        var cmd = new MoveCommand(Helper, direction, 3.0f);
        CommandManager.Instance.Execute(cmd);
    }
}