using UnityEngine;

[CreateAssetMenu]
public class MoveAsset : IdentifiedObject
{
    [Header("# Input")]
    [SerializeField] private bool _isInputUse;

    #region Getter

    public bool IsInputUse => _isInputUse;

    #endregion

    [Header("# Stat")]
    [SerializeField] private bool _isStatUse = true;
    [SerializeField] private string _statCodeNameMoveSpeed = "MoveSpeed";
    [SerializeField] private string _statCodeNameJumpForce = "JumpForce";
    
    #region Getter

    public bool IsStatUse => _isStatUse;
    public string StatCodeNameMoveSpeed => _statCodeNameMoveSpeed;
    
    public string StatCodeNameJumpForce => _statCodeNameJumpForce;

    #endregion
    
    [Header("# Default")]
    [SerializeField] private float _defaultMoveSpeed = 3.0f;
    [SerializeField] private float _defaultJumpForce = 10.0f;
    
    #region Getter
    public float DefaultMoveSpeed => _defaultMoveSpeed;

    public float DefaultJumpForce => _defaultJumpForce;
    
    #endregion
    
    [Header("# Lock")]
    [SerializeField] private bool _isLockHorizontal;
    [SerializeField] private bool _isLockVertical = true;
    
    #region Getter

    public bool IsLockHorizontal => _isLockHorizontal;
    
    public bool IsLockVertical => _isLockVertical;

    #endregion

    [Header("# Anim")] 
    [SerializeField] private bool _isAnimUse = true;
    [SerializeField] private string _keyAnimMove = "IsMove";
    [SerializeField] private string _keyAnimJump = "Jump";
    
    #region Getter

    public bool IsAnimUse => _isAnimUse;
    public string KeyAnimMove => _keyAnimMove;
    public string KeyAnimJump => _keyAnimJump;

    #endregion
    
    [Header("# Ground")] 
    [SerializeField] private LayerMask _groundLayer;

    #region Getter

    public LayerMask GroundLayer => _groundLayer;

    #endregion
}