using MiniValidation;
using PaperSource.DtoGraphValidation.Models;
using PaperSource.DtoGraphValidation.Profiles;

namespace PaperSource.DtoGraphValidation;

/// <summary>
///     https://github.com/ovation22/DataAnnotationsValidatorRecursive/blob/master/DataAnnotationsValidator/DataAnnotationsValidator/DataAnnotationsValidator.cs#L20
/// </summary>
public class MiniValidation_Tests
{
    [Theory]
    [ClassData(typeof(DefaultProfile<Parent, Child>))]
    [ClassData(typeof(CollectionProfileV1<Parent, Child>))]
    public void TryValidate_Annotated(string invalidProperty, object target)
    {
        MiniValidator.TryValidate(target, out var validationResults);

        Assert.Single(validationResults.Select(x => x.Key), invalidProperty);
    }

    [Theory]
    [ClassData(typeof(DefaultProfile<ParentValidatableObject, ChildValidatableObject>))]
    [ClassData(typeof(CollectionProfileV2<ParentValidatableObject, ChildValidatableObject>))]
    public void TryValidate_ValidatableObject(string invalidProperty, object target)
    {
        MiniValidator.TryValidate(target, out var validationResults);

        Assert.Single(validationResults.Select(x => x.Key), invalidProperty);
    }
}