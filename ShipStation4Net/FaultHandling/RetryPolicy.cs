using ShipStation4Net.FaultHandling.Strategies;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShipStation4Net.FaultHandling
{

    /// <summary>
    /// Provides the base implementation of the retry mechanism for unreliable actions and transient conditions.
    /// </summary>
    public class RetryPolicy
    {
        /// <summary>
        /// Returns a default policy that does no retries, it just invokes action exactly once.
        /// </summary>
        public static readonly RetryPolicy NoRetry = new RetryPolicy(new TransientErrorIgnoreStrategy(), new RetryOnApiLimit(0));

        /// <summary>
        /// Returns a default policy that implements a fixed retry interval configured with the default <see cref="FixedInterval"/> retry strategy.
        /// The default retry policy treats all caught exceptions as transient errors.
        /// </summary>
        public static readonly RetryPolicy RetryOnApiLimit = new RetryPolicy(new TransientErrorCatchAllStrategy(), new RetryOnApiLimit());

        /// <summary>
        /// Initializes a new instance of the RetryPolicy class with the specified number of retry attempts and parameters defining the progressive delay between retries.
        /// </summary>
        /// <param name="errorDetectionStrategy">The <see cref="ITransientErrorDetectionStrategy"/> that is responsible for detecting transient conditions.</param>
        /// <param name="retryStrategy">The retry strategy to use for this retry policy.</param>
        public RetryPolicy(ITransientErrorDetectionStrategy errorDetectionStrategy, RetryStrategy retryStrategy)
        {
            this.ErrorDetectionStrategy = errorDetectionStrategy;

            if (errorDetectionStrategy == null)
            {
                throw new InvalidOperationException("The error detection strategy type must implement the ITransientErrorDetectionStrategy interface.");
            }

            this.RetryStrategy = retryStrategy;
        }

        /// <summary>
        /// An instance of a callback delegate that will be invoked whenever a retry condition is encountered.
        /// </summary>
        public event EventHandler<RetryingEventArgs> Retrying;

        /// <summary>
        /// Gets the retry strategy.
        /// </summary>
        public RetryStrategy RetryStrategy { get; private set; }

        /// <summary>
        /// Gets the instance of the error detection strategy.
        /// </summary>
        public ITransientErrorDetectionStrategy ErrorDetectionStrategy { get; private set; }

        /// <summary>
        /// Repetitively executes the specified action while it satisfies the current retry policy.
        /// </summary>
        /// <param name="action">A delegate representing the executable action which doesn't return any results.</param>
        public async virtual void ExecuteAction(Task action)
        {
            await this.ExecuteAction(async () => { await action; return action; });
        }

        /// <summary>
        /// Repetitively executes the specified action while it satisfies the current retry policy.
        /// </summary>
        /// <typeparam name="TResult">The type of result expected from the executable action.</typeparam>
        /// <param name="func">A delegate representing the executable action which returns the result of type R.</param>
        /// <returns>The result from the action.</returns>
        public async virtual Task<TResult> ExecuteAction<TResult>(Func<Task<TResult>> func)
        {
            int retryCount = 0;
            TimeSpan delay = TimeSpan.Zero;
            Exception lastError;

            var shouldRetry = this.RetryStrategy.GetShouldRetry();

            for (; ; )
            {
                lastError = null;

                try
                {
                    return await func();
                }
                catch (Exception ex)
                {
                    lastError = ex;

                    if (!(this.ErrorDetectionStrategy.IsTransient(lastError) && shouldRetry(retryCount++, lastError, out delay)))
                    {
                        throw;
                    }
                }

                // Perform an extra check in the delay interval. Should prevent from accidentally ending up with the value of -1 that will block a thread indefinitely.
                // In addition, any other negative numbers will cause an ArgumentOutOfRangeException fault that will be thrown by Thread.Sleep.
                if (delay.TotalMilliseconds < 0)
                {
                    delay = TimeSpan.Zero;
                }

                this.OnRetrying(retryCount, lastError, delay);

                if (retryCount > 1 || !this.RetryStrategy.FastFirstRetry)
                {
                    Thread.Sleep(delay);
                }
            }
        }

        /// <summary>
        /// Notifies the subscribers whenever a retry condition is encountered.
        /// </summary>
        /// <param name="retryCount">The current retry attempt count.</param>
        /// <param name="lastError">The exception which caused the retry conditions to occur.</param>
        /// <param name="delay">The delay indicating how long the current thread will be suspended for before the next iteration will be invoked.</param>
        protected virtual void OnRetrying(int retryCount, Exception lastError, TimeSpan delay)
        {
            if (this.Retrying != null)
            {
                this.Retrying(this, new RetryingEventArgs(retryCount, delay, lastError));
            }
        }
    }
}
