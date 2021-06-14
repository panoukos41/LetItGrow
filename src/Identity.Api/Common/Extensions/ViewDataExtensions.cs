namespace Microsoft.AspNetCore.Mvc.ViewFeatures
{
    public static class ViewDataExtensions
    {
        public static dynamic GetTitle(this ViewDataDictionary viewData)
        {
            return viewData["Title"];
        }

        public static void SetTitle(this ViewDataDictionary viewData, string title)
        {
            viewData["Title"] = title;
        }
    }
}