using System.Windows.Forms;
using System.Windows.Input;

namespace Pogs.ViewModels
{
    /// <summary>
    /// An ICommand implementation that also provides a DialogResult after the command is executed.
    /// </summary>
    public interface IDialogResultCommand : ICommand
    {
        /// <summary>
        /// Gets the DialogResult based on the last call to Execute().
        /// </summary>
        DialogResult LastResult { get; }
    }
}