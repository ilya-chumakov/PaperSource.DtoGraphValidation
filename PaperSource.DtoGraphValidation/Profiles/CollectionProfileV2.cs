﻿using PaperSource.DtoGraphValidation.Models;

namespace PaperSource.DtoGraphValidation.Profiles;

/// <summary>
///     Format: Children.[0].ChildFlag
/// </summary>
public class CollectionProfileV2<TRoot, TNested>
    : ProfileBase
    where TRoot : class, IParent<TNested>, new()
    where TNested : class, IChild, new()
{
    public CollectionProfileV2()
    {
        var v = Factory<TRoot, TNested>.Valid;
        Data = new List<object[]>
        {
            new object[]
            {
                $"{nameof(Parent.Children)}.[0].{nameof(Parent.Child.ChildCreatedAt)}",
                v(x => x.Children[0].ChildCreatedAt = null)
            },
            new object[]
            {
                $"{nameof(Parent.Children)}.[0].{nameof(Parent.Child.ChildFlag)}",
                v(x => x.Children[0].ChildFlag = false)
            }
        };
    }
}