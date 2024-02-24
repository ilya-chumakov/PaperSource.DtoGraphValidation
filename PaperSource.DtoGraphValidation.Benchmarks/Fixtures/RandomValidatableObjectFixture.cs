using Bogus;
using PaperSource.DtoGraphValidation.Models;

namespace PaperSource.DtoGraphValidation.Benchmarks.Fixtures;

public static class RandomValidatableObjectFixture
{
    public static ParentValidatableObject FullInvalidParent()
    {
        return new Faker<ParentValidatableObject>()
            .RuleFor(x => x.Id, f => f.Random.Int(min: 10000))
            .RuleFor(x => x.Name, f => f.Random.String(13, 24).OrNull(f))
            .RuleFor(x => x.Child, FullInvalidChild);
    }

    public static ChildValidatableObject FullInvalidChild()
    {
        return new Faker<ChildValidatableObject>()
            .RuleFor(m => m.ChildCreatedAt, f => null)
            .RuleFor(m => m.ChildFlag, f => false);
    }
}