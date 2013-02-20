using Pogs.Views;

namespace Pogs.WinUI.Views
{
    public partial class CreateUserDialog : MvvmForm
    {
        public CreateUserDialog()
        {
            InitializeComponent();

            this.errorProvider1.SetIconPadding(this.textBox1, -20);
            this.errorProvider1.SetIconPadding(this.textBox2, -20);
        }
    }
}