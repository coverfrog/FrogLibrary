using UnityEngine;

public class ReadOnlyAttribute : PropertyAttribute
{
    public readonly bool onlyInPlayMode;

    public ReadOnlyAttribute(bool onlyInPlayMode = false)
    {
        this.onlyInPlayMode = onlyInPlayMode;
    }
}