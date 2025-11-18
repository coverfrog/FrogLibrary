using System;
using UnityEngine;

[Serializable]
public class MoveContext : IDisposable
{
    [SerializeField] private bool _isInputUse;
    
    public void SetInput(MoveHelper _, bool isInputUse) => _isInputUse = isInputUse;
    
    // #
    
    [SerializeField] private Vector2 _direction;
    
    public void SetDirection(MoveHelper _, Vector2 direction) => _direction = direction;
    
    // #
    
    public MoveContext()
    {
        
    }

    public MoveContext(MoveHelper helper)
    {
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
            
        }
        
        // # 입력 기능시, 등록 절차 진행
        if (!_isInputUse && asset.IsInputUse)
        {
            
        }
        
        _isInputUse = asset.IsInputUse;

      
    }

}