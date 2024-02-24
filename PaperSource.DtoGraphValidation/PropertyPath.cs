using System.Collections;
using PaperSource.DtoGraphValidation.Models;

namespace PaperSource.DtoGraphValidation;

public class PropertyPath : IEnumerable<object[]>
{
    public const string Id = nameof(Parent.Id);
    public const string Name = nameof(Parent.Name);
    public const string Child = nameof(Parent.Child);

    //public const string ChildCreatedAt = nameof(Parent.Child.ChildCreatedAt);
    //public const string ChildFlag = nameof(Parent.Child.ChildFlag);
    
    //public const string ChildrenFirstCreatedAt = nameof(Parent.Child.ChildCreatedAt);
    //public const string ChildrenFirstFlag = nameof(Parent.Child.ChildFlag);

    public const string ChildCreatedAt = $"{nameof(Parent.Child)}.{nameof(Parent.Child.ChildCreatedAt)}";
    public const string ChildFlag = $"{nameof(Parent.Child)}.{nameof(Parent.Child.ChildFlag)}";
    //public const string ChildrenFirstCreatedAt = $"{nameof(Parent.Children)}[0].{nameof(Parent.Child.ChildCreatedAt)}";
    //public const string ChildrenFirstFlag = $"{nameof(Parent.Children)}[0].{nameof(Parent.Child.ChildFlag)}";

    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { Id };
        yield return new object[] { Name };
        yield return new object[] { Child };
        yield return new object[] { ChildCreatedAt };
        yield return new object[] { ChildFlag };
        //yield return new object[] { ChildrenFirstCreatedAt };
        //yield return new object[] { ChildrenFirstFlag };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}