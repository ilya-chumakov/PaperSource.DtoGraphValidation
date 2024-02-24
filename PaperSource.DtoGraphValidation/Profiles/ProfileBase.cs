using System.Collections;

namespace PaperSource.DtoGraphValidation.Profiles;

public class ProfileBase : IEnumerable<object[]>
{
    protected List<object[]> Data { get; init; } = new(0);

    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (var pair in Data) yield return pair;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}