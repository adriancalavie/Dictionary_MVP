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
    /// Interaction logic for ModifyPage.xaml
    /// </summary>
    public partial class ModifyPage : Page
    {
        private Word _original;

        public bool hasOpenedDialog = false;
        public bool hasReturnedFromDialog = false;

        public ModifyPage(Word selected)
        {

            InitializeComponent();
            _original = new Word(selected);
            _original.Image = selected.Image;
            DataContext = selected;


            //if (selected.ImageName != String.Empty)
            //{
            //    SelectedImage.Source = LoadBitmapImage(Tools.App.GetImage(selected.ImageName));
            //    imagePath = Tools.App.GetImage(selected.ImageName);
            //    hasImageOn = true;
            //}
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {

            if (MainWindow.Words.Contains(DataContext as Word))
            {
                int indexFirst = -1;
                int indexSecond = -1;
                for (int i = 0; i < MainWindow.Words.Count; i++)
                {
                    if (MainWindow.Words[i].Equals(DataContext as Word) && !ReferenceEquals(MainWindow.Words[i], DataContext as Word))
                    {
                        indexFirst = i;
                    }
                    else if (MainWindow.Words[i].Equals(DataContext as Word) && ReferenceEquals(MainWindow.Words[i], DataContext as Word))
                    {
                        indexSecond = i;
                    }
                }

                if (indexFirst != -1)
                {
                    QuestionWIndow question = new QuestionWIndow(this, "This word already exists.\nDo you want to overwrite it?", indexFirst, indexSecond);
                    question.Show();
                    hasOpenedDialog = true;
                }
                else
                {
                    PageSwitcher.Switch(new AdminPage());
                }
            }
            else
            {
                PageSwitcher.Switch(new AdminPage());
            }

        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < MainWindow.Words.Count; i++)
            {
                if (ReferenceEquals(MainWindow.Words[i], (DataContext as Word)))
                {
                    MainWindow.Words[i] = _original;
                }
            }

            PageSwitcher.Switch(new AdminPage());
        }

        public Word GetOriginal()
        {
            return _original;
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
            if (WordCategoryComboBox.SelectedItem == Category.Default)
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
