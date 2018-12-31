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
	public partial class Page5 : ContentPage
	{
		public Page5 ()
		{
			InitializeComponent ();

            recordView.ItemSelected += (s, e) =>
            {
                System.Diagnostics.Debug.WriteLine(e.SelectedItem?.ToString());
            };


            recordView.SelectedItem = recordView.ItemsSource.Cast<ListViewItem>().Where(x => x.Id == 13).FirstOrDefault();
        }
	}
}