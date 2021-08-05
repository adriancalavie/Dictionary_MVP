using Dictionary_MVP.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Linq;

namespace Dictionary_MVP
{
    class ReadWriteXML
    {
        public static void WriteWords(ObservableSet<Word> words)
        {

            XmlTextWriter writer = new XmlTextWriter(getXmlFile(), null);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartElement("words");
            if (words != null)
                foreach (var word in words)
                {
                    writer.WriteStartElement("word");

                    writer.WriteStartAttribute("body");
                    writer.WriteString(word.Body);
                    writer.WriteEndAttribute();

                    writer.WriteStartAttribute("description");
                    writer.WriteString(word.Description);
                    writer.WriteEndAttribute();

                    writer.WriteStartAttribute("category");
                    writer.WriteString(word.Category.Name);
                    writer.WriteEndAttribute();

                    writer.WriteStartAttribute("image");
                    if (word.Image != null)
                    {
                        var filePath = Tools.App.GetImagesFolder() + word.Body + ".png";
                        writer.WriteString(filePath);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            BitmapEncoder encoder = new PngBitmapEncoder();
                            encoder.Frames.Add(BitmapFrame.Create(word.Image));
                            encoder.Save(fileStream);
                        }
                    }
                    writer.WriteEndAttribute();

                    writer.WriteEndElement();
                }

            writer.WriteEndElement();

            writer.Flush();
            writer.Close();
        }

        public static ObservableSet<Word> ReadWords()
        {
            ObservableSet<Word> result = new ObservableSet<Word>();
            XmlReader reader = XmlReader.Create(getXmlFile());


            if (reader.ReadToFollowing("word"))
            {
                do
                {

                    reader.MoveToAttribute("body");
                    string body = reader.ReadContentAsString();

                    reader.MoveToAttribute("description");
                    string description = reader.ReadContentAsString();

                    reader.MoveToAttribute("category");
                    Category category = new Category(reader.ReadContentAsString());

                    reader.MoveToAttribute("image");
                    string imageName = reader.ReadContentAsString();

                    Word word;
                    if(imageName != "")
                        word = new Word(body, description, category, Tools.App.LoadBitmapImage(imageName));
                    else
                        word = new Word(body, description, category);
                    result.Add(word);

                } while (reader.ReadToFollowing("word"));
            }
            reader.Close();

            return result;
        }

        private static string getXmlFile()
        {
            var filename = @"Words.xml";
            var resourcesFolder = Tools.App.GetResourcesFolder();
            var wordsFilePath = Path.Combine(resourcesFolder, filename);

            //ConsoleManager.Show();
            //Console.WriteLine(wordsFilePath);
            return wordsFilePath;
        }
    }
}
