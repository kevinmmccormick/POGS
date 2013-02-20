using System;
using System.Drawing;
using System.Windows.Forms;
using Pogs.ViewModels;

namespace Pogs.Views
{
    public partial class SecurityEditorView : MvvmUserControl
    {
        internal SecurityEditorViewModel ViewModel
        {
            get { return this.DataContext as SecurityEditorViewModel; }
        }

        public SecurityEditorView()
        {
            InitializeComponent();
        }

        private void SecurityEditorView_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled)
            {
                principalsDataGridView.DefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
                principalsDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
                principalsDataGridView.DefaultCellStyle.ForeColor = SystemColors.ControlText;
            }
            else
            {
                principalsDataGridView.DefaultCellStyle.SelectionBackColor = SystemColors.Window;
                principalsDataGridView.DefaultCellStyle.SelectionForeColor = SystemColors.GrayText;
                principalsDataGridView.DefaultCellStyle.ForeColor = SystemColors.GrayText;
            }
        }

        private void principalsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.ViewModel == null)
                return;

            if (principalsDataGridView.SelectedRows.Count == 0)
                this.ViewModel.SelectedDescriptor = null;
            else
                this.ViewModel.SelectedDescriptor = principalsDataGridView.SelectedRows[0].DataBoundItem as SecurityDescriptorViewModel;
        }

        private void principalsDataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (principalsDataGridView.CurrentCell != null &&
                principalsDataGridView.Columns[principalsDataGridView.CurrentCell.ColumnIndex] is DataGridViewCheckBoxColumn)
                principalsDataGridView.EndEdit(DataGridViewDataErrorContexts.Commit);
        }
    }
}