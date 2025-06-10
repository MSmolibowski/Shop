namespace Shop.Core.Extensions;
public static class CollectionExtension
{
    public static void ThrowIfNullOrEmpty<T>(this List<int> list) 
        where T : class
    {
        if (list == null) throw new ArgumentNullException("Value is null.");
        if (list.Count == 0) throw new ArgumentException("Empty collection.");
    }
}
