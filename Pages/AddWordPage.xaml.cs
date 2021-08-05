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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dictionary_MVP
{
    /// <summary>
    /// Interaction logic for AddWordPage.xaml
    /// </summary>
    public partial class AddWordPage : Page
    {
        public AddWordPage()
        {
            InitializeComponent();
        }

        private void AddWordPageButton_Click(object sender, RoutedEventArgs e)
        {
            if ((DataContext as Word).Body != "" && (DataContext as Word).Description != "" && (DataContext as Word).Category.Name != "" /*&& !MainWindow.Words.Contains((DataContext as Word))*/)
            {
                if (MainWindow.Words.Contains((DataContext as Word)))
                {
                    ErrorWindow errorWindow = new ErrorWindow("This word can't be inserted because it already exists.\nPlease try again!");
                    errorWindow.Show();
                }
                else
                {
                   
                    MainWindow.Words.Add(DataContext as Word);
                    PageSwitcher.Switch(new AdminPage());
                }
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new AdminPage());
        }

        private void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                openFileDialog.Filter = "PNG|*.png";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;
                }
            }

            (DataContext as Word).Image = Tools.App.LoadBitmapImage(filePath);
            RemoveImageButton.Visibility = Visibility.Visible;
        }

        private void WordCategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(WordCategoryComboBox.SelectedItem == Category.Default)
            {
                WordCategoryComboBox.Visibility = Visibility.Collapsed;
                WordCategoryTextBox.Visibility = Visibility.Visible;
            }
        }

        private void WordCategoryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searched = WordCategoryTextBox.Text;
            if (Category.Names.Contains(searched))
            {
                WordCategoryTextBox.Visibility = Visibility.Collapsed;
                WordCategoryComboBox.Visibility = Visibility.Visible;
                WordCategoryComboBox.SelectedItem = Category.Categories.ElementAt(ObservableSetExtension.search<Category>(Category.Categories, 0, new Category(searched)));
            }
        }

        private void RemoveImageButton_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as Word).Image = null;
            RemoveImageButton.Visibility = Visibility.Collapsed;
        }
    }
}
