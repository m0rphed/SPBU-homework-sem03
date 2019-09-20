namespace Task_01
{
    using System;

    public class LazyConcurrent<T> : ILazy<T>
    {
        /// <summary>
        /// Computation function
        /// </summary>
        private volatile Func<T> supplierFunction;

        /// <summary>
        /// Object used by lock
        /// </summary>
        private object _lockObject = new object();
        
        /// <summary>
        /// If false then <see cref="supplierFunction"/>
        /// will be called in <see cref="Get"/>
        /// </summary>
        private volatile bool _isComputed = false;

        /// <summary>
        /// Calculated result of the supplier
        /// </summary>
        private T computationResult;

        public LazyConcurrent(Func<T> supplier)
        {
            supplierFunction = supplier;
        }

        public T Get()
        {
            if (_isComputed) return computationResult;

            lock (_lockObject)
            {
                if (_isComputed) return computationResult;

                computationResult = supplierFunction();
                _isComputed = true;
            }

            return computationResult;
        }
    }
}
