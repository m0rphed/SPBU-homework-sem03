namespace Task_01
{
    using System;

    public class Lazy<T> : ILazy<T>
    {
        private Func<T> supplierFunction;

        private bool isComputed = false;

        private T computationResult;

        public Lazy(Func<T> supplier)
        {
            supplierFunction = supplier;
        }

        public T Get()
        {
            if (isComputed) return computationResult;

            computationResult = supplierFunction();
            isComputed = true;

            return computationResult;
        }
    }
}