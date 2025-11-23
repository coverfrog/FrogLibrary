using UnityEngine;

[CreateAssetMenu]
public class SkillAsset : IdentifiedObject
{
    [Header("# Input")]
    [SerializeField] private bool _isInputUse;
    [SerializeField] private int _slotIndex = -1;
    
    #region Getter

    public bool IsInputUse => _isInputUse;
    
    public int SlotIndex => _slotIndex;
    
    #endregion
    
    [Header("# Type")]
    [SerializeField] private SkillControlType _controlType;
    [SerializeField] private SkillInteractType _interactType;
    
    #region Getter
    
    public SkillControlType ControlType => _controlType;
    
    public SkillInteractType InteractType => _interactType;
    
    #endregion
}