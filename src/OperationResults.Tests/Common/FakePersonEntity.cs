namespace OperationResults.Tests.Common;

public class FakePersonEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public FakePersonEntity(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}
