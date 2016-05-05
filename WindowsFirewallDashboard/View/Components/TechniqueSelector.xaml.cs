using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
using static WindowsFirewallDashboard.Model.ProfileRuleAction;

namespace WindowsFirewallDashboard.View.Components
{
    /// <summary>
    /// Interaktionslogik für TechniqueSelector.xaml
    /// </summary>
    public partial class TechniqueSelector : UserControl
    {
        private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

        public class TechniqueChangedEventArgs : EventArgs
        {
            public Technique SelectedTechnique { private set; get; }

            public TechniqueChangedEventArgs(Technique newTechnique)
            {
                SelectedTechnique = newTechnique;
            }
        }

        public Brush AutomaticSeparator
        {
            get { return (Brush)GetValue(AutomaticSeparatorProperty); }
            set { SetValue(AutomaticSeparatorProperty, value); }
        }

        public static readonly DependencyProperty AutomaticSeparatorProperty = DependencyProperty.Register(nameof(AutomaticSeparator), typeof(Brush), typeof(TechniqueSelector));

        [Category(nameof(TechniqueSelector))]
        public Technique SelectedTechnique
        {
            get { return (Technique)GetValue(SelectedTechniqueProperty); }
            set
            {
                if (value != SelectedTechnique)
                {
                    SetValue(SelectedTechniqueProperty, value);
                    UpdateSelectedRadioButton(value);
                }
            }
        }

        public static readonly DependencyProperty SelectedTechniqueProperty = DependencyProperty.Register(nameof(SelectedTechnique), typeof(Technique), typeof(TechniqueSelector));


        public RadioButton Automatic
        {
            get
            {
                return OptionGroup.Options.ElementAt(0);
            }
        }

        public RadioButton Allow
        {
            get
            {
                return OptionGroup.Options.ElementAt(1);
            }
        }

        public RadioButton Block
        {
            get
            {
                return OptionGroup.Options.ElementAt(2);
            }
        }

        public RadioButton BlockAndPrompt
        {
            get
            {
                return OptionGroup.Options.ElementAt(3);
            }
        }

        public event EventHandler<TechniqueChangedEventArgs> SelectedTechniqueChanged;

        public TechniqueSelector()
        {
            InitializeComponent();

            AutomaticSeparator = new SolidColorBrush(Colors.Black);
            SelectedTechnique = default(Technique);
            OptionGroup.SelectionChanged += OptionGroup_SelectionChanged;

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdateSelectedRadioButton(SelectedTechnique);
        }

        private void OptionGroup_SelectionChanged(object sender, EventArgs e)
        {
            if (sender == Automatic)
            {
                RaiseTechniqueChanged(Technique.AutomaticOrPrompt);
            }
            else if (sender == Allow)
            {
                RaiseTechniqueChanged(Technique.Allow);
            }
            else if(sender == Block)
            {
                RaiseTechniqueChanged(Technique.Block);
            }
            else if (sender == BlockAndPrompt)
            {
                RaiseTechniqueChanged(Technique.BlockAndPrompt);
            }
        }

        private void RaiseTechniqueChanged(Technique technique)
        {
            SelectedTechnique = technique;

            if (SelectedTechniqueChanged != null)
            {
                SelectedTechniqueChanged.Invoke(this, new TechniqueChangedEventArgs(technique));
            }
        }

        private void UpdateSelectedRadioButton(Technique technique)
        {
            switch (technique)
            {
                case Technique.Allow:
                    Allow.IsChecked = true;
                    break;
                case Technique.Block:
                    Block.IsChecked = true;
                    break;
                case Technique.BlockAndPrompt:
                    BlockAndPrompt.IsChecked = true;
                    break;
                case Technique.AutomaticOrPrompt:
                    Automatic.IsChecked = true;
                    break;
                default:
                    break;
            }
        }
    }
}
