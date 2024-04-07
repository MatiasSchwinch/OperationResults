namespace OperationResults;

public partial class OperationResult
{
    public static OperationResult Success() => new(true);
    public static OperationResult Success(string message) => new(true, message);
    public static async ValueTask<OperationResult> SuccessAsync(string message) => await ValueTask.FromResult(Success(message));

    public static OperationResult<TValue> Success<TValue>(TValue value) => new(value, true);
    public static OperationResult<TValue> Success<TValue>(TValue value, string message) => new(value, true, message);
    public static async ValueTask<OperationResult<TValue>> SuccessAsync<TValue>(TValue value, string message) => await ValueTask.FromResult(Success(value, message));
}
