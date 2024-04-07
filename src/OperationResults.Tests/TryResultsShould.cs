using OperationResults.Tests.Common;

namespace OperationResults.Tests;

public class TryResultsShould
{
    [Fact]
    public void SuccessfulResultExecutingAction()
    {
        //Act
        var result = OperationResult.Try(() =>
        {
            return;
        });

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal("The operation result was successful", result.Message);
    }

    [Fact]
    public void SuccessfulResultExecutingActionAndReturnValue()
    {
        //Act
        var result = OperationResult.Try(() =>
        {
            return new FakePersonEntity("John", "Doe");
        });

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal("The operation result was successful, and set value.", result.Message);
        Assert.NotNull(result.Value);
        Assert.Equal("John", result.Value.FirstName);
        Assert.Equal("Doe", result.Value.LastName);
    }

    [Fact]
    public async void SuccessfulResultExecutingActionAsyncAtFirstAttemptAndReturnValue()
    {
        //Act
        var retryOptions = new RetryOptions(3, TimeSpan.FromSeconds(3));
        var result = await OperationResult.TryAsync(() =>
        {
            return Task.FromResult(new FakePersonEntity("John", "Doe"));
        }, retryOptions);

        //Assert
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal("The operation result was successful, and set value.", result.Message);
        Assert.NotNull(result.Value);
        Assert.Equal("John", result.Value.FirstName);
        Assert.Equal("Doe", result.Value.LastName);
    }

    [Fact]
    public async void UnsuccessfulResultExecutingActionAsyncAndRetryIfFail()
    {
        //Act
        var retryOptions = new RetryOptions(3, TimeSpan.FromSeconds(1.5));
        var result = await OperationResult.TryAsync(() =>
        {
            var zero = 0;
            var div = 1 / zero;

            return Task.CompletedTask;
        }, retryOptions);

        //Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.True(result.IsFailure);
        Assert.Equal("Multiple errors have occurred", result.Message);
        Assert.Equal("DivideByZeroException", result.Errors[2].Type);
        Assert.Equal("Attempt number 3 has failed", result.Errors[2].Title);
        Assert.Equal("Attempted to divide by zero.", result.Errors[2].Details);
        Assert.NotEmpty(result.Errors[2].Metadata);
        Assert.Equal(3, result.Errors.Count);
    }
}
