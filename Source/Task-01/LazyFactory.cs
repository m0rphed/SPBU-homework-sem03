namespace Task_01
{
    using System;

    public class LazyFactory<T>
    {
        public static ILazy<T> CreateLazy(Func<T> supplier) => new Lazy<T>(supplier);

        public static ILazy<T> CreateLazyConcurrent(Func<T> supplier) => new LazyConcurrent<T>(supplier);
    }
}