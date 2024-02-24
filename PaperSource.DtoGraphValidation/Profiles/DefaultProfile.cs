using PaperSource.DtoGraphValidation.Models;

namespace PaperSource.DtoGraphValidation.Profiles;

public class DefaultProfile<TRoot, TNested>
    : ProfileBase
    where TRoot : class, IParent<TNested>, new()
    where TNested : class, IChild, new()
{
    public DefaultProfile()
    {
        var v = Factory<TRoot, TNested>.Valid;
        Data = new List<object[]>
        {
            new object[] { PropertyPath.Id, v(x => x.Id = 10000) },
            new object[] { PropertyPath.Name, v(x => x.Name = "a") },
            new object[] { PropertyPath.Child, v(x => x.Child = null!) },
            new object[] { PropertyPath.ChildCreatedAt, v(x => x.Child!.ChildCreatedAt = null) },
            new object[] { PropertyPath.ChildFlag, v(x => x.Child!.ChildFlag = false) }
        };
    }
}