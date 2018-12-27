using System;
using System.Collections;
using Xamarin.Forms;

namespace ListViewTest01.UI
{
    /// <summary>
    /// Class GridView.
    /// </summary>
    public class GridView : View
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GridView"/> class.
        /// </summary>
        public GridView()
        {
            SelectionEnabled = true;
        }

        //
        // Static Fields
        //
        /// <summary>
        /// The items source property
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(GridView), null, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// The item template property
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(GridView), null, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// The row spacing property
        /// </summary>
        public static readonly BindableProperty RowSpacingProperty = BindableProperty.Create(nameof(RowSpacing), typeof(double), typeof(GridView), 0.0, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// The column spacing property
        /// </summary>
        public static readonly BindableProperty ColumnSpacingProperty = BindableProperty.Create(nameof(ColumnSpacing), typeof(double), typeof(GridView), 0.0, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// The item width property
        /// </summary>
        public static readonly BindableProperty ItemWidthProperty = BindableProperty.Create(nameof(ItemWidth), typeof(double), typeof(GridView), 100.0, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// The item height property
        /// </summary>
        public static readonly BindableProperty ItemHeightProperty = BindableProperty.Create(nameof(ItemHeight), typeof(double), typeof(GridView), 100.0, BindingMode.OneWay, null, null, null, null);

        /// <summary>
        /// The padding property
        /// </summary>
        public static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(GridView), new Thickness(0), BindingMode.OneWay, null, null, null, null);

        //
        // Properties
        //
        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)GetValue(GridView.ItemsSourceProperty);
            set => SetValue(GridView.ItemsSourceProperty, value);
        }

        /// <summary>
        /// Gets or sets the item template.
        /// </summary>
        /// <value>The item template.</value>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(GridView.ItemTemplateProperty);
            set => SetValue(GridView.ItemTemplateProperty, value);
        }

        /// <summary>
        /// Gets or sets the row spacing.
        /// </summary>
        /// <value>The row spacing.</value>
        public double RowSpacing
        {
            get => (double)GetValue(GridView.RowSpacingProperty);
            set => SetValue(GridView.RowSpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the column spacing.
        /// </summary>
        /// <value>The column spacing.</value>
        public double ColumnSpacing
        {
            get => (double)base.GetValue(GridView.ColumnSpacingProperty);
            set => SetValue(GridView.ColumnSpacingProperty, value);
        }

        /// <summary>
        /// Gets or sets the width of the item.
        /// </summary>
        /// <value>The width of the item.</value>
        public double ItemWidth
        {
            get => (double)base.GetValue(GridView.ItemWidthProperty);
            set => SetValue(GridView.ItemWidthProperty, value);
        }

        /// <summary>
        /// Gets or sets the height of the item.
        /// </summary>
        /// <value>The height of the item.</value>
        public double ItemHeight
        {
            get => (double)base.GetValue(GridView.ItemHeightProperty);
            set => SetValue(GridView.ItemHeightProperty, value);
        }

        /// <summary>
        /// Gets or sets the padding.
        /// </summary>
        /// <value>The padding.</value>
        public Thickness Padding
        {
            get => (Thickness)base.GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

        /// <summary>
        /// Occurs when item is selected.
        /// </summary>
        public event EventHandler<EventArgs<object>> ItemSelected;

        /// <summary>
        /// Invokes the item selected event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="item">Item.</param>
        public void InvokeItemSelectedEvent(object sender, object item)
        {
            ItemSelected?.Invoke(sender, new EventArgs<object>(item));
        }

        /// <summary>
        /// Gets or sets a value indicating whether [selection enabled].
        /// </summary>
        /// <value><c>true</c> if [selection enabled]; otherwise, <c>false</c>.</value>
        public bool SelectionEnabled
        {
            get;
            set;
        }
    }
}