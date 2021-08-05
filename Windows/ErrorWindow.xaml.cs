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
using System.Windows.Shapes;

namespace Dictionary_MVP
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public string Message { get; set; }
        public ErrorWindow(string message)
        {
            InitializeComponent();

            Message = message;

            DataContext = this;
        }

        private void ButtonOk_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
