namespace OperationResults;

public class RetryOptions
{
    public int MaxRetries { get; init; }
    public TimeSpan DelayBetweenRetries { get; init; }

    public RetryOptions()
    {
        MaxRetries = 1;
        DelayBetweenRetries = TimeSpan.Zero;
    }

    public RetryOptions(int maxRetries, TimeSpan delayBetweenRetries)
    {
        MaxRetries = maxRetries;
        DelayBetweenRetries = delayBetweenRetries;
    }
}
