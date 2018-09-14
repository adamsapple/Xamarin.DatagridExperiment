using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListViewTest01
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Page4 : ContentPage
	{
		public Page4 ()
		{
			InitializeComponent ();

            var grid = Grid1;
            var viewModel = BindingContext as ViewModel;
            if(viewModel == null)
            {
                return;
            }


            grid.Rows = viewModel.Items.ToList<object>();

        }
	}
}