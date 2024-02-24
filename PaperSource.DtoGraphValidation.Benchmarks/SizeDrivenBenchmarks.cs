using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using MiniValidation;
using PaperSource.DtoGraphValidation.Benchmarks.Fixtures;
using PaperSource.DtoGraphValidation.Benchmarks.Fluent;
using PaperSource.DtoGraphValidation.Models;

namespace PaperSource.DtoGraphValidation.Benchmarks;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
public class SizeDrivenBenchmarks
{
    private const int ExpectedErrorCount = 4;
    private static readonly DataAnnotationsValidator.DataAnnotationsValidator AnnotationsValidator = new();
    private static readonly ParentValidator FluentValidator = new();
    private static readonly FailfastParentValidator FailfastFluentValidator = new();
    private List<Parent> _roots;

    [Params(100, 1_000, 10_000)]
    public int Size { get; set; }

    [GlobalSetup]
    public void GlobalSetup()
    {
        _roots = Enumerable.Range(0, Size).Select(_ => RandomAnnotatedFixture.FullInvalidParent()).ToList();
    }

    [Benchmark(Baseline = true)]
    public void Manual()
    {
        foreach (var model in _roots)
        {
            var results = ManualChildAwareValidator.TryValidate(model);

            if (results.Count != ExpectedErrorCount) throw new NotSupportedException();
        }
    }

    [Benchmark]
    public void DataAnnotationsValidator()
    {
        foreach (var model in _roots)
        {
            var results = new List<ValidationResult>();

            AnnotationsValidator.TryValidateObjectRecursive(model, results);

            if (results.Count != ExpectedErrorCount) throw new NotSupportedException();
        }
    }

    [Benchmark]
    public void MiniValidation()
    {
        foreach (var model in _roots)
        {
            MiniValidator.TryValidate(model, out var results);

            if (results.Count != ExpectedErrorCount) throw new NotSupportedException();
        }
    }

    [Benchmark]
    public void FluentValidation()
    {
        foreach (var model in _roots)
        {
            var result = FluentValidator.Validate(model);

            if (result.Errors.Count != ExpectedErrorCount) throw new NotSupportedException();
        }
    }

    [Benchmark(Description = "FluentValidation + Fail Fast")]
    public void FluentValidationFF()
    {
        foreach (var model in _roots)
        {
            var result = FailfastFluentValidator.Validate(model);

            if (result.Errors.Count != 1) throw new NotSupportedException();
        }
    }
}