using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary_MVP
{
    public class ObservableSet<T> : ObservableCollection<T> where T: INotifyPropertyChanged
    {
        public new void Add(T item)
        {
            if (Contains(item)) return;

            base.Add(item);
        }
    }

    public static class ObservableSetExtension
    {
        public static void Sort<T>(this ObservableSet<T> collection)
        where T : IComparable<T>, IEquatable<T>, INotifyPropertyChanged
        {
            List<T> sorted = collection.OrderBy(x => x).ToList();

            int ptr = 0;
            while (ptr < sorted.Count - 1)
            {
                if (!collection[ptr].Equals(sorted[ptr]))
                {
                    int idx = search(collection, ptr + 1, sorted[ptr]);
                    collection.Move(idx, ptr);
                }

                ptr++;
            }
        }

        public static int search<T>(ObservableSet<T> collection, int startIndex, T other)
        where T: INotifyPropertyChanged
        {
            for (int i = startIndex; i < collection.Count; i++)
            {
                if (other.Equals(collection[i]))
                    return i;
            }

            return -1; // decide how to handle error case
        }
    }

}
