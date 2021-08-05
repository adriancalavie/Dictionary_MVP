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
    /// Interaction logic for SearchBar.xaml
    /// </summary>
    public partial class SearchBar : UserControl
    {
        private class Data
        {
            public static List<Word> getData()
            {
                List<Word> data = new List<Word>();

                foreach (var obj in MainWindow.Words)
                {
                    if (SearchPage.SelectedCategory != Category.Default)
                    {
                        if (obj.Category == SearchPage.SelectedCategory)
                            data.Add(obj);
                    }
                    else
                    {
                        data.Add(obj);
                    }
                }

                return data;
            }
        }

        public SearchBar()
        {
            InitializeComponent();

        }

        private Word selectedWord = null;

        public Word SelectedWord
        {
            get => selectedWord;
            set => selectedWord = value;
        }


        private void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            bool isFound = false;
            SelectedWord = null;

            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = Data.getData();

            string find = (sender as TextBox).Text;
            if (find.Length == 0)
            {
                resultStack.Children.Clear();
                resultStack.Background.Opacity = 0;
                border.Visibility = Visibility.Collapsed;
            }
            else if (e.Key == Key.Left || e.Key == Key.Right || e.Key == Key.Up || e.Key == Key.Down)
            {
                if (resultStack.Children.Count > 0)
                {
                    textBox.Text = resultStack.Children.OfType<Label>().FirstOrDefault().Content.ToString();
                    textBox.CaretIndex = textBox.Text.Length;
                    border.Visibility = Visibility.Collapsed;
                }
            }
            else if (e.Key == Key.Enter)
            {
                //textBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                if (resultStack.Children.Count > 0)
                {
                    textBox.Text = resultStack.Children.OfType<Label>().FirstOrDefault().Content.ToString();
                    textBox.CaretIndex = textBox.Text.Length;
                    border.Visibility = Visibility.Collapsed;
                }
                SearchIt();
                //TODO: take first element and go to 
            }
            else
            {
                resultStack.Background.Opacity = 0.5;
                border.Visibility = Visibility.Visible;
            }

            resultStack.Children.Clear();

            foreach (var obj in data)
            {
                if (obj.Body.Trim().ToLower().StartsWith(find.Trim().ToLower()))
                {
                    AddItem(obj);
                    int wordsMatching = resultStack.Children.Count;
                    if (wordsMatching <= 4)
                    {
                        resultStack.Height = ScrollViewer.Height = BorderAuto.Height = wordsMatching * 30;
                    }
                    isFound = true;
                }
            }

            if (!isFound)
            {
                resultStack.Background.Opacity = 0;
            }

        }
        private void AddItem(Word obj)
        {
            Label block = new Label();
            block.HorizontalContentAlignment = HorizontalAlignment.Center;

            block.Content = obj;
            block.FontSize = 16;
            block.Height = 30;
            block.Foreground = new SolidColorBrush(Colors.Black);

            block.VerticalAlignment = VerticalAlignment.Center;
            block.HorizontalContentAlignment = HorizontalAlignment.Left;

            block.MouseLeftButtonDown += (sender, e) =>
            {
                Label lb = sender as Label;
                textBox.Text = lb.Content.ToString();
                SelectedWord = lb.Content as Word;
                lb.Background = new SolidColorBrush(Colors.Transparent);
            };

            block.MouseEnter += (sender, e) =>
            {
                Label lb = sender as Label;
                lb.Background = new SolidColorBrush(Colors.White);
                lb.Foreground = new SolidColorBrush(Colors.Black);
            };

            block.MouseLeave += (sender, e) =>
            {
                Label lb = sender as Label;
                lb.Background = new SolidColorBrush(Colors.Transparent);
                lb.Foreground = new SolidColorBrush(Colors.Black);
            };

            resultStack.Children.Add(block);
        }


        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            //textBox.Text = "Search a word";
            //textBox.Foreground = new SolidColorBrush(Colors.Gray);
            resultStack.Children.Clear();
            resultStack.Background.Opacity = 0;
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            textBox.Text = "";
            textBox.Foreground = new SolidColorBrush(Colors.Black);
            resultStack.Background = new SolidColorBrush(Colors.LightGray);
            resultStack.Background.Opacity = 0;
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            SearchIt();
        }

        private void SearchIt()
        {
            bool isFound = false;
            var data = Data.getData();

            string temp = textBox.Text.ToLower().Trim();

            foreach (var item in data)
            {
                if (item.Body.ToLower() == temp)
                {
                    PageSwitcher.Switch(new WordPage(item));
                    isFound = true;
                    break;
                }
            }

            if (!isFound)
            {
                PageSwitcher.Switch(new WordPage(Word.Default));
            }
        }
    }
}
