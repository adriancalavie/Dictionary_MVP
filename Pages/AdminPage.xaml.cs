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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new MainPage());
        }

        private void AddWordButton_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new AddWordPage());
        }

        private void DeleteWordButton_Click(object sender, RoutedEventArgs e)
        {
            if(WordListBox.SelectedItem != null)
                MainWindow.Words.Remove((WordListBox.SelectedItem as Word));
        }

        private void ModifyWordButton_Click(object sender, RoutedEventArgs e)
        {
            if (WordListBox.SelectedItem != null)
                PageSwitcher.Switch(new ModifyPage(WordListBox.SelectedItem as Word));
        }
    }
}
