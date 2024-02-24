using System.ComponentModel.DataAnnotations;
using PaperSource.DtoGraphValidation.Models;
using PaperSource.DtoGraphValidation.Profiles;

namespace PaperSource.DtoGraphValidation;

/// <summary>
///     https://github.com/ovation22/DataAnnotationsValidatorRecursive/blob/master/DataAnnotationsValidator/DataAnnotationsValidator/DataAnnotationsValidator.cs#L20
/// </summary>
public class DataAnnotationsValidator_Tests
{
    private static readonly DataAnnotationsValidator.DataAnnotationsValidator Validator = new();

    [Theory]
    [ClassData(typeof(DefaultProfile<Parent, Child>))]
    [ClassData(typeof(CollectionProfileV0<Parent, Child>))]
    public void TryValidateObjectRecursive_Annotated(string invalidProperty, object target)
    {
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObjectRecursive(target, validationResults);

        Assert.Single(validationResults.SelectMany(x => x.MemberNames), invalidProperty);
    }

    [Theory]
    [ClassData(typeof(DefaultProfile<ParentValidatableObject, ChildValidatableObject>))]
    [ClassData(typeof(CollectionProfileV0<ParentValidatableObject, ChildValidatableObject>))]
    public void TryValidateObjectRecursive_ValidatableObject(string invalidProperty, object target)
    {
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObjectRecursive(target, validationResults);

        Assert.Single(validationResults.SelectMany(x => x.MemberNames), invalidProperty);
    }
}