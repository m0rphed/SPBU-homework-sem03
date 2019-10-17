namespace Task_01
{
    using System;

    /// <summary>
    /// Factory for creating lazy objects in single-threaded and multi-threaded mode.
    /// </summary>
    /// <typeparam name="T">Type of returning object.</typeparam>
    public class LazyFactory<T>
    {
        /// <summary>
        /// Creates a lazy object based on calculation with a guarantee of correct operation in single-threaded mode.
        /// </summary>
        /// <param name="supplier">Calculation providing as an object.</param>
        /// <returns>New instance of calculation in single-threaded mode.</returns>
        public static ILazy<T> CreateLazy(Func<T> supplier) => new Lazy<T>(supplier);

        /// <summary>
        /// Creates a lazy object based on calculation with the guarantee of correct operation in multi-threaded mode.
        /// </summary>
        /// <param name="supplier">Calculation providing as an object.</param>
        /// <returns>New instance of calculation in multi-threaded mode.</returns>
        public static ILazy<T> CreateLazyConcurrent(Func<T> supplier) => new LazyConcurrent<T>(supplier);
    }
}