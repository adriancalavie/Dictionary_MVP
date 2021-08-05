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
    /// Interaction logic for PlayPage.xaml
    /// </summary>
    public partial class PlayPage : Page
    {
        private Word word1 = null;
        private Word word2 = null;
        private Word word3 = null;
        private Word word4 = null;
        private Word word5 = null;

        List<Word> questions;

        private int correctAnswers = 0;
        private int stage = 1;
        public Word CurrentWord { get; set; }

        public PlayPage()
        {
            InitializeComponent();

            DataContext = CurrentWord;

            SetGame();

            questions = new List<Word> { word1, word2, word3, word4, word5 };
            StartGame();

        }

        private void StartGame()
        {
            NextQuestion(questions.ElementAt(0));
            Back.Visibility = Visibility.Collapsed;
        }

        private void NextQuestion(Word question)
        {
            ResponseTextBox.Text = "";

            if (ReferenceEquals(question, questions.ElementAt(4)))
            {
                NavigationButton.Content = "Finish";
            }


            QuestionImage.Source = null;
            QuestionImage.Visibility = Visibility.Collapsed;

            
            QuestionDescription.Text = null;
            ScrollViewDescription.Visibility = Visibility.Collapsed;


            CurrentWord = question;

            if (question.Image != null)
            {
                Random random = new Random();

                int coin = random.Next(0, 1);

                if (coin == 0)
                {
                    //SHOW IMAGE

                    QuestionImage.Source = question.Image;
                    QuestionImage.Visibility = Visibility.Visible;
                }
                else
                {
                    //SHOW DESCRIPTION
                    QuestionDescription.Text = question.Description;
                    ScrollViewDescription.Visibility = Visibility.Visible;
                }
            }
            else
            {
                //SHOW DESCRIPTION
                QuestionDescription.Text = question.Description;
                ScrollViewDescription.Visibility = Visibility.Visible;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            PageSwitcher.Switch(new MainPage());
        }

        private void SetGame()
        {
            Random random = new Random();

            int[] array = { -1, -1, -1, -1, -1 };

            for (int i = 0; i < 5; i++)
            {
                int contender = -1;
                do
                {
                    contender = random.Next(0, MainWindow.Words.Count);
                }
                while (array.Contains(contender));

                array[i] = contender;
            }

            word1 = MainWindow.Words.ElementAt(array[0]);
            word2 = MainWindow.Words.ElementAt(array[1]);
            word3 = MainWindow.Words.ElementAt(array[2]);
            word4 = MainWindow.Words.ElementAt(array[3]);
            word5 = MainWindow.Words.ElementAt(array[4]);
        }

        private void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            if (ResponseTextBox.Text == CurrentWord.Body)
            {
                correctAnswers++;
                MessageBox.Show("CORRECT");
            }
            else
            {
                MessageBox.Show("INCORRECT");
            }

            if (stage < 5)
            {
                NextQuestion(questions.ElementAt(stage++));
            }
            else
            {
                PageSwitcher.Switch(new Pages.FinalScreen(correctAnswers));
            }
        }
    }
}
