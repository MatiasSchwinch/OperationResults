namespace OperationResults;

public class RetryError : Error
{
    public RetryError(int currentRetry, Exception exception) : base(exception)
    {
        Title = $"Attempt number {currentRetry} has failed";
    }
}
