using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace LetItGrow.UI.Common
{
    public abstract class RxViewModel : ReactiveObject, IActivatableViewModel
    {
        /// <inheritdoc/>
        public ViewModelActivator Activator { get; } = new();

        /// <summary>
        /// Indicates wheter the base model is loaded.
        /// </summary>
        [Reactive]
        public bool Loading { get; set; }

        /// <summary>
        /// Indicates wheter the base model is not found.
        /// </summary>
        [Reactive]
        public bool NotFound { get; set; }
    }
}