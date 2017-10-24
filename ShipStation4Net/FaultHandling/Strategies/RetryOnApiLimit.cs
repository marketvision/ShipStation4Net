using ShipStation4Net.Exceptions;
using System;

namespace ShipStation4Net.FaultHandling.Strategies
{
    /// <summary>
    /// A retry strategy that handles ApiLimitReached exception & multiple retries
    /// </summary>
    public class RetryOnApiLimit : RetryStrategy
    {
        private readonly int retryCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryOnApiLimit"/> class.
        /// </summary>
        public RetryOnApiLimit()
            : this(DefaultClientRetryCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryOnApiLimit"/> class.
        /// </summary>
        /// <param name="retryCount">The number of retry attempts.</param>
        public RetryOnApiLimit(int retryCount)
            : this(null, retryCount)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryOnApiLimit"/> class.
        /// </summary>
        /// <param name="name">The retry strategy name.</param>
        /// <param name="retryCount">The number of retry attempts.</param>
        public RetryOnApiLimit(string name, int retryCount)
            : base(name, false)
        {
            this.retryCount = retryCount;
        }

        /// <summary>
        /// Returns the corresponding ShouldRetry delegate.
        /// </summary>
        /// <returns>The ShouldRetry delegate.</returns>
        public override ShouldRetry GetShouldRetry()
        {
            if (this.retryCount == 0)
            {
                return delegate (int currentRetryCount, Exception lastException, out TimeSpan interval)
                {
                    interval = TimeSpan.Zero;
                    return false;
                };
            }

            return delegate (int currentRetryCount, Exception lastException, out TimeSpan interval)
            {
                if (currentRetryCount < this.retryCount
                    && lastException != null
                    && lastException is ApiLimitReachedException)
                {
                    var exception = (ApiLimitReachedException)lastException;

                    interval = TimeSpan.FromSeconds(exception.RemainingSecondsBeforeReset + 3);
                    return true;
                }

                interval = TimeSpan.Zero;
                return false;
            };
        }
    }
}