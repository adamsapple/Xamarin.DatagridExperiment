using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListViewTest01.UI
{
    /// <summary>
    /// 
    /// </summary>
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecordView : ContentView
	{
        #region Bindable Properties.
        public static readonly BindableProperty SelecctedRowColorProperty =
            BindableProperty.Create(nameof(SelecctedRowColor), typeof(Color), typeof(RecordView), Color.Orange);

        public static readonly BindableProperty HeaderBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderBackgroundColor), typeof(Color), typeof(RecordView), Color.Azure);

        public static readonly BindableProperty EvenRowBackgroundColorProperty = 
            BindableProperty.Create(nameof(EvenRowBackgroundColor), typeof(Color), typeof(RecordView), Color.FromRgba(0, 0, 0, 0.04), propertyChanged: (b,o,n)=>(b as RecordView)?.masterListView.UpdateBackgroundColor());

        public static readonly BindableProperty FixedColumnBackgroundColorProperty =
            BindableProperty.Create(nameof(FixedColumnBackgroundColor), typeof(Color), typeof(RecordView), Color.AliceBlue);
        
        public static readonly BindableProperty ColumnsProperty =
            BindableProperty.Create(nameof(Columns), typeof(RecordViewColumnCollection), typeof(RecordView),
                //propertyChanged    : (b, o, n) => (b as RecordView).InitHeaderView(),
                defaultValueCreator: b => { return (b as RecordView)?._columns; }
        );

        public static readonly BindableProperty HeaderHeightProperty =
            BindableProperty.Create(nameof(HeaderHeight), typeof(double), typeof(RecordView), 50d,
                propertyChanged: (b, o, n) => (b as RecordView).Columns.ToList().ForEach(x => x.TitleLabel.HeightRequest = (double)n));


        public static readonly BindableProperty RowHeightProperty =
            BindableProperty.Create(nameof(RowHeight), typeof(double), typeof(RecordView), 40d);

        public static readonly BindableProperty SelectionEnabledProperty =
            BindableProperty.Create(nameof(SelectionEnabled), typeof(bool), typeof(RecordView), true,
                propertyChanged: (b, o, n) => {
                    var self = b as RecordView;
                    if (!self.SelectionEnabled && self.masterListView.SelectedItem != null)
                    {
                        self.masterListView.SelectedItem = null;
                    }
                });

        public static readonly BindableProperty NoDataViewProperty =
            BindableProperty.Create(nameof(NoDataView), typeof(View), typeof(RecordView),
                propertyChanged: (b, o, n) => {
                    if (o != n)
                    {
                        (b as RecordView).noDataView.Content = n as View;
                    }
                });

