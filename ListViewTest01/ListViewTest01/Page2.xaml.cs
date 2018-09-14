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
	public partial class Page2 : ContentPage
	{
		public Page2 ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void ScrollView1_ScrollToRequested(object sender, ScrollToRequestedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.ScrollX);
            
        }
    }
}