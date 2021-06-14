using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LetItGrow.CoreWeb.Common
{
    public class ThemeJs
    {
        public const string Light = "light";

        public const string Dark = "dark";

        private readonly IJSRuntime js;

        public ThemeJs(IJSRuntime jsRuntime) => js = jsRuntime;

        public ValueTask<string> GetTheme() => js.InvokeAsync<string>("window.getTheme");
        
        public ValueTask<string> Toggle() => js.InvokeAsync<string>("window.toggleTheme");
    }
}