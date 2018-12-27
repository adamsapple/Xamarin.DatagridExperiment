using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ListViewTest01.UI
{
    public class HideHBarScrollView : ScrollView
    {
        //public static readonly BindableProperty SetScrollEnabledProperty = BindableProperty.Create(
        //                                                                        "SetScrollEnabled",
        //                                                                        typeof(bool),
        //                                                                        typeof(HideHBarScrollView),
        //                                                                        false);

        //public bool SetScrollEnabled
        //{
        //    get => (bool)GetValue(SetScrollEnabledProperty);
        //    set => SetValue(SetScrollEnabledProperty, value);            
        //}
        public HideHBarScrollView()
        {
            InputTransparent = false;
        }
    }
}
