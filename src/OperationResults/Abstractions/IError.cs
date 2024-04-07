namespace OperationResults.Abstractions;

public interface IError
{
    string Title { get; }
    string? Details { get; }
    string? Type { get; }
    Dictionary<string, object?> Metadata { get; }
}