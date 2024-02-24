using System.ComponentModel.DataAnnotations;

namespace PaperSource.DtoGraphValidation.Models;

public class ParentValidatableObject : IValidatableObject, IParent<ChildValidatableObject>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public ChildValidatableObject? Child { get; set; }
    public List<ChildValidatableObject> Children { get; init; } = new(0);

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Id < 1 || Id > 9999)
        {
            yield return new ValidationResult("foo error message #1", new[] { nameof(Id) });
        }

        if (string.IsNullOrEmpty(Name) || Name.Length != 12)
        {
            yield return new ValidationResult("foo error message #2", new[] { nameof(Name) });
        }

        if (Child == null)
        {
            yield return new ValidationResult("foo error message #3", new[] { nameof(Child) });
        }
    }
}