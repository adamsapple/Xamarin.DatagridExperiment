using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using ListViewTest01.UI;
using ListViewTest01.UI.Droid;


[assembly: ExportRenderer(typeof(HideHBarScrollView), typeof(HideHBarScrollViewRenderer))]
namespace ListViewTest01.UI.Droid
{
    public class HideHBarScrollViewRenderer : ScrollViewRenderer
    {
        public HideHBarScrollViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }
                
            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;
            }

            e.NewElement.PropertyChanged += OnElementPropertyChanged;
        }

        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //for IOS
            //ShowsHorizontalScrollIndicator = false;
            //ShowsVerticalScrollIndicator = false;

            if (ChildCount > 0)
            {
                var child = GetChildAt(0);
                child.HorizontalScrollBarEnabled = false;
                child.VerticalScrollBarEnabled   = false;
            }
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            return false;   // return base.OnTouchEvent (e);
        }
        public override bool OnInterceptTouchEvent(Android.Views.MotionEvent ev)
        {
            return false;   // base.OnInterceptTouchEvent(ev);
        }
    }
}