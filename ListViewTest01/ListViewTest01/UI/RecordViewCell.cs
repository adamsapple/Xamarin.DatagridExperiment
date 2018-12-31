using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ListViewTest01.UI
{
    public class RecordViewCell : ViewCell
    {
        public new double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        public bool IsFixedColumn
        {
            get { return (bool)GetValue(IsFixedColumnProperty); }
            set { SetValue(IsFixedColumnProperty, value); }
        }

        public IList<RecordViewColumn> Columns
        {
            get { return (IList<RecordViewColumn>)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        public static readonly BindableProperty HeightProperty =
            BindableProperty.Create(nameof(Height), typeof(double), typeof(RecordViewCell), 0d);

        public static readonly BindableProperty IsFixedColumnProperty =
            BindableProperty.Create(nameof(IsFixedColumn), typeof(bool), typeof(RecordViewCell), false);

        public static readonly BindableProperty ColumnsProperty =
            BindableProperty.Create(nameof(Columns), typeof(RecordViewColumnCollection), typeof(RecordViewCell), null,
                propertyChanged: (b,o,n) => (b as RecordViewCell)?.CreateContent());

        

        public RecordViewCell()
        {
        }

        public void CreateContent()
        {
            var view = new StackLayout
            {
                Spacing         = 0,
                Padding         = new Thickness(0),
                Margin          = new Thickness(0),
                Orientation     = StackOrientation.Horizontal,
                //VerticalOptions = LayoutOptions.FillAndExpand,
            };
            
            Columns?.Where(x => x.IsFixedColumn == IsFixedColumn).ToList().ForEach(x =>
            {
                var label = new Label
                {
                    LineBreakMode           = LineBreakMode.NoWrap,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment   = TextAlignment.Center,
                    //HorizontalOptions       = LayoutOptions.FillAndExpand,
                };
                label.SetBinding(Label.TextProperty, x.PropertyName);
                label.SetBinding(Label.WidthRequestProperty, new Binding("Width", source: x));
                label.SetBinding(Label.HeightRequestProperty, new Binding("Height", source: this));

                view.Children.Add(label);
            });

            View = new ContentView
            {
                Content = view
            };
        }
    }
}
