using Dictionary_MVP.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Dictionary_MVP
{
    public class Word : ObservableObject
    {
        public static readonly Word Default = new Word(/*EMPTY*/);

        private BitmapImage image;
        public BitmapImage Image
        {
            get => image;
            set { SetAndNotify(ref image, value, () => Image); }
        }

        private string body;
        public string Body
        {
            get => body;
            set { SetAndNotify(ref body, value, () => Body); }
        }

        private string description;
        public string Description
        {
            get => description;
            set { SetAndNotify(ref description, value, () => Description); }
        }

        private Category category;
        public Category Category
        {
            get => category;
            set { SetAndNotify(ref category, value, () => Category); }
        }

        public string GetImageName()
        {
            return GetHashCode().ToString() + ".png";
        }

        public Word()
        {
            Body = "Not found";
            Description = "This word doesn't exist. Try another one.";
            Category = new Category("-");
        }

        public Word(string body, string desctiption, Category category, BitmapImage bitmapImage = null)
        {
            Body = body;
            Description = desctiption;
            Category = category;
            Image = bitmapImage;
        }

        public Word(Word other)
        {
            Body = other.Body;
            Description = other.Description;
            Category = new Category(other.Category.Name);
            Image = null;
        }

        public override string ToString()
        {
            return Body;
        }

        public override bool Equals(object obj)
        {
            return obj is Word &&
                (obj as Word).body == body;
        }

        public override int GetHashCode()
        {
            int hashCode = 565366710;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(body);

            return hashCode;
        }
    }
}
