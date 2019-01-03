using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XFLib.UI
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
            
            Columns?.Where(x => x.IsFixedColumn == IsFixedColumn).ToList()
            .ForEach(column =>
            {
                View cell = null;

                if (column.ColumnTemplate != null)
                {
                    cell = new ContentView
                    {
                        Content = column.ColumnTemplate.CreateContent() as View
                    };
                    if (column.PropertyName != null)
                    {
                        //cell.SetBinding(BindingContextProperty,
                        //    new Binding(column.PropertyName, source: column));
                    }
                }
                else
                {
                    cell = new Label
                    {
                        LineBreakMode           = LineBreakMode.NoWrap,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment   = TextAlignment.Center,
                        //HorizontalOptions     = LayoutOptions.FillAndExpand,
                    };
                    cell.SetBinding(Label.TextProperty, column.PropertyName);
                }

                cell.SetBinding(VisualElement.WidthRequestProperty, new Binding("Width", source: column));
                cell.SetBinding(VisualElement.HeightRequestProperty, new Binding("Height", source: this));
                view.Children.Add(cell);
            });

            View = new ContentView
            {
                Content = view
            };
        }
    }
}
