using Dictionary_MVP.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary_MVP
{
    public class Category : ObservableObject, IEquatable<Category>, IComparable<Category>
    {
        public static ObservableSet<Category> Categories { get; set; }
        public static HashSet<string> Names { get; set; }
        private string name;
        public string Name
        {
            get => name;
            set { SetAndNotify(ref name, value, () => Name); }
        }

        public static Category Default = new Category("-");

        public Category(string name)
        {
            if (Categories == null)
                Categories = new ObservableSet<Category>();
            if (Names == null)
                Names = new HashSet<string>();

            Name = name;
            Categories.Add(this);
            Categories.Sort<Category>();
            if (!Names.Contains(name))
                Names.Add(name);
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object obj)
        {
            return obj is Category &&
                (obj as Category).Name == Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public bool Equals(Category other)
        {
            return other.Name == Name;
        }

        public int CompareTo(Category other)
        {
            return -other.Name.CompareTo(Name);
        }

        ~Category()
        {

        }
    }
}
