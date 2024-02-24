using FluentValidation;
using PaperSource.DtoGraphValidation.Models;

namespace PaperSource.DtoGraphValidation.Benchmarks.Fluent;

public class ChildValidator : AbstractValidator<Child>
{
    public ChildValidator()
    {
        RuleFor(x => x.ChildCreatedAt).NotNull();
        RuleFor(x => x.ChildFlag).Equal(true);
    }
}

public class FailfastChildValidator : AbstractValidator<Child>
{
    public FailfastChildValidator()
    {
        ClassLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.ChildCreatedAt).NotNull();
        RuleFor(x => x.ChildFlag).Equal(true);
    }
}