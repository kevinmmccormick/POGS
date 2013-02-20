using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Input;

namespace Pogs.ViewModels
{
    [Description("A component that allows binding of ICommand objects to Windows Forms controls.")]
    [ProvideProperty("CommandName", typeof(IButtonControl))]
    public partial class CommandManager : Component, IExtenderProvider
    {
        private Dictionary<IButtonControl, string> _controlMappings = new Dictionary<IButtonControl, string>();
        private Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

        private BindingSource _dataSource;
        private object _currentItem;

        [DefaultValue(null)]
        [RefreshProperties(RefreshProperties.Repaint)]
        [AttributeProvider(typeof(IListSource))]
        public BindingSource DataSource
        {
            get { return _dataSource; }
            set
            {
                if (_dataSource == value)
                    return;

                if (_dataSource != null)
                    UnregisterDataSource();

                _dataSource = value;
                if (_dataSource != null)
                    RegisterDataSource();
            }
        }

        [TypeConverter(typeof(CommandNameConverter))]
        public string GetCommandName(IButtonControl control)
        {
            string result;
            _controlMappings.TryGetValue(control, out result);
            return result;
        }

        public void SetCommandName(IButtonControl control, string value)
        {
            if (value != null)
            {
                if (!_controlMappings.ContainsKey(control))
                    RegisterClick(control);

                _controlMappings[control] = value;
            }
            else if (_controlMappings.ContainsKey(control))
            {
                UnregisterClick(control);
                _controlMappings.Remove(control);
            }
        }

        public bool ShouldSerializeCommandName(IButtonControl control)
        {
            return _controlMappings.ContainsKey(control);
        }

        public CommandManager()
        {
            InitializeComponent();
        }

        public CommandManager(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        private void UnregisterDataSource()
        {
            this.DataSource.CurrentChanged -= DataSource_CurrentChanged;
            if (_currentItem != null)
                UnregisterCurrentItem();
        }

        private void RegisterDataSource()
        {
            this.DataSource.CurrentChanged += new EventHandler(DataSource_CurrentChanged);
            if (this.DataSource.Current != null)
                RegisterCurrentItem();
        }

        private void RegisterCurrentItem()
        {
            if (_currentItem != null)
                throw new InvalidOperationException("RegisterCurrentItem was called before UnregisterCurrentItem.");

            _currentItem = this.DataSource.Current;

            foreach (var kvp in _controlMappings)
            {
                //1. find the command
                ICommand command = FindCommandFromCurrent(kvp.Value);
                var control = (Control)kvp.Key;

                if (command == null)
                {
                    control.Enabled = false;
                }
                else
                {
                    //2. set enabled
                    control.Enabled = command.CanExecute(null);

                    //3. register canexecutechanged and index
                    if (!_commands.ContainsKey(kvp.Value))
                    {
                        command.CanExecuteChanged += Command_CanExecuteChanged;
                        _commands.Add(kvp.Value, command);
                    }
                }
            }
        }

        private void UnregisterCurrentItem()
        {
            if (_currentItem == null)
                throw new InvalidOperationException("UnregisterCurrentItem was called before RegisterCurrentItem.");

            foreach (var kvp in _controlMappings)
            {
                //1. find the command
                ICommand command = FindCommandFromCurrent(kvp.Value);
                ((Control)kvp.Key).Enabled = false;

                if (_commands.ContainsKey(kvp.Value))
                {
                    command.CanExecuteChanged -= Command_CanExecuteChanged;
                    _commands.Remove(kvp.Value);
                }
            }

            _currentItem = null;
        }

        private IEnumerable<Control> GetControlsMappedTo(string commandName)
        {
            return _controlMappings
                .Where(kvp => kvp.Value == commandName)
                .Select(kvp => kvp.Key)
                .OfType<Control>();
        }

        private IEnumerable<string> GetCommandNames(ICommand command)
        {
            return _commands.Where(kvp => kvp.Value == command).Select(kvp => kvp.Key);
        }

        private void Command_CanExecuteChanged(object sender, EventArgs e)
        {
            ICommand command = (ICommand)sender;
            bool canExecute = command.CanExecute(null);

            foreach (var commandName in GetCommandNames(command))
            {
                foreach (var control in GetControlsMappedTo(commandName))
                {
                    control.Enabled = canExecute;
                }
            }
        }

        private void DataSource_CurrentChanged(object sender, EventArgs e)
        {
            if (_currentItem != null)
                UnregisterCurrentItem();
            if (this.DataSource.Current != null)
                RegisterCurrentItem();
        }

        private ICommand FindCommandFromCurrent(string commandPropertyName)
        {
            PropertyInfo property = _currentItem.GetType()
                    .GetProperty(commandPropertyName, BindingFlags.FlattenHierarchy | BindingFlags.Static |
                                                      BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

            if (property != null)
                return property.GetValue(this.DataSource.Current, null) as ICommand;
            else
                return null;
        }

        private void UnregisterClick(IButtonControl control)
        {
            //special handling for linklabel
            var link = control as LinkLabel;
            if (link != null)
                link.LinkClicked -= ControlClicked;
            else
                ((Control)control).Click -= ControlClicked;
        }

        private void RegisterClick(IButtonControl control)
        {
            //special handling for linklabel
            var link = control as LinkLabel;
            if (link != null)
                link.LinkClicked += ControlClicked;
            else
                ((Control)control).Click += ControlClicked;
        }

        private void ControlClicked(object sender, EventArgs e)
        {
            IButtonControl control = (IButtonControl)sender;
            string commandName = _controlMappings[control];
            ICommand command;
            if (_commands.TryGetValue(commandName, out command))
            {
                command.Execute(null);

                if (command is IDialogResultCommand && ((Control)control).FindForm() != null)
                    ((Control)control).FindForm().DialogResult = ((IDialogResultCommand)command).LastResult;
            }
        }

        #region IExtenderProvider Members

        public bool CanExtend(object extendee)
        {
            return extendee is IButtonControl && extendee is Control;
        }

        #endregion
    }

    public class CommandNameConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            Type t = Type.GetType("System.ComponentModel.ExtendedPropertyDescriptor");
            var manager = GetValueOnPrivateMember(t, context.PropertyDescriptor, "provider") as CommandManager;
            if (manager != null && manager.DataSource != null)
            {
                Type targetType = ListBindingHelper.GetListItemType(manager.DataSource.DataSource, manager.DataSource.DataMember);
                if (targetType != null)
                    return new TypeConverter.StandardValuesCollection(GetCommandNames(targetType));
            }

            return base.GetStandardValues(context);
        }

        private List<string> GetCommandNames(Type targetType)
        {
            var result = new List<string>();

            foreach (PropertyInfo property in targetType.GetProperties(BindingFlags.FlattenHierarchy | BindingFlags.Static | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
            {
                if (typeof(ICommand).IsAssignableFrom(property.PropertyType))
                {
                    result.Add(property.Name);
                }
            }

            return result;
        }

        public static object GetValueOnPrivateMember(Type type, object dataobject, string fieldname)
        {
            BindingFlags getFieldBindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField;
            return type.InvokeMember(fieldname, getFieldBindingFlags, null, dataobject, null);
        }
    }
}