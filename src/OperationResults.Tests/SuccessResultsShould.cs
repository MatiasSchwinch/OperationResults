using OperationResults.Tests.Common;

namespace OperationResults.Tests;

public class SuccessResultsShould
{
    [Fact]
    public void ReturnSuccessResultWithoutMessage()
    {
        // Act
        var successResult = OperationResult.Success();

        //Assert
        Assert.NotNull(successResult);
        Assert.True(successResult.IsSuccess);
        Assert.Equal("The operation result was successful", successResult.Message);
    }

    [Fact]
    public void ReturnSuccessResultWitMessage()
    {
        // Act
        var successResult = OperationResult.Success("Message text");

        //Assert
        Assert.NotNull(successResult);
        Assert.True(successResult.IsSuccess);
        Assert.Equal("Message text", successResult.Message);
    }

    [Fact]
    public async void ReturnSuccessResultAsyncWitMessage()
    {
        // Act
        var successResult = await OperationResult.SuccessAsync("Message text");

        //Assert
        Assert.NotNull(successResult);
        Assert.True(successResult.IsSuccess);
        Assert.Equal("Message text", successResult.Message);
    }

    [Fact]
    public void ReturnSuccessResultWitValueAndMessage()
    {
        // Act
        var fakeUserObject = new FakePersonEntity("John", "Doe");

        var successResult = OperationResult.Success(fakeUserObject, "Message text");

        //Assert
        Assert.NotNull(successResult);
        Assert.True(successResult.IsSuccess);
        Assert.Equal("Message text", successResult.Message);
        Assert.NotNull(successResult.Value);
        Assert.Equal("John", successResult.Value.FirstName);
        Assert.Equal("Doe", successResult.Value.LastName);
    }
}
