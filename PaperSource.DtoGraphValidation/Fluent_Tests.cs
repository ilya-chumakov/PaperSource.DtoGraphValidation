using PaperSource.DtoGraphValidation.Benchmarks.Fluent;
using PaperSource.DtoGraphValidation.Models;
using PaperSource.DtoGraphValidation.Profiles;

namespace PaperSource.DtoGraphValidation;

/// <summary>
///     https://github.com/ovation22/DataAnnotationsValidatorRecursive/blob/master/DataAnnotationsValidator/DataAnnotationsValidator/DataAnnotationsValidator.cs#L20
/// </summary>
public class Fluent_Tests
{
    private static readonly ParentValidator Validator = new();

    [Theory]
    [ClassData(typeof(DefaultProfile<Parent, Child>))]
    [ClassData(typeof(CollectionProfileV1<Parent, Child>))]
    public void TryInvalidProperty(string invalidProperty, object target)
    {
        var result = Validator.Validate((Parent)target);

        Assert.Single(result.Errors.Select(x => x.PropertyName), invalidProperty);
    }
}