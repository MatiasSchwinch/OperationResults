using OperationResults.Abstractions;

namespace OperationResults;

public partial class OperationResult
{
    public static OperationResult Try(Action action)
    {
		try
		{
			action();
			return Success();
		}
		catch (Exception ex)
		{
			return Error(ex);
		}
    }

    public static OperationResult<TValue> Try<TValue>(Func<TValue> action)
    {
        try
        {
            return Success(action());
        }
        catch (Exception ex)
        {
            return Error<TValue>(ex);
        }
    }

    public static Task<OperationResult> TryAsync(Func<Task> action) =>
        HandleRetriesAsync(action, new RetryOptions());

    public static Task<OperationResult<TValue>> TryAsync<TValue>(Func<Task<TValue>> action) =>
        HandleRetriesAsync(action, new RetryOptions());

    public static Task<OperationResult> TryAsync(Func<Task> action, RetryOptions retryOptions) =>
        HandleRetriesAsync(action, retryOptions);

    public static Task<OperationResult<TValue>> TryAsync<TValue>(Func<Task<TValue>> action, RetryOptions retryOptions) =>
        HandleRetriesAsync(action, retryOptions);

    private static async Task<OperationResult> HandleRetriesAsync(Func<Task> action, RetryOptions retryOptions)
    {
        var errors = new List<IError>();

        for (int currentRetry = 0; currentRetry < retryOptions.MaxRetries; currentRetry++)
        {
            try
            {
                await action();
                return Success();
            }
            catch (Exception ex)
            {
                errors.Add(new RetryError(currentRetry + 1, ex));
            }

            await Task.Delay(retryOptions.DelayBetweenRetries);
        }

        return Error(errors);
    }

    private static async Task<OperationResult<TValue>> HandleRetriesAsync<TValue>(Func<Task<TValue>> action, RetryOptions retryOptions)
    {
        var errors = new List<IError>();

        for (int currentRetry = 0; currentRetry < retryOptions.MaxRetries; currentRetry++)
        {
            try
            {
                return Success(await action());
            }
            catch (Exception ex)
            {
                errors.Add(new RetryError(currentRetry + 1, ex));
            }

            await Task.Delay(retryOptions.DelayBetweenRetries);
        }

        return Error<TValue>(errors);
    }
}
