using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ListViewTest01.UI
{
    internal sealed class RecordViewFixedColumnCell : RecordViewCellBase
    {
        #region Fields
        //protected Grid mainLayout;
        //protected Color _bgColor;
        //protected Color _textColor;
        //protected bool _hasSelected;
        #endregion

        protected override void CreateViewCells()
        {
            //var recordView  = RecordView;
            var colId       = 0;
            var mainLayout  = new Grid()
            {
                //BackgroundColor = DataGrid.BorderColor,
                Margin            = new Thickness(0),
                Padding           = new Thickness(0),
                RowSpacing        = 0,
                ColumnSpacing     = 0,
                HeightRequest     = Height,
            };

            RecordView.Columns.Take(1).ToList().ForEach(col =>
            {
                mainLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = col.Width });

                var text = new Label
                {
                    //TextColor = _textColor,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    //LineBreakMode = LineBreakMode.WordWrap,
                };
                text.SetBinding(Label.TextProperty, col.PropertyName);
                
                var cell = new ContentView
                {
                    //Margin  = new Thickness(0),
                    //Padding = new Thickness(0),
                    Content = text,
                };

                mainLayout.BindingContext = RowContext;
                mainLayout.Children.Add(cell);
                Grid.SetColumn(cell, colId);

                colId++;
            });

            View = mainLayout;
        }
    }
}
