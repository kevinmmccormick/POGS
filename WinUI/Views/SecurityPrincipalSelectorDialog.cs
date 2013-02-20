using System;
using System.Linq;
using System.Windows.Forms;
using Pogs.ViewModels;
using Pogs.Views;

namespace Pogs.PogsMain
{
    public partial class SecurityPrincipalSelectorDialog : MvvmForm
    {
        internal SecurityPrincipalSelectorViewModel ViewModel
        {
            get { return this.DataContext as SecurityPrincipalSelectorViewModel; }
        }

        public SecurityPrincipalSelectorDialog()
        {
            InitializeComponent();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (this.ViewModel == null)
                return;

            this.ViewModel.Selected = dataGridView1.SelectedRows.Cast<DataGridViewRow>()
                    .Select(dgvr => dgvr.DataBoundItem)
                    .OfType<SecurityPrincipalViewModel>()
                    .ToList();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.ViewModel != null && this.ViewModel.Ok.CanExecute(null))
            {
                this.ViewModel.Ok.Execute(null);
                this.DialogResult = this.ViewModel.Ok.LastResult;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            //workaround a bug where a bound datagridview does not work properly on load
            dataGridView1.ClearSelection();

            base.OnLoad(e);
        }
    }
}