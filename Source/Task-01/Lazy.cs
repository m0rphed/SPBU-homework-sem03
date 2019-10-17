namespace Task_01
{
    using System;

    /// <inheritdoc />
    public class Lazy<T> : ILazy<T>
    {
        private Func<T> _supplierFunction;

        private bool _isComputed;

        private T _computationResult;

        /// <summary>
        /// Initializes a new instance of the <see cref="Lazy{T}"/> class.
        /// </summary>
        /// <param name="supplier">Calculation providing as an object.</param>
        public Lazy(Func<T> supplier) => _supplierFunction = supplier;

        /// <inheritdoc />
        public T Get()
        {
            if (_isComputed)
            {
                return _computationResult;
            }

            _computationResult = _supplierFunction();
            _supplierFunction = null;
            _isComputed = true;

            return _computationResult;
        }
    }
}