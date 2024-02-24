using PaperSource.DtoGraphValidation.Models;

namespace PaperSource.DtoGraphValidation.Profiles;

public static class Factory<TRoot, TNested>
    where TRoot : class, IParent<TNested>, new()
    where TNested : class, IChild, new()
{
    public static TRoot Valid(Action<TRoot> postCreate)
    {
        var model = new TRoot
        {
            Id = 2,
            Name = "_aaaa_aaaa_a",
            Child = new TNested
            {
                ChildCreatedAt = DateTime.MaxValue,
                ChildFlag = true
            },
            Children = new List<TNested>
            {
                new()
                {
                    ChildCreatedAt = DateTime.MaxValue,
                    ChildFlag = true
                }
            }
        };
        postCreate(model);
        return model;
    }
}