using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ListViewTest01.UI
{
    /// <summary>
    /// RecordView用のCell
    /// </summary>
    [ContentProperty("View")]
    internal abstract class RecordViewCellBase : ViewCell
    {
        #region Properties.
        public RecordView RecordView
        {
            get { return (RecordView)GetValue(RecordViewProperty); }
            set { SetValue(RecordViewProperty, value); }
        }

        public object RowContext
        {
            get { return (object)GetValue(RowContextProperty); }
            set { SetValue(RowContextProperty, value); }
        }

        public IList<RecordViewColumn> Columns
        {
            get { return (IList<RecordViewColumn>)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }


        public new double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        //public abstract View View { get; set; }
        #endregion Properties.

        #region Bindable Properties.
        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty RecordViewProperty =
            BindableProperty.Create(nameof(RecordView), typeof(RecordView), typeof(RecordViewCellBase), null,
                propertyChanged: (b, o, n) => (b as RecordViewCellBase).CreateViewCells());

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty RowContextProperty =
            BindableProperty.Create(nameof(RowContext), typeof(object), typeof(RecordViewCellBase),
                propertyChanged: (b, o, n) => (b as RecordViewCellBase).CreateViewCells());

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty ColumnsProperty =
            BindableProperty.Create(nameof(Columns), typeof(RecordViewColumnCollection), typeof(RecordView), null,
                propertyChanged: (b, o, n) => (b as RecordViewCellBase).CreateViewCells());

        /// <summary>
        /// 
        /// </summary>
        public static readonly BindableProperty HeightProperty =
            BindableProperty.Create(nameof(Height), typeof(double), typeof(RecordViewCellBase), 0d);
        #endregion Bindable Properties.

        #region Fields
        //protected Grid mainLayout;
        //protected Color _bgColor;
        //protected Color _textColor;
        //protected bool _hasSelected;
        #endregion

        #region Methods.
        /// <summary>
        /// Cell構築
        /// </summary>
        protected abstract void CreateViewCells();
        //{
        //    mainLayout = new Grid()
        //    {
        //        //BackgroundColor = DataGrid.BorderColor,
        //        Margin        = new Thickness(0),
        //        Padding       = new Thickness(0),
        //        RowSpacing    = 0,
        //        ColumnSpacing = 0,
        //    };

        //    foreach (var col in RecordView.Columns)
        //    {
        //        mainLayout.ColumnDefinitions.Add(new ColumnDefinition() { Width = col.Width });
        //        View cell;

        //        var text = new Label
        //        {
        //            //TextColor = _textColor,
        //            //HorizontalOptions = col.HorizontalContentAlignment,
        //            //VerticalOptions = col.VerticalContentAlignment,
        //            //LineBreakMode = LineBreakMode.WordWrap,
        //        };
        //        text.SetBinding(Label.TextProperty, new Binding(col.PropertyName, BindingMode.Default));

        //        cell = new ContentView
        //        {
        //            Padding         = 0,
        //            BackgroundColor = _bgColor,
        //            Content         = text,
        //        };

        //        mainLayout.Children.Add(cell);
        //        Grid.SetColumn(cell, RecordView.Columns.IndexOf(col));
        //    }

        //    View = mainLayout;
        //}

        /// <summary>
        /// 
        /// </summary>
        protected void BackgroundColorChanged()
        {
            //_hasSelected = DataGrid.SelectedItem == RowContext;
            //int actualIndex = DataGrid?.InternalItems?.IndexOf(BindingContext) ?? -1;
            //if (actualIndex > -1)
            //{
            //    _bgColor = (DataGrid.SelectionEnabled && DataGrid.SelectedItem != null && DataGrid.SelectedItem == RowContext) ?
            //        DataGrid.ActiveRowColor : DataGrid.RowsBackgroundColorPalette.GetColor(Index, BindingContext);
            //    _textColor = DataGrid.RowsTextColorPalette.GetColor(actualIndex, BindingContext);

            //    ChangeColor(_bgColor);
            //}
        }
        #endregion Methods.
    }
}
