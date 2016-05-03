using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsAdvancedFirewallApi.Utils;

namespace WindowsFirewallDashboard.View.Components
{
    /// <summary>
    /// Interaktionslogik für OptionGroup.xaml
    /// </summary>
    public partial class OptionGroup : UserControl
    {
        public ObservableCollection<RadioButton> Options
        {
            get { return (ObservableCollection<RadioButton>)GetValue(OptionProperty); }
            set { SetValue(OptionProperty, value); }
        }
        public static readonly DependencyProperty OptionProperty = DependencyProperty.Register(nameof(Options), typeof(ObservableCollection<RadioButton>), typeof(OptionGroup));

        public Brush FirstOptionSeparatorBrush
        {
            get { return (Brush)GetValue(FirstOptionSeparatorBrushProperty); }
            set { SetValue(FirstOptionSeparatorBrushProperty, value); }
        }
        public static readonly DependencyProperty FirstOptionSeparatorBrushProperty = DependencyProperty.Register($"{nameof(FirstOptionSeparatorBrush)}", typeof(Brush), typeof(OptionGroup));

        public string GroupName
        {
            get { return (string)GetValue(GroupNameProperty); }
            set { SetValue(GroupNameProperty, value); }
        }
        public static readonly DependencyProperty GroupNameProperty = DependencyProperty.Register(nameof(GroupName), typeof(string), typeof(OptionGroup));

        public event EventHandler SelectionChanged;

        public OptionGroup()
        {
            InitializeComponent();

            Options = new ObservableCollection<RadioButton>();

            InitializeBindings();

            Options.CollectionChanged += Options_CollectionChanged;

            UpdateLayout();
        }

        private void Options_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RadioButtons.Children.Clear();

            foreach (var radioButton in Options)
            {
                radioButton.Margin = new Thickness(4);
                radioButton.Checked += SelectedOptionChanged;

                RadioButtons.Children.Add(radioButton);
                BindToGroupName(radioButton);
                BindToForeground(radioButton);
            }

            UpdateLayout();
        }

        private void SelectedOptionChanged(object sender, RoutedEventArgs e)
        {
            if (SelectionChanged != null)
            {
                SelectionChanged.Invoke(sender, e);
            }
        }

        public new void UpdateLayout()
        {
            base.UpdateLayout();

            if (Options?.Count > 1)
            {
                RadioButtons.Children.Insert(1, (Rectangle)FindResource("FirstOptionSeparatorTemplate"));
            }
        }

        private void InitializeBindings()
        {
            /*BindingOperations.SetBinding(FirstOptionSeparator, Shape.FillProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(FirstOptionSeperatorBrushProperty)
            });

            BindToGroupName(Automatic);
            BindToGroupName(Allow);
            BindToGroupName(Block);
            BindToGroupName(BlockAndPrompt);*/
        }

        private void BindToGroupName(DependencyObject target)
        {
            BindingOperations.SetBinding(target, RadioButton.GroupNameProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(GroupNameProperty)
            });
        }

        private void BindToForeground(DependencyObject target)
        {
            BindingOperations.SetBinding(target, ForegroundProperty, new Binding
            {
                Source = this,
                Path = new PropertyPath(ForegroundProperty)
            });
        }
    }
}
