namespace PaperSource.DtoGraphValidation.Models;

public interface IChild
{
    DateTime? ChildCreatedAt { get; set; }
    bool ChildFlag { get; set; }
}