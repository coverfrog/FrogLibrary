public interface IObserver<T>
{
    void OnNotify(in T t);
}