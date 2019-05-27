using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
//using Xamarin.Forms.DataGrid;

namespace ListViewTest01
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private void DataGrid1_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //if(sender is DataGrid grid)
            //{
            //    grid.SelectedItem = null;
            //    //grid.SelectedItem = null;
            //}
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var columns = DataGrid1.Columns;
            // カラムを後から足すのは非対応だった
            //columns.Add(new DataGridColumn()
            //{
            //    Title        = "hoge",
            //    PropertyName = "Data01",
            //    Width        = 200
            //});
            //ScrollView1.RaiseChild(DataGrid1);
            //DataGrid1.Columns = columns;
            //DataGrid1.WidthRequest = DataGrid1.Width + 200;

            var cols = columns.Where(x => x.Title == "Sector1");

            foreach(var col in cols)
            {
                col.Width = 100;
                //DataGrid1.Columns = columns;
                //var items = DataGrid1.ColumnDefinitions;
            }
            DataGrid1.WidthRequest = DataGrid1.Width + 100;

            if (BindingContext is ViewModel viewModel)
            {
                var id = viewModel.Items.Count + 1;
                viewModel.Items.Add(new ListViewItem() { Id = id, Data01 = id+"-01-aaaa", Data02 = id + "-02-bbbb", Data03 = id + "-03-cccc", Data04 = id + "-04-dddd", Data05 = id + "-05-eeee" });
            }
        }
        private void AddColumn(object sender, EventArgs e)
        {
            var columns = DataGrid1.Columns;
            // カラムを後から足すのは非対応だった
            //columns.Add(new DataGridColumn()
            //{
            //    Title = "hoge",
            //    PropertyName = "Data01",
            //    Width = 200
            //});
            //ScrollView1.RaiseChild(DataGrid1);
            //DataGrid1.Columns = columns;
            //DataGrid1.WidthRequest = DataGrid1.Width + 200;
            if (BindingContext is ViewModel viewModel)
            {
                var id = viewModel.Items.Count + 1;
                viewModel.Items.Add(new ListViewItem() { Id = id, Data01 = id + "-01-aaaa", Data02 = id + "-02-bbbb", Data03 = id + "-03-cccc", Data04 = id + "-04-dddd", Data05 = id + "-05-eeee" });
            }
        }
    }
}
