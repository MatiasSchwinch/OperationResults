using OperationResults.Tests.Common;

namespace OperationResults.Tests;

public class ErrorShould
{
    [Fact]
    public void CreateError()
    {
        // Act
        var error = new Error("title error");

        // Assert
        Assert.NotNull(error);
        Assert.Equal("title error", error.Title);
    }

    [Fact]
    public void CreateErrorWithMetadata()
    {
        // Act
        var error = new Error("error with metadata")
            .AddMetadata("ExtraData", "custom metadata");

        // Assert
        Assert.NotNull(error);
        Assert.Equal("error with metadata", error.Title);
        Assert.NotEmpty(error.Metadata);
        Assert.Equal("custom metadata", error.Metadata["ExtraData"]);
    }

    [Fact]
    public void CreateErrorFromException()
    {
        // Act
        var exception = new NotSupportedException("exception message");
        var error = new Error(exception);

        // Assert
        Assert.NotNull(error);
        Assert.Equal("NotSupportedException", error.Type);
        Assert.Equal("An exception has occurred", error.Title);
        Assert.Equal("exception message", error.Details);
        Assert.NotEmpty(error.Metadata);
    }

    [Fact]
    public void CreateCustomError()
    {
        // Act
        var error = new CustomError();

        // Assert
        Assert.NotNull(error);
        Assert.Equal("CustomError", error.Type);
        Assert.Equal("title error", error.Title);
        Assert.Equal("details error", error.Details);
        Assert.NotEmpty(error.Metadata);
        Assert.Equal("www.fakelink.com", error.Metadata["HelpLink"]);
    }
}
