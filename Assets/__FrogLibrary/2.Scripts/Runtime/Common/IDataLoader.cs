using System;
using Cysharp.Threading.Tasks;

namespace FrogLibrary
{
    public interface IDataLoader
    {
        UniTask LoadData(Action<float> onProgress, Action onComplete);
    }
}