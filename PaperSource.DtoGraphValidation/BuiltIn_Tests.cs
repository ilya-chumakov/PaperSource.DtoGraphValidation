using System.ComponentModel.DataAnnotations;
using PaperSource.DtoGraphValidation.Models;
using PaperSource.DtoGraphValidation.Profiles;

namespace PaperSource.DtoGraphValidation;

public class BuiltIn_Tests
{
    [Fact]
    public void TryValidateObject_InvalidChild_NoErrors()
    {
        var validationResults = new List<ValidationResult>();

        var obj = Factory<Parent, Child>.Valid(x => x.Child!.ChildCreatedAt = null);

        Validator.TryValidateObject(obj, new ValidationContext(obj, null, null), validationResults, true);

        Assert.Empty(validationResults);
    }
}