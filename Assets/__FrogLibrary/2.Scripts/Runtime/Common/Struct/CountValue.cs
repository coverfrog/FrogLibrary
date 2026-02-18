using System;

namespace FrogLibrary
{
    [Serializable]
    public class CountValue<T>
    {
        public T value;
        public int count;
    }
}