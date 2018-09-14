using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.DataGrid;
using Xamarin.Forms.Xaml;

namespace ListViewTest01
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page1 : ContentPage
	{
		public Page1 ()
		{
			InitializeComponent ();

            //DataGrid2.PropertyChanging += DataGrid2_PropertyChanging;
            DataGrid2.PropertyChanged += DataGrid2_PropertyChanged;
        }

        private void DataGrid2_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("e.PropertyName:" + e.PropertyName);
            //if (e.PropertyName == "SortedColumnIndex")
            //{
            //    if (sender is DataGrid src)
            //    {
            //        var dst = DataGrid1;
            //        var orig = src.SortedColumnIndex;
            //        if (orig != null)
            //        {
            //            var order = new SortData(orig.Index, orig.Order);
            //            dst.SortedColumnIndex = order;
            //        }
            //    }
            //}
        }

        private void DataGrid2_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine("e.PropertyName:"+ e.PropertyName);
            //if (e.PropertyName == "SortedColumnIndex")
            //{
            //    if (sender is DataGrid src)
            //    {
            //        var dst = DataGrid1;
            //        var orig = src.SortedColumnIndex;
            //        if(orig != null)
            //        {
            //            var order = new SortData(orig.Index, orig.Order);
            //            dst.SortedColumnIndex = order;
            //        }
            //    }
            //}
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //var a = DataGrid1.Columns.ElementAt(0).Width;
            //DataGrid1.Columns.ElementAt(0).Width = new GridLength(a.Value+1);

            //Grid1.ColumnDefinitions[0].Width = new GridLength(a.Value + 10);
        }

        private void DataGrid1_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (sender is DataGrid src)
            {
                var dst = DataGrid2;
                if(e.SelectedItem is ListViewItem item)
                {
                    //dst.SelectedItem = item;
                }

                //grid.SelectedItem = null;
                //grid.SelectedItem = null;
            }
        }
        private void DataGrid2_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (sender is DataGrid grid)
            {
                //grid.SelectedItem = null;
                //grid.SelectedItem = null;
            }
        }
    }
}