using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Dictionary_MVP.Tools
{
    /// <summary>
    /// <c>Tools.Application</c> is an utility class.
    /// </summary>
    public static class App
    {
        /// <summary>
        /// Returns the running directory for this app.
        /// </summary>
        public static string GetAppDirectory()
        {
            string runningPath = AppDomain.CurrentDomain.BaseDirectory;
            runningPath = Path.GetFullPath(Path.Combine(runningPath, @"..\..\"));
            //ConsoleManager.Show();
            //Console.WriteLine(runningPath);
            return runningPath;
        }
        public static string GetResourcesFolder()
        {
            string resourcesFolder = string.Format("{0}Resources\\", GetAppDirectory());
            //ConsoleManager.Show();
            //Console.WriteLine(resourcesFolder);

            return resourcesFolder;
        }

        public static string GetImagesFolder()
        {
            string imagesFolder = string.Format("{0}WordImages\\", GetResourcesFolder());
            //ConsoleManager.Show();
            //Console.WriteLine(imagesFolder);
            return imagesFolder;
        }

        public static string GetImage(string name)
        {
            string imagePath = string.Format("{0}" + name, GetImagesFolder());
            //ConsoleManager.Show();
            //Console.WriteLine(imagesFolder);
            return imagePath;
        }
        public static BitmapImage LoadBitmapImage(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Open))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }
    }
}
