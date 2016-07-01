using Azelea.Demostrator.Module.Navigation.ViewModels;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Azelea.Demostrator.Module.Navigation.Views
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class NavigationView : UserControl
    {
        public NavigationView()
        {
            InitializeComponent();
        }

        [Import]
        NavigationViewModel ViewModel
        {
            set
            {
                DataContext = value;
            }
        }
    }
}
