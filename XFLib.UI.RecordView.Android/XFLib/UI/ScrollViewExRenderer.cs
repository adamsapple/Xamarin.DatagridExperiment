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
using Android.Animation;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using XFLib.UI;
using XFLib.UI.Droid;

[assembly: ExportRenderer(typeof(ScrollViewEx), typeof(ScrollViewExRenderer))]
namespace XFLib.UI.Droid
{
    /// <summary>
    /// Class ExtendedScrollViewRenderer.
    /// </summary>
    public class ScrollViewExRenderer : ScrollViewRenderer
    {
        /// <summary>
        /// The epsilon.
        /// </summary>
        private const double Epsilon = 0.1;

        public ScrollViewExRenderer(Context context) : base(context)
        {
            this.ViewTreeObserver.ScrollChanged += (sender, ev) => {
                var scrollView = (ScrollViewEx)this.Element;
                if (scrollView == null)
                {
                    return;
                }

                var bounds = new Rectangle(this.ScrollX, this.ScrollY, GetChildAt(0).Width, GetChildAt(0).Height);
                scrollView.UpdateBounds(bounds);
            };
        }

        /// <summary>
        /// Handles the <see cref="E:ElementChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="VisualElementChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);


            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;
            }

            e.NewElement.PropertyChanged += OnElementPropertyChanged;
        }

        /// <summary>
        /// Handles the <see cref="E:ElementPropertyChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //if (ChildCount > 0)
            //{
            //    GetChildAt(0).HorizontalScrollBarEnabled = false;
            //}

            if (e.PropertyName != ScrollViewEx.PositionProperty.PropertyName)
            {
                return;
            }

            var scrollView = (ScrollViewEx)this.Element;
            var position   = scrollView.Position;

            if (Math.Abs((int)(this.ScrollY - position.Y)) < Epsilon
                && Math.Abs((int)(this.ScrollX - position.X)) < Epsilon)
            {
                return;
            }

            ScrollTo((int)position.X, (int)position.Y);
            UpdateLayout();
        }
    }
}