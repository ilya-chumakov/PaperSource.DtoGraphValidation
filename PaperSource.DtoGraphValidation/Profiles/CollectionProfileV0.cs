using PaperSource.DtoGraphValidation.Models;

namespace PaperSource.DtoGraphValidation.Profiles;

/// <summary>
///     Format: Children.ChildFlag
/// </summary>
public class CollectionProfileV0<TRoot, TNested>
    : ProfileBase
    where TRoot : class, IParent<TNested>, new()
    where TNested : class, IChild, new()
{
    public CollectionProfileV0()
    {
        var v = Factory<TRoot, TNested>.Valid;
        Data = new List<object[]>
        {
            new object[]
            {
                $"{nameof(Parent.Children)}.{nameof(Parent.Child.ChildCreatedAt)}",
                v(x => x.Children[0].ChildCreatedAt = null)
            },
            new object[]
            {
                $"{nameof(Parent.Children)}.{nameof(Parent.Child.ChildFlag)}",
                v(x => x.Children[0].ChildFlag = false)
            }
        };
    }
}