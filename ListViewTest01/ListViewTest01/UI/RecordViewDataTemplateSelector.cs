using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ListViewTest01.UI
{

    internal class RecordViewDataTemplateSelector : DataTemplateSelector
    {
        private readonly DataTemplate dataTemplate;

        public RecordView RecordView { get; set; }

        public RecordViewDataTemplateSelector()
        {
            dataTemplate = new DataTemplate(typeof(RecordViewFixedColumnCell));
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            // item       -> viewmodel
            // container  -> parentview

            var listView   = container as RepeaterView;
            var recordView = listView.Parent as RecordView;
            var items      = listView.ItemsSource.Cast<object>().ToList();

            //dataTemplate.SetValue(RecordViewCellBase.DataGridProperty, dataGrid);
            dataTemplate.SetValue(RecordViewFixedColumnCell.RecordViewProperty, RecordView);
            dataTemplate.SetValue(RecordViewFixedColumnCell.ColumnsProperty, RecordView.Columns);
            dataTemplate.SetValue(RecordViewFixedColumnCell.RowContextProperty, item);
            dataTemplate.SetValue(RecordViewFixedColumnCell.BindingContextProperty, item);

            //if (items != null)
            //    _dataGridRowTemplate.SetValue(RecordViewCellBase.IndexProperty, items.IndexOf(item));

            return dataTemplate;
        }
    }
}
