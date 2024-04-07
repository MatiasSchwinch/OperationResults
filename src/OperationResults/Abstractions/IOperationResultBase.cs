namespace OperationResults.Abstractions
{
    public interface IOperationResultBase
    {
        bool IsSuccess { get; init; }
        bool IsFailure { get; }
        string? Message { get; set; }
        IList<IError> Errors { get; }

        bool HasErrors();
        bool HasSuccess();
    }

    public interface IOperationResultBase<TValue> : IOperationResultBase
    {
        TValue? Value { get; init; }
    }
}