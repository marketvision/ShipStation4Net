using System;

namespace ShipStation4Net.FaultHandling
{
    /// <summary>
    /// Defines a callback delegate that will be invoked whenever a retry condition is encountered.
    /// </summary>
    /// <param name="retryCount">The current retry attempt count.</param>
    /// <param name="lastException">The exception which caused the retry conditions to occur.</param>
    /// <param name="delay">The delay indicating how long the current thread will be suspended for before the next iteration will be invoked.</param>
    /// <returns>Returns a callback delegate that will be invoked whenever to retry should be attempt.</returns>
    public delegate bool ShouldRetry(int retryCount, Exception lastException, out TimeSpan delay);

    /// <summary>
    /// Represents a retry strategy that determines how many times should be retried and the interval between retries.
    /// </summary>
    public abstract class RetryStrategy
    {
        #region Public members
        /// <summary>
        /// The default number of retry attempts.
        /// </summary>
        public static readonly int DefaultClientRetryCount = 5;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="RetryStrategy"/> class.
        /// </summary>
        /// <param name="name">The name of the retry strategy.</param>
        /// <param name="firstFastRetry">a value indicating whether or not the very first retry attempt will be made immediately
        /// whereas the subsequent retries will remain subject to retry interval.</param>
        protected RetryStrategy(string name, bool firstFastRetry)
        {
            this.Name = name;
            this.FastFirstRetry = firstFastRetry;
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the very first retry attempt will be made immediately
        /// whereas the subsequent retries will remain subject to retry interval.
        /// </summary>
        public bool FastFirstRetry { get; set; }

        /// <summary>
        /// Gets the name of the retry strategy.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Returns the corresponding ShouldRetry delegate.
        /// </summary>
        /// <returns>The ShouldRetry delegate.</returns>
        public abstract ShouldRetry GetShouldRetry();
    }
}