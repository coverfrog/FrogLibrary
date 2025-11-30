using UnityEngine;

namespace FrogLibrary
{
    public interface IDataTable<out T>
    {
        T GetData();
    }
}
