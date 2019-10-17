namespace Task_01
{
    using System;

    /// <inheritdoc />
    public class LazyConcurrent<T> : ILazy<T>
    {
        /// <summary>
        /// Computation function.
        /// </summary>
        private volatile Func<T> _supplierFunction;

        /// <summary>
        /// Object used by lock.
        /// </summary>
        private readonly object _lockObject = new object();

        /// <summary>
        /// If false then <see cref="_supplierFunction"/>
        /// will be called in <see cref="Get"/>.
        /// </summary>
        private volatile bool _isComputed = false;

        /// <summary>
        /// Calculated result of the supplier.
        /// </summary>
        private T _computationResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyConcurrent{T}"/> class.
        /// </summary>
        /// <param name="supplier">Calculation providing as an object.</param>
        public LazyConcurrent(Func<T> supplier) => _supplierFunction = supplier;

        /// <inheritdoc />
        public T Get()
        {
            if (_isComputed)
            {
                return _computationResult;
            }

            lock (_lockObject)
            {
                if (_isComputed)
                {
                    return _computationResult;
                }

                _computationResult = _supplierFunction();
                _supplierFunction = null;
                _isComputed = true;
            }

            return _computationResult;
        }
    }
}
