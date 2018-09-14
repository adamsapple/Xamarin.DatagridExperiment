using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListViewTest01
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LapGrid : ContentView
	{
        private List<ClickableLabel> headers = new List<ClickableLabel>();

        public LapGrid ()
		{
			InitializeComponent ();

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
            
            //
            // ソートラベルの描画状態を更新
            //
            var sortData = listView00.SortData;
            foreach(var header in headers)
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
        }
    }
}