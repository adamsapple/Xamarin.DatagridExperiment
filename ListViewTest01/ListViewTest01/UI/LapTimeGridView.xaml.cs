using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListViewTest01.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LapTimeGridView : ContentView
	{
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
        #endregion Members.

        private ICommand TapCommand { get; }

        public LapTimeGridView()
        {
            InitializeComponent();
#if true
            listView00.SortData.Type = typeof(ListViewItem);
            listView00.AddSlave(listView01);
            //listView00.Refresh();

            // headersにヘッダカラムを追加していく
            headers.Add(col0);
            headers.Add(col1);
            headers.Add(col2);
            headers.Add(col3);
            headers.Add(col4);
            headers.Add(col5);

            HeaderElement     = Header;
            BodyElement       = Body;

            TapCommand = new Command(x =>
            {
                System.Diagnostics.Debug.WriteLine(x);
                TapRow(x);
            });

            //
            // ソートラベルの描画状態を更新
            //
            var sortData = listView00.SortData;
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
#if true
            var sortData = listView00.SortData;

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

            listView00.Sort();

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
#endif
        }

        internal void TapRow(object parameter)
        {
            System.Diagnostics.Debug.WriteLine(parameter);
        }

    }
}