using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ListViewTest01.UI
{
    public class GridViewColumn : BindableObject, IDefinition
    {
        #region Bindable Properties.
        public static readonly BindableProperty WidthProperty =
            BindableProperty.Create(nameof(Width), typeof(double), typeof(GridViewColumn), 0d,
                defaultValueCreator: b => (b as GridViewColumn)?.TitleLabel.Width,
                propertyChanged: (b, o, n) =>
                {
                    if (o != n)
                    {
                        var self = (b as GridViewColumn);
                        self.TitleLabel.WidthRequest = (double)n;
                        self.OnSizeChanged();
                    }
                });

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title), typeof(string), typeof(GridViewColumn), string.Empty,
                propertyChanged: (b, o, n) => { if (o != n) (b as GridViewColumn).TitleLabel.Text = (string)n; });

        //public static readonly BindableProperty FormattedTitleProperty =
        //    BindableProperty.Create(nameof(FormattedTitle), typeof(FormattedString), typeof(RecordViewColumn),
        //        propertyChanged: (b, o, n) => (b as RecordViewColumn).HeaderLabel.FormattedText = (FormattedString)n);

        public static readonly BindableProperty PropertyNameProperty =
            BindableProperty.Create(nameof(PropertyName), typeof(string), typeof(GridViewColumn), string.Empty,
                propertyChanged: (b, o, n) => { if (o != n) (b as GridViewColumn).TitleLabel.ColmnName = (string)n; });

        //public static readonly BindableProperty StringFormatProperty =
        //    BindableProperty.Create(nameof(StringFormat), typeof(string), typeof(RecordViewColumn), null);

        //public static readonly BindableProperty CellTemplateProperty =
        //    BindableProperty.Create(nameof(CellTemplate), typeof(DataTemplate), typeof(RecordViewColumn), null);

        public static readonly BindableProperty HorizontalTextAlignmentProperty =
            BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(GridViewColumn), TextAlignment.Center,
                defaultValueCreator: b => (b as GridViewColumn)?.TitleLabel.HorizontalTextAlignment,
                propertyChanged: (b, o, n) => { if (o != n) (b as GridViewColumn).TitleLabel.HorizontalTextAlignment = (TextAlignment)n; });

        public static readonly BindableProperty VerticalTextAlignmentProperty =
            BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(GridViewColumn), TextAlignment.Center,
                defaultValueCreator: b => (b as GridViewColumn)?.TitleLabel.VerticalTextAlignment,
                propertyChanged: (b, o, n) => { if (o != n) (b as GridViewColumn).TitleLabel.VerticalTextAlignment = (TextAlignment)n; });

        public static readonly BindableProperty HorizontalOptionsProperty =
            BindableProperty.Create(nameof(HorizontalOptions), typeof(LayoutOptions), typeof(GridViewColumn), LayoutOptions.CenterAndExpand,
                defaultValueCreator: b => (b as GridViewColumn)?.TitleLabel.HorizontalOptions,
                propertyChanged: (b, o, n) => { if (o != n) (b as GridViewColumn).TitleLabel.HorizontalOptions = (LayoutOptions)n; });

        public static readonly BindableProperty VerticalOptionsProperty =
            BindableProperty.Create(nameof(VerticalOptions), typeof(LayoutOptions), typeof(GridViewColumn), LayoutOptions.CenterAndExpand,
                defaultValueCreator: b => (b as GridViewColumn)?.TitleLabel.VerticalOptions,
                propertyChanged: (b, o, n) => { if (o != n) (b as GridViewColumn).TitleLabel.VerticalOptions = (LayoutOptions)n; });

        //public static readonly BindableProperty SortingEnabledProperty =
        //    BindableProperty.Create(nameof(SortingEnabled), typeof(bool), typeof(RecordViewColumn), true);

        //public static readonly BindableProperty HeaderLabelStyleProperty =
        //    BindableProperty.Create(nameof(HeaderLabelStyle), typeof(Style), typeof(RecordViewColumn),
        //        propertyChanged: (b, o, n) =>
        //        {
        //            if ((b as RecordViewColumn)?.HeaderLabel != null && (o != n))
        //                (b as RecordViewColumn).HeaderLabel.Style = n as Style;
        //        });

        #endregion Bindable Properties.

        #region Properties.

        public double Width
        {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        //public FormattedString FormattedTitle
        //{
        //    get { return (string)GetValue(FormattedTitleProperty); }
        //    set { SetValue(FormattedTitleProperty, value); }
        //}

        public string PropertyName
        {
            get { return (string)GetValue(PropertyNameProperty); }
            set { SetValue(PropertyNameProperty, value); }
        }

        //public string StringFormat
        //{
        //    get { return (string)GetValue(StringFormatProperty); }
        //    set { SetValue(StringFormatProperty, value); }
        //}

        //public DataTemplate CellTemplate
        //{
        //    get { return (DataTemplate)GetValue(CellTemplateProperty); }
        //    set { SetValue(CellTemplateProperty, value); }
        //}

        internal ClickableLabel TitleLabel { get; set; }

        public TextAlignment HorizontalTextAlignment
        {
            get { return (TextAlignment)GetValue(HorizontalTextAlignmentProperty); }
            set { SetValue(HorizontalTextAlignmentProperty, value); }
        }

        public TextAlignment VerticalTextAlignment
        {
            get { return (TextAlignment)GetValue(VerticalTextAlignmentProperty); }
            set { SetValue(VerticalTextAlignmentProperty, value); }
        }

        public LayoutOptions HorizontalOptions
        {
            get { return (LayoutOptions)GetValue(HorizontalOptionsProperty); }
            set { SetValue(HorizontalOptionsProperty, value); }
        }

        public LayoutOptions VerticalOptions
        {
            get { return (LayoutOptions)GetValue(VerticalOptionsProperty); }
            set { SetValue(VerticalOptionsProperty, value); }
        }

        //public bool SortingEnabled
        //{
        //    get { return (bool)GetValue(SortingEnabledProperty); }
        //    set { SetValue(SortingEnabledProperty, value); }
        //}

        //public Style HeaderLabelStyle
        //{
        //    get { return (Style)GetValue(HeaderLabelStyleProperty); }
        //    set { SetValue(HeaderLabelStyleProperty, value); }
        //}
        #endregion Properties.

        #region imprements IDefinition.
        public event EventHandler SizeChanged;
        #endregion imprements IDefinition.

        public GridViewColumn()
        {
            TitleLabel = new ClickableLabel
            {
                Margin = new Thickness(0),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
        }

        void OnSizeChanged()
        {
            SizeChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
