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
using ListViewTest01;
using Android.Animation;

[assembly: ExportRenderer(typeof(ScrollViewEx), typeof(ListViewTest01.Droid.ScrollViewExRenderer))]
namespace ListViewTest01.Droid
{
    public class ScrollViewExRenderer : ScrollViewRenderer
    {
        public ScrollViewExRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            //this.OnScrollChanged += (i, t, oldl, oldt) =>
            //{

            //};
            //this.Scrolled += (sender, ev) =>
            //{
            //    ScrollViewWithEvents sv = (ScrollViewWithEvents)Element;
            //    sv.UpdateBounds(this.Bounds.ToRectangle());
            //};

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;
            e.NewElement.PropertyChanged += OnElementPropertyChanged;
        }

        readonly double EPSILON = 0.1;

        protected void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (e.PropertyName == Xamarin.Forms.ScrollView.PositionProperty.PropertyName)
            {
                //ScrollView sv = (ScrollView)Element;
                //Xamarin.Forms.Point pt = sv.Position;

                //if (System.Math.Abs(this.Bounds.Location.Y - pt.Y) < EPSILON
                //    && System.Math.Abs(this.Bounds.Location.X - pt.X) < EPSILON)
                //    return;

                //this.ScrollRectToVisible(
                //    new RectangleF((float)pt.X, (float)pt.Y, Bounds.Width, Bounds.Height), false);
            }
        }
    }
}