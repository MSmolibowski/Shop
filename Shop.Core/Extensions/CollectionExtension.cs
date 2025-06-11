namespace Shop.Core.Extensions;
public static class CollectionExtension
{
    public static void ThrowIfNullOrEmpty(this IEnumerable<int> list) 
    {
        if (list == null || list.Count() == 0)
        {
            throw new ArgumentNullException("Collection cannot be null or empty.");
        }        
    }
}
