using System.ComponentModel.DataAnnotations;

namespace PaperSource.DtoGraphValidation.Models;

public class ChildValidatableObject : IValidatableObject, IChild
{
    public DateTime? ChildCreatedAt { get; set; }
    public bool ChildFlag { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (ChildCreatedAt == null)
        {
            yield return new ValidationResult("foo error message #2", new[] { nameof(ChildCreatedAt) });
        }

        if (ChildFlag == false)
        {
            yield return new ValidationResult("foo error message #3", new[] { nameof(ChildFlag) });
        }
    }
}