　      public bool SelectionEnabled
        {
            get { return (bool)GetValue(SelectionEnabledProperty); }
            set { SetValue(SelectionEnabledProperty, value); }
        }

        public Color SelecctedRowColor
        {
            get { return (Color)GetValue(SelecctedRowColorProperty); }
            set { SetValue(SelecctedRowColorProperty, value); }
        }

        public Color HeaderBackgroundColor
        {
            get { return (Color)GetValue(HeaderBackgroundColorProperty); }
            set { SetValue(HeaderBackgroundColorProperty, value); }
        }

        public Color EvenRowBackgroundColor
        {
            get => (Color)this.GetValue(EvenRowBackgroundColorProperty);
            set => this.SetValue(EvenRowBackgroundColorProperty, value);
        }

        public Color FixedColumnBackgroundColor
        {
            get { return (Color)GetValue(FixedColumnBackgroundColorProperty); }
            set { SetValue(FixedColumnBackgroundColorProperty, value); }
        }
        //public string FontFamily
        //{
        //    get { return (string)GetValue(FontFamilyProperty); }
        //    set { SetValue(FontFamilyProperty, value); }
        //}

        public IList<RecordViewColumn> Columns
        {
            get { return (IList<RecordViewColumn>)GetValue(ColumnsProperty); }
            //set { SetValue(ColumnsProperty, value); }
        }

        public double RowHeight
        {
            get { return (double)GetValue(RowHeightProperty); }
            set { SetValue(RowHeightProperty, value); }
        }

        public double HeaderHeight
        {
            get { return (double)GetValue(HeaderHeightProperty); }
            set { SetValue(HeaderHeightProperty, value); }
        }


        public View NoDataView
        {
            get { return (View)GetValue(NoDataViewProperty); }
            set { SetValue(NoDataViewProperty, value); }
        }
        #endregion Bindable Properties.

        #region Properties.
        /// <summary>
        /// Headerを格納してるElement
        /// </summary>
        Element HeaderElement { get; }

        /// <summary>
        /// データ行を格納しているElement
        /// </summary>
        Element BodyElement   { get; }

        #endregion Properties.

        #region Members.
        private List<ClickableLabel> headers = new List<ClickableLabel>();
        private RecordViewColumnCollection _columns = new RecordViewColumnCollection();
        #endregion Members.

        private ICommand TapCommand { get; }

        public RecordView()
        {
            InitializeComponent();
#if true
            masterListView.SortData.Type = typeof(ListViewItem);
            masterListView.AddSlave(slaveListView);
            //listView00.Refresh();

            // headersにヘッダカラムを追加していく
            //headers.Add(Columns[0].TitleLabel);
            //headers.Add(col1);
            //headers.Add(col2);
            //headers.Add(col3);
            //headers.Add(col4);
            //headers.Add(col5);

            HeaderElement     = HeaderGrid;
            BodyElement       = BodyGrid;

            TapCommand = new Command(x =>
            {
                System.Diagnostics.Debug.WriteLine(x);
                TapRow(x);
            });

            _columns.CollectionChanged += (s,e) => InitHeaderView();

            HeaderScrollViewH.IsEnabled = false;

            //HeaderScrollViewH.Scrolled += (s, r) => 
            //{
            //};
            BodyScrollViewH.Scrolled += async(s, r) =>
            {
                //var pos = BodyScrollViewH.Position;

                //pos.X = r.X;
                await HeaderScrollViewH.ScrollToAsync(s.ScrollX, 0, false);
                //HeaderScrollViewH.SetScrolledPosition(s.ScrollX, 0);
                //HeaderScrollViewH.Position = pos;
                //HeaderScrollViewH.;
                //System.Diagnostics.Debug.WriteLine($"BodyScrollViewH.Scrolled pos.X={r.X}");
                //System.Diagnostics.Debug.WriteLine($"{ Test00.Width},  { Test01.Width}");
               
            };
#endif
        }

        private void UpdateRowSize()
        {
            var col0s = masterListView.Children;
            var cols  = slaveListView.Children;



            var binding = new Binding("Width", BindingMode.OneWay, source: Columns[0]);

            col0s[0].SetBinding(View.WidthRequestProperty, binding);
        }

        private void InitHeaderView()
        {
            var columns = Columns;

            if (!columns.Any())
            {
                return;
            }

            columns.Take(1)
                   .ToList()
                   .ForEach(x => 
                   {
                       HeaderGrid.ColumnDefinitions[0].Width = x.Width;
                       BodyGrid.ColumnDefinitions[0].Width   = x.Width;
                   });

            var height = HeaderHeight;
            columns.Select(x => x.TitleLabel)
                   .Where(x => x.HeightRequest != height)
                   .ToList()
                   .ForEach(x => x.HeightRequest = height);

            headers.Clear();
            floatedHeaderEntryPoint.Children.Clear();

            columns.Take(1)
                   .ToList()
                   .ForEach(x => {
                       headers.Add(x.TitleLabel);
                       fixedHeaderEntryPoint.Content = x.TitleLabel;
                   });

            columns.Skip(1)
                   .ToList()
                   .ForEach(x => {
                       headers.Add(x.TitleLabel);
                       floatedHeaderEntryPoint.Children.Add(x.TitleLabel);
                   });
            
            //
            // ソートラベルの描画状態を更新
            //
            var sortData = masterListView.SortData;
            foreach (var header in headers)
            {
                if (header.ColmnName.Equals(sortData.Name))
                {
                    header.SortingOrder = sortData.Order;
                }
                else
                {
                    header.SortingOrder = SortingOrder.None;
                }
                
                header.Clicked -= HeaderColumn_Clicked;
                header.Clicked += HeaderColumn_Clicked;
            }
        }
        /// <summary>
        /// ヘッダの押下処理。
        /// データのソート方法を変更する。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeaderColumn_Clicked(object sender, EventArgs e)
        {
            // データが無ければ終了
            if (BindingContext == null)
            {
                return;
            }

            string colmn = null;

            if (sender is ClickableLabel label)
            {
                colmn = label.ColmnName;
            }

            // ソートカラムに指定が無ければ終了
            if (colmn == null)
            {
                return;
            }

            var sortData = masterListView.SortData;

            //
            // 押下したヘッダによる処理
            // 同じヘッダならソート方法の変更、違うヘッダなら昇順。
            //
            if (colmn.Equals(sortData.Name))
            {
                var val = ((int)sortData.Order + 1) % 3;
                sortData.Order = (SortingOrder)Enum.ToObject(typeof(SortingOrder), val);
            }
            else
            {
                sortData.Name = colmn;
                sortData.Order = SortingOrder.Ascendant;
            }

            masterListView.Sort();

            //
            // ソートラベルの描画状態を更新
            //
            foreach (var header in headers)
            {
                if (header.ColmnName.Equals(sortData.Name))
                {
                    header.SortingOrder = sortData.Order;
                }
                else
                {
                    header.SortingOrder = SortingOrder.None;
                }
            }
        }

        internal void TapRow(object parameter)
        {
            System.Diagnostics.Debug.WriteLine(parameter);
        }
    }

    public sealed class RecordViewColumnCollection : ObservableCollection<RecordViewColumn>
    {
    }
}