using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms.DataGrid;

namespace ListViewTest01
{
    public class ViewModel : ModelBase
    {
        public ObservableCollection<ListViewItem> Items { get; set; } = new ObservableCollection<ListViewItem>();

        private ListViewItem _selectedItem = null;
        public ListViewItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                Debug.WriteLine(value?.Id);
                SetProperty(ref _selectedItem, value);
            }
        }

        private SortData _sortData;// = new SortData(0, SortingOrder.None);
        public SortData SortData
        {
            get => _sortData;
            set
            {
                SetProperty(ref _sortData, value);
            }
        }

        

        public ViewModel()
        {
            Items.Add(new ListViewItem() { Id = 1, Data01 = "1Data01-aaaa", Data02 = "1Data02-aaaa", Data03 = "1Data03-aaaa", Data04 = "1Data04-aaaa", Data05 = "1Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 2, Data01 = "2Data01-bbbb", Data02 = "2Data02-aaaa", Data03 = "2Data03-aaaa", Data04 = "2Data04-aaaa", Data05 = "2Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 3, Data01 = "3Data01-cccc", Data02 = "3Data02-aaaa", Data03 = "3Data03-aaaa", Data04 = "3Data04-aaaa", Data05 = "3Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 4, Data01 = "4Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 5, Data01 = "5Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 6, Data01 = "6Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 7, Data01 = "7Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 8, Data01 = "8Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 9, Data01 = "9Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 10, Data01 = "10Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 11, Data01 = "11Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 12, Data01 = "12Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 13, Data01 = "13Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 14, Data01 = "14Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 15, Data01 = "15Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 101, Data01 = "1Data01-aaaa", Data02 = "1Data02-aaaa", Data03 = "1Data03-aaaa", Data04 = "1Data04-aaaa", Data05 = "1Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 102, Data01 = "2Data01-bbbb", Data02 = "2Data02-aaaa", Data03 = "2Data03-aaaa", Data04 = "2Data04-aaaa", Data05 = "2Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 103, Data01 = "3Data01-cccc", Data02 = "3Data02-aaaa", Data03 = "3Data03-aaaa", Data04 = "3Data04-aaaa", Data05 = "3Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 104, Data01 = "4Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 105, Data01 = "5Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 106, Data01 = "6Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 107, Data01 = "7Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 108, Data01 = "8Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 109, Data01 = "9Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 110, Data01 = "10Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 111, Data01 = "11Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 112, Data01 = "12Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 113, Data01 = "13Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 114, Data01 = "14Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
            Items.Add(new ListViewItem() { Id = 115, Data01 = "15Data01-dddd", Data02 = "4Data02-aaaa", Data03 = "4Data03-aaaa", Data04 = "4Data04-aaaa", Data05 = "4Data05-aaaa" });
        }
    }
}
