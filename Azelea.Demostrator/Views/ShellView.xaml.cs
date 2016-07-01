using Azelea.Demostrator.ViewModels;
using System.ComponentModel.Composition;
using System.Windows;

namespace Azelea.Demostrator.Views
{
    /// <summary>
    /// ShellView.xaml 的交互逻辑
    /// </summary>
    [Export]
    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
        }

        [Import]
        ShellViewModel ViewModel
        {
            set
            {
                DataContext = value;
            }
        }
    }
}
