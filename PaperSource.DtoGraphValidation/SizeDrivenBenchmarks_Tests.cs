using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using MiniValidation;
using PaperSource.DtoGraphValidation.Benchmarks.Fixtures;
using PaperSource.DtoGraphValidation.Benchmarks.Fluent;
using PaperSource.DtoGraphValidation.Models;
using Xunit.Abstractions;

namespace PaperSource.DtoGraphValidation;

public class SizeDrivenBenchmarks_Tests
{
    private const int ExpectedErrorCount = 4;
    private static readonly DataAnnotationsValidator.DataAnnotationsValidator DaValidator = new();
    private static readonly ParentValidator FluentValidator = new();
    private static readonly FailfastParentValidator FailfastFluentValidator = new();
    private static readonly Parent Model = RandomAnnotatedFixture.FullInvalidParent();
    private readonly ITestOutputHelper _output;

    public SizeDrivenBenchmarks_Tests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void Manual()
    {
        var results = ManualChildAwareValidator.TryValidate(Model);
        Assert.Equal(ExpectedErrorCount, results.Count);
    }

    [Fact]
    public void DataAnnotationsValidator()
    {
        var results = new List<ValidationResult>();

        DaValidator.TryValidateObjectRecursive(Model, results);
        _output.WriteLine(JsonSerializer.Serialize(Model));
        _output.WriteLine(JsonSerializer.Serialize(results));

        Assert.Equal(RandomAnnotatedFixture.InvalidPropertyNames, results.SelectMany(x => x.MemberNames).Distinct());
        Assert.Equal(ExpectedErrorCount, results.Count);
    }

    [Fact]
    public void MiniValidation()
    {
        MiniValidator.TryValidate(Model, out var results);
        _output.WriteLine(JsonSerializer.Serialize(Model));
        _output.WriteLine(JsonSerializer.Serialize(results));

        Assert.Equal(RandomAnnotatedFixture.InvalidPropertyNames, results.Select(x => x.Key).Distinct());
        Assert.Equal(ExpectedErrorCount, results.Count);
    }

    [Fact]
    public void FluentValidation()
    {
        var result = FluentValidator.Validate(Model);
        _output.WriteLine(JsonSerializer.Serialize(Model));
        _output.WriteLine(JsonSerializer.Serialize(result));

        Assert.Equal(RandomAnnotatedFixture.InvalidPropertyNames, result.Errors.Select(x => x.PropertyName).Distinct());
        Assert.Equal(ExpectedErrorCount, result.Errors.Count);
    }

    [Fact]
    public void FluentValidationFF()
    {
        var result = FailfastFluentValidator.Validate(Model);
        _output.WriteLine(JsonSerializer.Serialize(Model));
        _output.WriteLine(JsonSerializer.Serialize(result));

        Assert.Equal(nameof(Parent.Id), result.Errors.Select(x => x.PropertyName).Single());
        Assert.Single(result.Errors);
    }
}