using System.ComponentModel.DataAnnotations;

namespace PaperSource.DtoGraphValidation.Models;

public class Parent : IParent<Child>
{
    [Range(1, 9999)]
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    [StringLength(12, MinimumLength = 12)]
    public string? Name { get; set; }

    [Required]
    public Child? Child { get; set; }

    [Required]
    public List<Child> Children { get; init; } = new(0);
}