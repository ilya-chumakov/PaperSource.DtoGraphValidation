using FluentValidation;
using PaperSource.DtoGraphValidation.Models;

namespace PaperSource.DtoGraphValidation.Benchmarks.Fluent;

public class ParentValidator : AbstractValidator<Parent>
{
    public ParentValidator()
    {
        RuleFor(x => x.Id).InclusiveBetween(1, 9999);
        RuleFor(x => x.Name).NotEmpty().Length(min: 12, max: 12);
        RuleFor(x => x.Child).NotNull().SetValidator(new ChildValidator());
        RuleForEach(x => x.Children).NotNull().SetValidator(new ChildValidator());
    }
}

public class FailfastParentValidator : AbstractValidator<Parent>
{
    public FailfastParentValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.Id).InclusiveBetween(1, 9999);
        RuleFor(x => x.Name).NotEmpty().Length(min: 12, max: 12);
        RuleFor(x => x.Child).NotNull().SetValidator(new ChildValidator());
        RuleForEach(x => x.Children).NotNull().SetValidator(new ChildValidator());
    }
}