using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ListViewTest01.UI
{
    public class ClickableLabel : Label
    {
        protected TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();

        public event EventHandler Clicked;

        /// <summary>
        /// The item template property.
        /// </summary>
        public static readonly BindableProperty ColmnNameProperty =
                BindableProperty.Create(
                    nameof(ColmnNameProperty),
                    typeof(string),
                    typeof(ClickableLabel),
                    "",
                    propertyChanged: (bindable, value, newValue) =>
                    {
                        if (bindable is ClickableLabel self)
                        {
                            self.Text = ((string)newValue) + SortingOrderToString(self.SortingOrder);
                        }
                    }
                );

        /// <summary>
        /// Gets or sets the item template.
        /// </summary>
        /// <value>The item template.</value>
        public string ColmnName
        {
            get => (string)this.GetValue(ColmnNameProperty);
            set => this.SetValue(ColmnNameProperty, value);
        }

        /// <summary>
        /// The item template property.
        /// </summary>
        public static readonly BindableProperty SortingOrderProperty =
                BindableProperty.Create(
                    nameof(SortingOrderProperty),
                    typeof(SortingOrder),
                    typeof(ClickableLabel),
                    SortingOrder.None,
                    propertyChanged: (bindable, value, newValue) =>
                    {
                        if (bindable is ClickableLabel self)
                        {
                            self.Text = self.ColmnName + SortingOrderToString((SortingOrder)newValue);
                        }
                    }
                );
        
        /// <summary>
        /// Gets or sets the item template.
        /// </summary>
        /// <value>The item template.</value>
        public SortingOrder SortingOrder
        {
            get => (SortingOrder)this.GetValue(SortingOrderProperty);
            set => this.SetValue(SortingOrderProperty, value);
        }

        private static string SortingOrderToString(SortingOrder order)
        {
            string[] arr = { "", "▲", "▼" };

            return arr[(int)order];
        }

        public ClickableLabel() : base()
        {
            this.GestureRecognizers.Add(tapGestureRecognizer);

            tapGestureRecognizer.Tapped += (sender, e) => Clicked?.Invoke(sender, e);
        }
    }
}
