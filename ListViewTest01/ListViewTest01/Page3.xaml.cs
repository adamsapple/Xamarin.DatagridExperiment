using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Forms.Controls;

namespace ListViewTest01
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page3 : ContentPage
	{
        ViewModel viewModel;

        public Page3 ()
		{
            InitializeComponent();

            if (BindingContext is ViewModel vm)
            {
                viewModel = vm;

                var collection = viewModel.Items.ToList()
                                        .OrderBy(i => Guid.NewGuid())
                                        .ToArray();

                listView00.SortData.Type = typeof(ListViewItem);
                //listView01.SortData.Type = typeof(ListViewItem);
                listView00.AddSlave(listView01);


                //listView00.Refresh();

                var sortData = listView00.SortData;
                for (var i=0; i<10; i++)
                {
                    var obj = this.FindByName<ClickableLabel>("col" + i);
                    if (obj == null)
                    {
                        break;
                    }
                    if (obj.ColmnName.Equals(sortData.Name))
                    {
                        obj.SortingOrder = sortData.Order;
                    }
                    else
                    {
                        obj.SortingOrder = SortingOrder.None;
                    }
                }
                
            }
		}

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void ScrollViewH_Scrolled(ScrollViewEx sender, Rectangle rect)
        {
            System.Diagnostics.Debug.WriteLine(""+ rect.X+ ","+ rect.Y);
        }

        private void ClickableLabel_Clicked(object sender, EventArgs e)
        {
            if(viewModel == null)
            {
                return;
            }

            string colmn = null;

            if (sender is ClickableLabel label)
            {
                colmn = label.ColmnName;
            }


            if (colmn == null)
            {
                return;
            }

            var sortData = listView00.SortData;

            //items.Add(new ListViewItem() { Id = 116, Data01 = "16-dddd", Data02 = "16-aaaa", Data03 = "16-aaaa", Data04 = "16-aaaa", Data05 = "16-aaaa" });
            if (colmn.Equals(sortData.Name))
            {
                var val        = ((int)sortData.Order + 1) % 3;
                sortData.Order = (SortingOrder)Enum.ToObject(typeof(SortingOrder), val);
            }
            else
            {
                sortData.Name  = colmn;
                sortData.Order = SortingOrder.Ascendant;
            }

            
            //listView01.SortData.Name = text;
            listView00.Sort();

            for (var i = 0; i < 10; i++)
            {
                var obj = this.FindByName<ClickableLabel>("col" + i);
                if (obj == null)
                {
                    break;
                }
                if (sortData.Name.Equals(obj.ColmnName))
                {
                    obj.SortingOrder = sortData.Order;
                }
                else
                {
                    obj.SortingOrder = SortingOrder.None;
                }
            }

            /*
            switch (text)
            {
                case "Id": viewModel.Items = items.Sort(x => x.Id); break;
                case "Data01": viewModel.Items = items.Sort(x => x.Data01); break;
                case "Data02": viewModel.Items = items.Sort(x => x.Data02); break;
                case "Data03": viewModel.Items = items.Sort(x => x.Data03); break;
                case "Data04": viewModel.Items = items.Sort(x => x.Data04); break;
                case "Data05": viewModel.Items = items.Sort(x => x.Data05); break;
            }
            */
        }
    }
}