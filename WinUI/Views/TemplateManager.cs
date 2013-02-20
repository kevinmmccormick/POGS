using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pogs.DataModel;
using Pogs.VisualModel;

namespace Pogs.PogsMain
{
    public partial class TemplateManager : Form
    {
        #region Declarations

        private PogsDatabase _database;
        private BindingList<TemplateView> _templateViews = new BindingList<TemplateView>();

        #endregion

        #region Constructors

        internal TemplateManager(PogsDatabase database)
        {
            if (database == null)
                throw new ArgumentNullException("database");

            _database = database;

            InitializeComponent();

            entryFieldTypeViewBindingSource.DataSource = EntryFieldTypeView.All;
            templateViewBindingSource.DataSource = _templateViews;
            LoadExistingTemplates();
        }

        #endregion

        #region UI Eventhandlers

        private void addTemplateButton_Click(object sender, EventArgs e)
        {
            var view = new TemplateView(new EntryTemplate()) { ImageList = templateImages };
            _templateViews.Add(view);

            //immediately select the new view
            var row = templateDataGridView.Rows.Cast<DataGridViewRow>().First(dgvr => dgvr.DataBoundItem == view);
            templateDataGridView.CurrentCell = row.Cells[0];    //setting this property updates the bindingsource's current prop
            templateName.Focus();
        }

        private void deleteTemplateButton_Click(object sender, EventArgs e)
        {
            TemplateView view = templateViewBindingSource.Current as TemplateView;
            if (view == null)
                return;

            using (ConfirmDeleteDialog cfd = new ConfirmDeleteDialog(
                String.Format("Are you sure you want to delete the template '{0}'?", view.Name),
                "Any entries that use this template will be given custom fields with the old values.",
                "Delete Template", "Delete Template"))
            {
                if (cfd.ShowDialog() == DialogResult.OK)
                    _templateViews.Remove(view);
            }
        }

        private void addFieldButton_Click(object sender, EventArgs e)
        {
            var view = templateViewBindingSource.Current as TemplateView;
            if (view == null)
                return;

            var newView = view.AddNewField();

            var row = templateFieldsDataGridView.Rows.Cast<DataGridViewRow>().First(dgvr => dgvr.DataBoundItem == newView);
            templateFieldsDataGridView.CurrentCell = row.Cells[0];    //setting this property updates the bindingsource's current prop
            templateFieldsDataGridView.Focus();
        }

        private void removeFieldButton_Click(object sender, EventArgs e)
        {
            var templateView = templateViewBindingSource.Current as TemplateView;
            var fieldView = fieldViewsBindingSource.Current as EntryFieldView;
            if (templateView == null || fieldView == null)
                return;

            using (ConfirmDeleteDialog cfd = new ConfirmDeleteDialog(
                String.Format("Are you sure you want to delete the '{0}' field from the template '{1}'?", fieldView.Name, templateView.Name),
                "Any entries that use this field will be given custom fields with the old values.",
                "Delete Field", "Delete Template Field"))
            {
                if (cfd.ShowDialog() == DialogResult.OK)
                    templateView.RemoveView(fieldView);
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            //commit the changes to all of the objects in memory and the database
            foreach (var v in _templateViews)
            {
                if (v.Commit()) //memory
                {
                    if (_database.Templates.Contains(v.Template))
                        _database.CommitTemplate(v.Template);       //database
                    else
                        _database.AddTemplate(v.Template);          //database
                }
            }

            //remove deleted templates
            foreach (var t in _database.Templates.Except(_templateViews.Select(tv => tv.Template)).ToList())
            {
                _database.RemoveTemplate(t);                        //database
            }

            this.DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void templateViewBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            //manually select icon
            chooseIconListView.SelectedItems.Clear();

            var tv = templateViewBindingSource.Current as TemplateView;
            if (tv != null)
            {
                chooseIconListView.SelectedIndexChanged -= chooseIconListView_SelectedIndexChanged;
                chooseIconListView.Items[tv.Template.IconIndex].Selected = true;
                chooseIconListView.SelectedIndexChanged += chooseIconListView_SelectedIndexChanged;
            }
        }

        private void chooseIconListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tv = templateViewBindingSource.Current as TemplateView;
            if (tv != null && chooseIconListView.SelectedItems.Count == 1)
            {
                tv.IconIndex = chooseIconListView.SelectedItems[0].Index;
            }
        }

        #endregion

        #region Private Methods

        private void LoadExistingTemplates()
        {
            _database.RefreshTemplates();

            foreach (var template in _database.Templates.OrderBy(t => t.Name))
            {
                var view = new TemplateView(template) { ImageList = templateImages };
                _templateViews.Add(view);
            }
        }

        private void CustomDispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var view in _templateViews)
                {
                    view.Dispose();
                }
            }
        }

        #endregion
    }
}