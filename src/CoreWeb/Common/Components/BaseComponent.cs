using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;

namespace LetItGrow.CoreWeb.Common.Components
{
    public class BaseComponent : ComponentBase
    {
        /// <summary>
        /// Class for other components to provide to yours.
        /// </summary>
        [Parameter]
        public string? Class { get; set; }

        /// <summary>
        /// Css builder to use instead of using your own.
        /// </summary>
        protected CssBuilder css;

        /// <summary>
        /// Get a Css builder with Class prepended to it or emtpy.
        /// </summary>
        /// <returns>A Css builder for the current Class.</returns>
        protected CssBuilder Css() => Class is null
            ? CssBuilder.Empty() : CreateCss(Class);
        
        /// <summary>
        /// Get a Css builder with Class appended.
        /// </summary>
        /// <returns>A Css builder for the current Class.</returns>
        protected CssBuilder Css(string css) => CssBuilder.Default(css).AddClass(Class);

        /// <summary>
        /// Get a new Css builder to build your css.
        /// </summary>
        /// <param name="css"></param>
        /// <returns>An empty css builder.</returns>
        protected static CssBuilder CreateCss(string css) =>
            CssBuilder.Default(css);
    }
}