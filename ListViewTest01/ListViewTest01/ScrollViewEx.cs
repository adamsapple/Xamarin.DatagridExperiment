using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ListViewTest01
{
    public class ScrollViewEx : ScrollView
    {
        public new event Action<ScrollViewEx, Rectangle> Scrolled;

        public void UpdateBounds(Rectangle bounds)
        {
            Position = bounds.Location;
            Scrolled?.Invoke(this, bounds);
        }
        
        public static readonly BindableProperty PositionProperty =
            BindableProperty.Create(
                "Position",
                typeof(Point),
                typeof(ScrollViewEx),
                default(Point),
                BindingMode.TwoWay,
                propertyChanged: null,
                propertyChanging: null);
        //validateValue: null,
        //coerceValue: null);


        //    //BindableProperty.Create<ScrollViewWithEvents, Point>(
        //    //    p => p.Position, default(Point));

        //    public static readonly BindableProperty AnimateScrollProperty =
        //        BindableProperty.Create(
        //            "AnimateScroll",
        //            typeof(bool),
        //            typeof(ScrollViewEx),
        //            null);

        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        //    public bool AnimateScroll
        //    {
        //        get { return (bool)GetValue(AnimateScrollProperty); }
        //        set { SetValue(AnimateScrollProperty, value); }
        //    }
    }
}
