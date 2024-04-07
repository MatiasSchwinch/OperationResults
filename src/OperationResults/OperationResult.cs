namespace OperationResults;

public partial class OperationResult : OperationResultBase
{
    public OperationResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
        Message = ToString();
    }

    public OperationResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
}

public class OperationResult<TValue> : OperationResultBase<TValue>
{
    //public static OperationResult<TValue> Invalid => OperationResult.Error<TValue>("");

    public OperationResult(TValue? value, bool isSucces)
    {
        Value = value;
        IsSuccess = isSucces;
        Message = ToString();
    }

    public OperationResult(TValue? value, bool isSucces, string message)
    {
        Value = value;
        IsSuccess = isSucces;
        Message = message;
    }
}
