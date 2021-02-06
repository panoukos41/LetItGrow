namespace BlazorComponentUtilities
{
    public static class CssBuilderExtensions
    {
        public static string Start(this CssBuilder builder) =>
            builder.NullIfEmpty() is string str
                ? str + ' '
                : string.Empty;

        public static string End(this CssBuilder builder) =>
            builder.NullIfEmpty() is string str
                ? ' ' + str
                : string.Empty;
    }
}