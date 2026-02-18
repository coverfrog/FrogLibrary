namespace FrogLibrary
{
    public interface IDeepClone<out T>
    {
        T DeepClone();
    }
}