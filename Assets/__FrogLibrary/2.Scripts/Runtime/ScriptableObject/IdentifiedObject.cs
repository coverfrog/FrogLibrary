using System;
using System.Collections.Generic;
using UnityEngine;

namespace FrogLibrary
{
    [CreateAssetMenu(menuName = "FrogLibrary/IdentifiedObject", fileName = "IdentifiedObject")]
    public class IdentifiedObject : ScriptableObject, ICloneable
    {
        [Header("# Info")]
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _codename;
        [SerializeField] private string _displayName;
        [SerializeField, TextArea] private string _description;

        #region Getter

        public Sprite Icon => _icon;

        public string Codename => _codename;

        public string DisplayName => _displayName;

        public string Description => _description;

        #endregion

        #region Clone

        public virtual object Clone()
        {
            return Instantiate(this);
        }

        #endregion
    }
}