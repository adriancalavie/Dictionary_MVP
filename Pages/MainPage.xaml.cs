using System;
using System.Collections.Generic;
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

namespace Dictionary_MVP
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new AdminPage());
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new SearchPage());
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new PlayPage());
        }
    }
}
