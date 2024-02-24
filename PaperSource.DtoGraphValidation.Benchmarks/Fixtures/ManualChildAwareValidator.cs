using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PaperSource.DtoGraphValidation.Models;

namespace PaperSource.DtoGraphValidation.Benchmarks.Fixtures;

public static class ManualChildAwareValidator
{
    public static List<ValidationResult> TryValidate(Parent parent)
    {
        var results = new List<ValidationResult>(7);

        if (parent.Id < 1 || parent.Id > 9999)
        {
            AddResult(nameof(parent.Id));
        }

        if (string.IsNullOrEmpty(parent.Name) || parent.Name.Length < 12 || parent.Name.Length > 12)
        {
            AddResult(nameof(parent.Name));
        }

        if (parent.Child == null)
        {
            AddResult(nameof(parent.Child));
        }

        var child = parent.Child;

        if (child.ChildCreatedAt == null)
        {
            AddResult(nameof(child.ChildCreatedAt));
        }

        if (child.ChildFlag == false)
        {
            AddResult(nameof(child.ChildFlag));
        }

        void AddResult(string propName)
        {
            results.Add(new ValidationResult(propName, new[] { propName }));
        }

        return results;
    }
}