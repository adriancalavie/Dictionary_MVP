using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for QuestionWIndow.xaml
    /// </summary>
    public partial class QuestionWIndow : Window
    {
        private ModifyPage goBackPage;
        private int _indexFirst;
        private int _indexSecond;

        public string Message { get; set; }
        public QuestionWIndow(ModifyPage comingFromPage, string message, int indexFirst, int indexSecond)
        {
            InitializeComponent();

            goBackPage = comingFromPage;
            Message = message;

            _indexFirst = indexFirst;
            _indexSecond = indexSecond;

            DataContext = this;
        }

        private void ButtonYes_Click(object sender, RoutedEventArgs e)
        {
            var word = MainWindow.Words.ElementAt(_indexSecond);
            if(_indexFirst<_indexSecond)
            {
                MainWindow.Words.RemoveAt(_indexSecond);
                MainWindow.Words.RemoveAt(_indexFirst);
            }
            else
            {
                MainWindow.Words.RemoveAt(_indexFirst);
                MainWindow.Words.RemoveAt(_indexSecond);
            }

            var index = (_indexFirst < _indexSecond) ? _indexFirst: _indexSecond;
            MainWindow.Words.Insert(index, word);
            goBackPage.hasReturnedFromDialog = true;
            PageSwitcher.Switch(new AdminPage());
            Close();
        }

        private void ButtonNo_Click(object sender, RoutedEventArgs e)
        {
            goBackPage.hasReturnedFromDialog = true;
            PageSwitcher.Switch(goBackPage);
            Close();
        }
    }
}
