namespace OperationResults.Tests.Common;

public class CustomError : Error
{
    public CustomError() : base("title error", "details error")
    {
        Metadata.Add("HelpLink", "www.fakelink.com");
    }
}
