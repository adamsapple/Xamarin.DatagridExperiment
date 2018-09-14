using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ListViewTest01
{
    public static class ObservableCollectionExtensions
    {
        public static void Sort2<T>(this ObservableCollection<T> collection, Comparison<T> comparison)
        {
            var sortableList = collection.ToList();
            sortableList.Sort(comparison);

            for (int i = 0; i < sortableList.Count; i++)
            {
                collection.Move(collection.IndexOf(sortableList[i]), i);
            }
        }
        public static ObservableCollection<T> Sort<T>(this ObservableCollection<T> collection, Func<T, object> keySelector)
        {
            //var sortableList = collection.ToList();
            //sortableList.Sort(comparison);

            return new ObservableCollection<T>(collection.OrderBy(keySelector).ToList());
        }
    }
}
