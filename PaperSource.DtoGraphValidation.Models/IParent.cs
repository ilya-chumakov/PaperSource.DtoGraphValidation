namespace PaperSource.DtoGraphValidation.Models;

public interface IParent<TNested>
{
    int Id { get; set; }
    string? Name { get; set; }
    TNested? Child { get; set; }
    List<TNested> Children { get; init; }
}