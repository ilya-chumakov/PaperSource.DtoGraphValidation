using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using MiniValidation;
using PaperSource.DtoGraphValidation.Benchmarks.Fixtures;
using PaperSource.DtoGraphValidation.Models;

namespace PaperSource.DtoGraphValidation.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class ValidatableObjectBenchmarks
{
    private static readonly DataAnnotationsValidator.DataAnnotationsValidator DaValidator = new();
    private List<ParentValidatableObject> _roots;

    [Params(100, 1_000, 10_000)]
    public int Size { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _roots = Enumerable.Range(0, Size).Select(_ => RandomValidatableObjectFixture.FullInvalidParent()).ToList();
    }

    [Benchmark(Baseline = true, Description = "Manual IVO.Validate call")]
    public void ManualIVO()
    {
        foreach (var model in _roots)
        {
            var results = model
                .Validate(new ValidationContext(model))
                .Union(model.Child.Validate(new ValidationContext(model.Child)))
                .ToArray();

            Debug.Assert(results.Length == 5);
        }
    }

    [Benchmark(Description = "DataAnnotationsValidator + IVO")]
    public void DataAnnotationsValidatorIVO()
    {
        foreach (var model in _roots)
        {
            var validationResults = new List<ValidationResult>();

            DaValidator.TryValidateObjectRecursive(model, validationResults);

            Debug.Assert(validationResults.Count == 5);
        }
    }

    [Benchmark(Description = "MiniValidation + IVO")]
    public void MiniValidationIVO()
    {
        foreach (var model in _roots)
        {
            MiniValidator.TryValidate(model, out var results);

            Debug.Assert(results.Count == 5);
        }
    }
}