namespace Shop.Core.Extensions;
public static class StringExtension
{
    public static void ThrowIfNullOrEmpty(this string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException("Argument cannot be null or empty.");
        }
    }
}
