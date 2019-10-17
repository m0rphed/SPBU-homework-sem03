namespace Task_01
{
    /// <summary>
    /// Lazy Computing Interface.
    /// </summary>
    /// <typeparam name="T">Type of returning object.</typeparam>
    public interface ILazy<out T>
    {
        T Get();
    }
}