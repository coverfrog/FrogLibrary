using UnityEngine;

[CreateAssetMenu]
public class MoveAsset : IdentifiedObject
{
    [Header("# Move")]
    [SerializeField] private bool _isInputUse;
    
    public bool IsInputUse => _isInputUse;

    // # IsStatUse
    //
    // 스탯 사용 여부

    [SerializeField] private bool _isStatUse = true;
    
    public bool IsStatUse => _isStatUse;

    // # StatCodeName
    //
    // 스탯 사용시 스탯 코드 네임

    [SerializeField] private string _statCodeName = "MoveSpeed";
    
    public string StatCodeName => _statCodeName;
}