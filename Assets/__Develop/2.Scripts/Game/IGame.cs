using FrogLibrary;
using UnityEngine;

public interface IGame
{
    IdentifiedObject Io { get; }
    
    void Launch(IUser user);
}
