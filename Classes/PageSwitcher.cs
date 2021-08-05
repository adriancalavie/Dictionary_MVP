using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dictionary_MVP
{
    class PageSwitcher
    {
        public static MainWindow pageSwitcher;

        public static void Switch(Page page)
        {
            pageSwitcher.Navigate(page);
        }
    }
}
