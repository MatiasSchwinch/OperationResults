using OperationResults.Abstractions;

namespace OperationResults;

public abstract class OperationResultBase : IOperationResultBase
{
    public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public string? Message { get; set; }
    public IList<IError> Errors { get; }

    protected OperationResultBase()
    {
        Errors = new List<IError>();
    }

    public bool HasSuccess() => IsSuccess;
    public bool HasErrors() => Errors.Any();

    public override string ToString()
    {
        return $"The operation result was {(IsSuccess ? "successful" : "unsuccessful")}";
    }
}

public abstract class OperationResultBase<TValue> : OperationResultBase, IOperationResultBase<TValue>
{
    public TValue? Value { get; init; }

    public override string ToString()
    {
        return $"The operation result was {(IsSuccess ? "successful" : "unsuccessful")}, {(Value is not null ? "and set value" : "and not set value")}.";
    }
}
