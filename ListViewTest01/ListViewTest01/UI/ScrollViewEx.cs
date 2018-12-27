using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ListViewTest01.UI
{
    public class ScrollViewEx : ScrollView
    {
        public new event Action<ScrollViewEx, Rectangle> Scrolled;

        /// <summary>
        /// Updates the bounds.
        /// </summary>
        /// <param name="bounds">The bounds.</param>
        public void UpdateBounds(Rectangle bounds)
        {
            Position = bounds.Location;
            Scrolled?.Invoke(this, bounds);
        }

        #region Position Property.
        /// <summary>
        /// The position property
        /// </summary>
        public static readonly BindableProperty PositionProperty =
            BindableProperty.Create(
                "Position",
                typeof(Point),
                typeof(ScrollViewEx),
                default(Point),
                BindingMode.TwoWay,
                propertyChanged: null,
                propertyChanging: null);

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }
        #endregion Position Property.

        #region AnimateScroll Property.
        /// <summary>
		/// The animate scroll property
		/// </summary>
		public static readonly BindableProperty AnimateScrollProperty =
            BindableProperty.Create(
                    "AnimateScroll",
                    typeof(bool),
                    typeof(ScrollViewEx),
                    false,
                    BindingMode.TwoWay,
                    propertyChanged: null,
                    propertyChanging: null);

        /// <summary>
        /// Gets or sets a value indicating whether [animate scroll].
        /// </summary>
        /// <value><c>true</c> if [animate scroll]; otherwise, <c>false</c>.</value>
        public bool AnimateScroll
        {
            get { return (bool)GetValue(AnimateScrollProperty); }
            set { SetValue(AnimateScrollProperty, value); }
        }
        #endregion AnimateScroll Property.
    }
}
