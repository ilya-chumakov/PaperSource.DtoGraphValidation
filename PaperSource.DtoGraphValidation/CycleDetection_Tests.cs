using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MiniValidation;

namespace PaperSource.DtoGraphValidation;

public class CycleDetection_Tests
{
    private static readonly TreeNode Root;
    private static readonly DataAnnotationsValidator.DataAnnotationsValidator Validator = new();
    private static readonly string InvalidProperty;

    static CycleDetection_Tests()
    {
        Root = new TreeNode(1,
            null,
            new TreeNode(2, new TreeNode(101)) // the only error
        );

        Root.Right!.Right = Root;
        InvalidProperty = "Right.Left.Val";
    }

    [Fact]
    public void MiniValidation_Cycle()
    {
        MiniValidator.TryValidate(Root, out var results);

        Assert.Single(results);
        Assert.Single(results.Select(x => x.Key), InvalidProperty);
    }

    [Fact]
    public void DataAnnotationValidator_Cycle()
    {
        var validationResults = new List<ValidationResult>();
        Validator.TryValidateObjectRecursive(Root, validationResults);

        Assert.Single(validationResults);
        Assert.Single(validationResults.Select(x => x.MemberNames.Single()), InvalidProperty);
    }

    [Fact(Skip = "Stack overflow exception here")]
    public void Fluent_Cycle()
    {
        var validator = new TreeNodeValidator();
        var result = validator.Validate(Root);

        Assert.Single(result.Errors.Select(x => x.PropertyName), InvalidProperty);
    }
}

public class TreeNode
{
    public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
    {
        Val = val;
        Left = left;
        Right = right;
    }

    public TreeNode? Left { get; set; }
    public TreeNode? Right { get; set; }

    [Range(1, 100)]
    public int Val { get; set; }
}

public class TreeNodeValidator : AbstractValidator<TreeNode>
{
    public TreeNodeValidator()
    {
        RuleFor(x => x.Val).InclusiveBetween(1, 100);
        RuleFor(x => x.Left).NotNull().SetValidator(this!);
        RuleFor(x => x.Right).NotNull().SetValidator(this!);
    }
}