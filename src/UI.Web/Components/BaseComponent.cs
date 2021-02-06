using Microsoft.AspNetCore.Components;

namespace LetItGrow.UI.Web.Components
{
    public abstract class BaseComponent : ComponentBase
    {
        /// <summary>
        /// Class as in html, this is a string that accepts CSS
        /// so that the component can use it freely.
        /// </summary>
        [Parameter]
        public virtual string? Class { get; set; }

        ///// <summary>
        ///// Disabled as in html, this is a boolean to be used
        ///// from base components to disable their html or not.
        ///// </summary>
        //[Parameter]
        //public virtual bool Disabled { get; set; }

        ///// <summary>
        ///// Id as in html, this is a string to be used as Id
        ///// in html elements. The component decides what to do
        ///// with it.
        ///// </summary>
        //[Parameter]
        //public string? Id { get; set; }
    }
}