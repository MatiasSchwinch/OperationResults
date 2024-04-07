using OperationResults.Abstractions;

namespace OperationResults;

public partial class OperationResult
{
    public static OperationResult Error(string message) => new(false, message);

    public static OperationResult Error(IError error)
    {
        var errorResult = Error(error.Title);
        errorResult.Errors.Add(error);

        return errorResult;
    }

    public static OperationResult Error(IList<IError> errors)
    {
        var errorResult = Error(errors.Count > 1
            ? "Multiple errors have occurred"
            : errors.FirstOrDefault()!.Title);

        foreach (var error in errors)
        {
            errorResult.Errors.Add(error);
        }

        return errorResult;
    }

    public static OperationResult Error(Exception exception)
    {
        var errorResult = Error(exception.Message);
        errorResult.Errors.Add(new Error(exception));

        return errorResult;
    }

    public static OperationResult<TValue> Error<TValue>(string message) => new(default, false, message);

    public static OperationResult<TValue> Error<TValue>(IError error)
    {
        var errorResult = Error<TValue>(error.Title);
        errorResult.Errors.Add(error);

        return errorResult;
    }

    public static OperationResult<TValue> Error<TValue>(IList<IError> errors)
    {
        var errorResult = Error<TValue>(errors.Count > 1
            ? "Multiple errors have occurred"
            : errors.FirstOrDefault()!.Title);

        foreach (var error in errors)
        {
            errorResult.Errors.Add(error);
        }

        return errorResult;
    }

    public static OperationResult<TValue> Error<TValue>(Exception exception)
    {
        var errorResult = Error<TValue>(exception.Message);
        errorResult.Errors.Add(new Error(exception));

        return errorResult;
    }
}
