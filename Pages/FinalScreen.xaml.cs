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

namespace Dictionary_MVP.Pages
{
    /// <summary>
    /// Interaction logic for FinalScreen.xaml
    /// </summary>
    public partial class FinalScreen : Page
    {
        public string Message { get; set; }
        public FinalScreen(int correctAnswers)
        {
            InitializeComponent();

            Message = "You responded corectly to " + correctAnswers + " out of 5.";

            DataContext = this;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new MainPage());
        }
    }
}
