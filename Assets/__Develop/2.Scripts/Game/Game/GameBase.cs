using FrogLibrary;
using UnityEngine;

public abstract class GameBase : IdentifiedObject, IGame
{
    #region IGame implementation

    public IdentifiedObject Io => this;
    
    public abstract void Launch(IUser user);

    #endregion
}
