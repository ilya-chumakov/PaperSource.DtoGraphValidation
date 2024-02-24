using System.ComponentModel.DataAnnotations;

namespace PaperSource.DtoGraphValidation.Models;

public class Child : IChild
{
    [Required]
    public DateTime? ChildCreatedAt { get; set; }

    [AllowedValues(true)]
    public bool ChildFlag { get; set; }
}