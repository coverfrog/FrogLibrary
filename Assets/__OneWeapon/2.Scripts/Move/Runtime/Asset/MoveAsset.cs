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
    [SerializeField] private string _statCodeName = "MoveSpeed";
    [SerializeField] private float _defaultSpeed = 3.0f;
    
    #region Getter

    public bool IsStatUse => _isStatUse;
    public string StatCodeName => _statCodeName;
    public float DefaultSpeed => _defaultSpeed;

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
    [SerializeField] private string _keyAnimName = "IsMove";
    
    #region Getter

    public bool IsAnimUse => _isAnimUse;
    
    public string KeyAnimName => _keyAnimName;

    #endregion

}