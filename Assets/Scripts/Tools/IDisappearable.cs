public interface IDisappearable
{
    event System.Action<IDisappearable> OnDisappeared;
    void Disappear();
}