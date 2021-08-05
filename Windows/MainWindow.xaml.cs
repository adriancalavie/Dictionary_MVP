using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static ObservableSet<Word> Words { get; set; }
       public MainWindow()
        {
            InitializeComponent();
            Words = ReadWriteXML.ReadWords();

            DataContext = this;

            PageSwitcher.pageSwitcher = this;
            PageSwitcher.Switch(new MainPage());


        }

        public void Navigate(Page page)
        {
            this.Content = page;
        }

        protected override void OnClosed(EventArgs e)
        {
            if(Content is ModifyPage)
            {
                var currentPage = Content as ModifyPage;

                if(currentPage.hasOpenedDialog && !currentPage.hasReturnedFromDialog)
                {
                    var word = currentPage.GetOriginal();

                    for (int i = 0; i < Words.Count; i++)
                    {
                        if (ReferenceEquals(Words[i], (currentPage.DataContext as Word)))
                        {
                            Words[i] = word;
                        }
                    }
                }
            }

            ReadWriteXML.WriteWords(Words);
            base.OnClosed(e);
        }
    }
}
