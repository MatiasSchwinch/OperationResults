using OperationResults.Tests.Common;

namespace OperationResults.Tests;

public class ErrorResultsShould
{
    [Fact]
    public void ReturnErrorResultWithMessage()
    {
        // Act
        var errorResult = OperationResult.Error("error has occurred");

        //Assert
        Assert.NotNull(errorResult);
        Assert.False(errorResult.IsSuccess);
        Assert.True(errorResult.IsFailure);
        Assert.Equal("error has occurred", errorResult.Message);
    }

    [Fact]
    public void ReturnErrorResultWithCustomError()
    {
        // Act
        var errorResult = OperationResult.Error(new CustomError());

        //Assert
        Assert.NotNull(errorResult);
        Assert.False(errorResult.IsSuccess);
        Assert.True(errorResult.IsFailure);
        Assert.Equal("title error", errorResult.Message);
        Assert.NotEmpty(errorResult.Errors);
        Assert.Equal("CustomError", errorResult.Errors[0].Type);
        Assert.Equal("www.fakelink.com", errorResult.Errors[0].Metadata["HelpLink"]);
    }

    [Fact]
    public void ReturnErrorResultWithException()
    {
        // Act
        var exception = new NotSupportedException("exception message");
        var errorResult = OperationResult.Error(exception);

        //Assert
        Assert.NotNull(errorResult);
        Assert.False(errorResult.IsSuccess);
        Assert.True(errorResult.IsFailure);
        Assert.Equal("exception message", errorResult.Message);
        Assert.NotEmpty(errorResult.Errors);
        Assert.Equal("NotSupportedException", errorResult.Errors[0].Type);
        Assert.Equal("An exception has occurred", errorResult.Errors[0].Title);
        Assert.Equal("exception message", errorResult.Errors[0].Details);
        Assert.NotEmpty(errorResult.Errors[0].Metadata);
    }

    [Fact]
    public void ReturnErrorResultWitGenericType()
    {
        // Act
        var errorResult = OperationResult.Error<object>("error has occurred");

        //Assert
        Assert.NotNull(errorResult);
        Assert.False(errorResult.IsSuccess);
        Assert.True(errorResult.IsFailure);
        Assert.Null(errorResult.Value);
        Assert.Equal("error has occurred", errorResult.Message);
    }
}
