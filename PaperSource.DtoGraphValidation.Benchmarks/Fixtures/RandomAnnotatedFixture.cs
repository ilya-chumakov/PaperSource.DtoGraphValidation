using System.Collections.Generic;
using Bogus;
using PaperSource.DtoGraphValidation.Models;

namespace PaperSource.DtoGraphValidation.Benchmarks.Fixtures;

public static class RandomAnnotatedFixture
{
    static RandomAnnotatedFixture()
    {
        InvalidPropertyNames = GetInvalidPropertyNames();
    }

    public static string[] InvalidPropertyNames { get; }

    public static Parent FullInvalidParent()
    {
        return new Faker<Parent>()
            .RuleFor(x => x.Id, f => f.Random.Int(10000))
            .RuleFor(x => x.Name, f => f.Random.String(13, 24).OrNull(f))
            .RuleFor(x => x.Child, FullInvalidChild);
    }

    private static Child FullInvalidChild()
    {
        return new Faker<Child>()
            .RuleFor(m => m.ChildCreatedAt, f => null)
            .RuleFor(m => m.ChildFlag, f => false);
    }

    private static string[] GetInvalidPropertyNames()
    {
        var list = new List<string>();
        list.Add(nameof(Parent.Id));
        list.Add(nameof(Parent.Name));
        list.Add($"{nameof(Parent.Child)}.{nameof(Parent.Child.ChildCreatedAt)}");
        list.Add($"{nameof(Parent.Child)}.{nameof(Parent.Child.ChildFlag)}");
        return list.ToArray();
    }
}