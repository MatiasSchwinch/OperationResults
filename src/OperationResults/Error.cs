using OperationResults.Abstractions;

namespace OperationResults;

public class Error : IError
{
    private readonly Type? _errorType;

    public string Type
    {
        get
        {
            if (_errorType is null)
            {
                return GetType().Name;
            }

            return _errorType.Name;
        }
    }

    public string Title { get; protected set; }
    public string? Details { get; protected set; }
    public Dictionary<string, object?> Metadata { get; }

    public Error(string title)
    {
        Title = title;
        Metadata = new Dictionary<string, object?>();
    }

    public Error(string title, string details) : this(title)
    {
        Details = details;
    }

    public Error(Exception exception) : this("An exception has occurred", exception.Message)
    {
        _errorType = exception.GetType();
        Metadata.TryAdd("Type", exception.GetType().FullName);
        Metadata.TryAdd("StackTrace", exception.StackTrace);
        Metadata.TryAdd("HResult", exception.HResult);
        Metadata.TryAdd("Source", exception.Source);

        if (exception.InnerException is not null)
            Metadata.TryAdd("InnerException", new Error(exception.InnerException));
    }

    public IError AddMetadata(string key, object? value)
    {
        Metadata.TryAdd(key, value);
        return this;
    }
}
