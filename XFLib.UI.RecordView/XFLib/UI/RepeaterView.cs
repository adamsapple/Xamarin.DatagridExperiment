using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Reflection;

namespace XFLib.UI
{
    /// <summary>
    /// Repeater view.
    /// </summary>
    public class RepeaterView : StackLayout
    {
        #region Bindable Properties.
        /// <summary>
        /// Bindable Property for UniqueId.
        /// </summary>
        public static readonly BindableProperty UniqueIdProperty =
            BindableProperty.Create(nameof(UniqueId), typeof(string), typeof(RepeaterView), defaultValue: "Id");

        /// <summary>
        /// The item template property.
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = 
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(RepeaterView), propertyChanged: (bindable, value, newValue) => Populate(bindable));

        /// <summary>
        /// The items source property.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = 
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(RepeaterView),
                //BindingMode.TwoWay,
                propertyChanged: (bindable, value, newValue) =>
                {
                    var self = bindable as RepeaterView;

                    if (value is INotifyCollectionChanged oldNcc)
                    {
                        oldNcc.CollectionChanged -= self.OnCollectionChanged;
                    }

                    if (newValue is INotifyCollectionChanged newNcc)
                    {
                        if (!self.IsSlave)
                        {
                            newNcc.CollectionChanged += self.OnCollectionChanged;
                        }
                    }

                    Populate(bindable);
                }
            );
        /// <summary>
        /// Bindable Propertiy for SelecctedRowColor.
        /// </summary>
        public static readonly BindableProperty SelecctedRowColorProperty =
            BindableProperty.Create(nameof(SelecctedRowColor), typeof(Color), typeof(RecordView), defaultValue: Color.Orange);

        /// <summary>
        /// Bindable Propertiy for EvenRowBackGroundColor.
        /// </summary>
        public static readonly BindableProperty EvenRowBackgroundColorProperty =
            BindableProperty.Create(nameof(EvenRowBackgroundColor), typeof(Color), typeof(RecordView), defaultValue: Color.FromRgba(0, 0, 0, 0.05), propertyChanged: BackgroundColorChanged);

        /// <summary>
        /// Bindable Property for SelectedItem.
        /// </summary>
        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(RepeaterView),
                propertyChanged: SelectedItemChanged);
                //validateValue: (b, v) => (b as RepeaterView).SelectionEnabled);

        /// <summary>
        /// SelectedIndexProperty
        /// </summary>
        public static BindableProperty SelectedIndexProperty =
            BindableProperty.Create(nameof(SelectedIndex), typeof(int), typeof(RepeaterView), defaultValue: -1);

        /// <summary>
        /// Bindable property for SelectionEnabled.
        /// </summary>
        public static readonly BindableProperty SelectionEnabledProperty =
            BindableProperty.Create(nameof(SelectionEnabled), typeof(bool), typeof(RepeaterView), true,
                propertyChanged: (b,o,n) => 
                    {
                        if (o == n)
                        {
                            return;
                        }
                        var self = b as RepeaterView;
                        if ((bool)n == false)
                        {
                            self.SelectedItem = null;
                        }
                        else
                        {
                            self.UpdateSelectedItem(self.SelectedItem);
                        }
                    });

        #endregion Bindable Properties.

        #region Properties.
        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public IEnumerable ItemsSource
        {
            get => (IEnumerable)this.GetValue(ItemsSourceProperty);
            set
            {
                this.SetValue(ItemsSourceProperty, value);
                Slaves.ForEach(x => x.ItemsSource = ItemsSource);
            }
        }

        /// <summary>
        /// Gets or sets the item template.
        /// </summary>
        /// <value>The item template.</value>
        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)this.GetValue(ItemTemplateProperty);
            set => this.SetValue(ItemTemplateProperty, value);
        }

        /// <summary>
        /// SelectedItem Property.
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        /// <summary>
        /// SelectedIndex
        /// </summary>
        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public Color SelecctedRowColor
        {
            get { return (Color)GetValue(SelecctedRowColorProperty); }
            set { SetValue(SelecctedRowColorProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool SelectionEnabled
        {
            get { return (bool)GetValue(SelectionEnabledProperty); }
            set { SetValue(SelectionEnabledProperty, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        public Color EvenRowBackgroundColor
        {
            get => (Color)this.GetValue(EvenRowBackgroundColorProperty);
            set => this.SetValue(EvenRowBackgroundColorProperty, value);
        }
        /// <summary>
        /// 
        /// </summary>
        public string UniqueId
        {
            get { return (string)GetValue(UniqueIdProperty); }
            set { SetValue(UniqueIdProperty, value); }
        }
        #endregion Properties.


        private View SelectedContent { get; set; }
        private bool IsSelectVisible { get; set; } = true;
        
        public IEnumerable SelfOrMasterItemsSource => ItemsSource ?? Master?.ItemsSource;

        /// <summary>
        /// 行数の更新等、再表示に使う
        /// </summary>
        public void Refresh()
        {
            var itemsSource = SelfOrMasterItemsSource?.Cast<object>().ToArray();

            // Clean
            this.Children.Clear();

            // Only populate once both properties are received
            if (itemsSource == null)
            {
                return;
            }

            foreach (var viewModel in itemsSource)
            {
                //var content = this.ItemTemplate.CreateContent();
                //BindableObject bindableObject = (BindableObject)(content = (View)ItemTemplate.CreateContent(viewModel, this));
                var content = ItemTemplate.CreateContent(viewModel, this);
                //var content = this.ItemTemplate.();
                var view    = content is View ? content as View : (content as ViewCell).View;

                if (view == null)
                {
                    throw new Exception($"Invalid visual object {nameof(content)}");
                }

                view.BackgroundColor = (Children.Count() % 2 == 0) ? Color.Transparent : EvenRowBackgroundColor;
                view.BindingContext  = viewModel;

                if (!view.GestureRecognizers.Any())
                {
                    ////
                    /// Tapイベントの登録 (選択物の更新)
                    //
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    //tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, "TapCommand");
                    // Tap時イベント。選択物の変更/解除
                    tapGestureRecognizer.Command = new Xamarin.Forms.Command(x => UpdateSelectedItemByTap(view.BindingContext, view));
                    // BindingされたItemのユニークなKey名を登録
                    tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, UniqueId);
                    view.GestureRecognizers.Add(tapGestureRecognizer);
                }

                this.Children.Add(view);
            }

            foreach (var slave in Slaves)
            {
                slave.Refresh();
            }
        }

        public void UpdateBackgroundColor()
        {
            if (Slaves.Any())
            {
                Slaves.ForEach(x => x.UpdateBackgroundColor());
            }
        }

        #region Static method for Bindable Properties.
        /// <summary>
        /// Binding:SelectedIndexの更新
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldvalue"></param>
        /// <param name="newvalue"></param>
        private static void SelectedItemChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if(oldvalue == newvalue)
            {
                return;
            }

            if (bindable is RepeaterView self)
            {
                self.UpdateSelectedItem(newvalue);
                self.UpdateSelectedIndexBySelectedItem();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldvalue"></param>
        /// <param name="newvalue"></param>
        public static void BackgroundColorChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            (bindable as RepeaterView)?.UpdateBackgroundColor();
        }
        #endregion Static method for Bindable Properties.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="view"></param>
        private void UpdateSelectedItem(object newItem, View view = null)
        {
            if(view == null)
            {
                view = Children.Where(x => x.BindingContext == newItem).FirstOrDefault();
            }

            UpdateSelectedItemVisual(view);

            RelaySelectedItemToMaster(SelectedItem);
            RelaySelectedItemToSlave(SelectedItem);
        }

        /// <summary>
        /// 選択物の更新
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="view"></param>
        private void UpdateSelectedItemByTap(object newItem, View view)
        {
            if (SelectedItem == newItem)
            {
                SelectedItem    = null;
            }
            else
            {
                SelectedItem    = newItem;
            }
        }

        private void UpdateSelectedItemVisual(View view = null)
        {
            if (!SelectionEnabled)
            {
                return;
            }
            
            if (SelectedContent != null)
            {
                SelectedContent.BackgroundColor = Color.Transparent;
            }

            var seelctedItem = SelectedItem;
            if (seelctedItem == null)
            {
                SelectedContent = null;
                return;
            }

            if (view == null)
            {
                view = Children.Where(x => x.BindingContext == seelctedItem).FirstOrDefault();
            }
                
            if (view != null)
            {
                //SelectedContent = (view as Layout<View> as StackLayout).Children[0];
                SelectedContent = (view as ContentView)?.Content;
                if (SelectedContent != null)
                {
                    SelectedContent.BackgroundColor = SelecctedRowColor;
                }
            }
        }

        /// <summary>
        /// 選択変更の伝搬用メソッド
        /// </summary>
        /// <param name="newItem"></param>
        /// <param name="view"></param>
        private void UpdateSelectedItemForce(object newItem, View view = null)
        {
            if (SelectedItem != newItem)
            {
                SelectedItem = newItem;
            }

            UpdateSelectedItemVisual(view);
        }

        /// <summary>
        /// Master側への選択物更新通知
        /// </summary>
        /// <param name="newItem"></param>
        private void RelaySelectedItemToMaster(object newItem)
        {
            Master?.UpdateSelectedItemForce(newItem);
            Master?.RelaySelectedItemToMaster(newItem);
        }

        /// <summary>
        /// Slave側への選択物更新通知
        /// </summary>
        /// <param name="newItem"></param>
        private void RelaySelectedItemToSlave(object newItem)
        {
            Slaves?.ForEach(x =>
            {
                x.UpdateSelectedItemForce(newItem);
                x.RelaySelectedItemToSlave(newItem);
            });
        }


        public void Sort(string propName, SortingOrder order)
        {
            if (this.ItemsSource == null || this.ItemTemplate == null)
            {
                return;
            }

            var items = this.ItemsSource.Cast<object>();

            ////
            /// Sorting.
            //
            if (SortData.Order != SortingOrder.None)
            {
                object comparer(object x) => ItemsUtil.GetValueByPath(x, propName);     // Comparer

                if (order == SortingOrder.Descendant)
                {
                    items = items.OrderByDescending(comparer);
                }
                else
                {
                    items = items.OrderBy(comparer);
                }
            }
            var sortedItems = items.ToList();

            foreach (var child in this.Children.Select((Value, Index) => new { Value, Index }))
            {
                child.Value.BindingContext = sortedItems[child.Index];
            }

            foreach (var slave in Slaves)
            {
                foreach (var child in slave.Children.Select((Value, Index) => new { Value, Index }))
                {
                    child.Value.BindingContext = sortedItems[child.Index];
                }
            }

            UpdateSelectedItemVisual();
            RelaySelectedItemToMaster(SelectedItem);
            RelaySelectedItemToSlave(SelectedItem);
        }

        public void Sort()
        {
            if (this.ItemsSource == null || this.ItemTemplate == null)
            {
                return;
            }

            ////
            /// Sorting.
            //
            var sortedItems = SortData.ExecOrder(this.ItemsSource.Cast<object>())
                                      .ToList();

            foreach (var child in this.Children.Select((Value, Index) => new { Value, Index }))
            {
                child.Value.BindingContext = sortedItems[child.Index];
            }

            foreach (var slave in Slaves)
            {
                foreach (var child in slave.Children.Select((Value, Index) => new { Value, Index }))
                {
                    child.Value.BindingContext = sortedItems[child.Index];
                }
            }

            UpdateSelectedItemVisual();
            RelaySelectedItemToMaster(SelectedItem);
            RelaySelectedItemToSlave(SelectedItem);
        }

        /// <summary>
        /// Sorting.
        /// </summary>
        //public void Sort()
        //{
        //    // Only populate once both properties are received
        //    if (this.ItemsSource == null ||
        //        this.ItemTemplate == null)
        //    {
        //        return;
        //    }

        //    var items = this.ItemsSource.Cast<object>().ToList();

        //    if (SortData.Order != SortingOrder.None)
        //    {
        //        try
        //        {
        //            var comparer = this.SortData.GetComparer();

        //            items.Sort(comparer);
        //        }
        //        catch (Exception)
        //        {
        //        }
        //    }

        //    foreach (var child in this.Children.Select((Value, Index) => new { Value, Index }))
        //    {
        //        child.Value.BindingContext = items[child.Index];
        //    }

        //    foreach (var slave in Slaves)
        //    {
        //        foreach (var child in slave.Children.Select((Value, Index) => new { Value, Index }))
        //        {
        //            child.Value.BindingContext = items[child.Index];
        //        }
        //    }

        //    UpdateSelectedItemVisual();
        //    RelaySelectedItemToMaster(SelectedItem);
        //    RelaySelectedItemToSlave(SelectedItem);
        //}

        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (SelectedIndex < 0 || SelectedIndex >= ItemsSource.Cast<object>().Count())
            {
                SelectedItem = null;
            }
            else
            {
                SelectedItem = ItemsSource.Cast<object>().ElementAt(SelectedIndex);
            }
        }

        /// <summary>
        /// Populate the specified bindable.
        /// </summary>
        /// <returns>The populate.</returns>
        /// <param name="bindable">Bindable.</param>
        private static void Populate(BindableObject bindable)
        {
            var self = (RepeaterView)bindable;

            //Masterからのみ指示を受けるようにしたい為、Slaveの場合は動かさない
            if (self.Master != null) {
                return;
            }

            // Clean
            self.Refresh();
        }

        /// <summary>
        /// SelectedIndexの更新
        /// </summary>
        private int UpdateSelectedIndexBySelectedItem()
        {
            var itemSource = SelfOrMasterItemsSource.Cast<object>();

            if (itemSource == null || SelectedItem == null)
            {
                SelectedIndex = -1;
            }
            else
            {
                SelectedIndex = itemSource.IndexOf(SelectedItem);
            }

            //System.Diagnostics.Debug.WriteLine($"SelectedIndex={SelectedIndex}");

            return SelectedIndex;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //var self = sender as RepeaterView;
            //if (self == null) {
            //    return;
            //}

            //Populate(this);

            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    AddChildren(e);
                    return;
                    
                case NotifyCollectionChangedAction.Move:
                    break;
                case NotifyCollectionChangedAction.Remove:
                    break;
                case NotifyCollectionChangedAction.Replace:
                    break;
                case NotifyCollectionChangedAction.Reset:
                    break;
            }

            Populate(this);
        }


        private void AddChildren(NotifyCollectionChangedEventArgs e)
        {
            var items = e.NewItems;
            var id    = e.NewStartingIndex;
            foreach (var viewModel in items)
            {
                //var content = this.ItemTemplate.CreateContent();
                //BindableObject bindableObject = (BindableObject)(content = (View)ItemTemplate.CreateContent(viewModel, this));
                var content = ItemTemplate.CreateContent(viewModel, this);
                //var content = this.ItemTemplate.();
                var view = content is View ? content as View : (content as ViewCell).View;

                if (view == null)
                {
                    throw new Exception($"Invalid visual object {nameof(content)}");
                }

                view.BackgroundColor = (Children.Count() % 2 == 0) ? Color.Transparent : EvenRowBackgroundColor;
                view.BindingContext  = viewModel;

                if (!view.GestureRecognizers.Any())
                {
                    ////
                    /// Tapイベントの登録 (選択物の更新)
                    //
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    //tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, "TapCommand");
                    // Tap時イベント。選択物の変更/解除
                    tapGestureRecognizer.Command = new Xamarin.Forms.Command(x => UpdateSelectedItemByTap(view.BindingContext, view));
                    // BindingされたItemのユニークなKey名を登録
                    tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandParameterProperty, UniqueId);
                    view.GestureRecognizers.Add(tapGestureRecognizer);
                }

                this.Children.Insert(id++, view);
            }

            foreach (var slave in Slaves)
            {
                slave.AddChildren(e);
            }
        }

        private void MobeChildren()
        {

        }

        public SortData SortData = new SortData();

        /*
        public static readonly BindableProperty SortDataProperty =
                BindableProperty.Create(
                    nameof(SortDataProperty),
                    typeof(SortData),
                    typeof(RepeaterView),
                    default(SortData));

        public SortData SortData
        {
            get => (SortData)this.GetValue(SortDataProperty);
            set => this.SetValue(SortDataProperty, value);
        }
        */

        private bool IsSlave => (Master != null);

        private RepeaterView       Master = null;
        private List<RepeaterView> Slaves = new List<RepeaterView>();

        /// <summary>
        /// 状態を同期するSlaveを設定する
        /// </summary>
        /// <param name="slave"></param>
        public void AddSlave(RepeaterView slave) {
            slave.Master         = this;
            slave.BindingContext = this.BindingContext;
            
            Slaves.Add(slave);
            slave.Refresh();
        }
    }

    [TypeConverter(typeof(SortDataTypeConverter))]
    public class SortData
    {
        #region ctor.
        public SortData()
        {
        }

        public SortData(string propName, SortingOrder order)
        {
            PropertyName = propName;
            Order        = order;
        }
        #endregion ctor.

        #region Properties.
        public SortingOrder Order { get; set; } = SortingOrder.None;

        public string PropertyName { get; set; } = string.Empty;

        //public Type Type { get; set; }
        #endregion Properties.


        public IEnumerable<object> ExecOrder(IEnumerable<object> enumerable)
        {
            object comparer(object x) => ItemsUtil.GetValueByPath(x, PropertyName);     // Comparer

            if(Order == SortingOrder.None)
            {
                return enumerable;
            }
            else if (Order == SortingOrder.Descendant)
            {
                return enumerable.OrderByDescending(comparer);
            }
            else
            {
                return enumerable.OrderBy(comparer);
            }
        }
        //private IComparer<object> baseComparer;

        //public void SetType(Type type)
        //{
        //    baseComparer = DynamicComparer<object>.CustomComparer(type, Name);
        //}

        //public IComparer<object> GetComparer()
        //{
        //    if (Name == null)
        //    {
        //        throw new Exception("Name is Null.");
        //    }
        //    if (Type == null)
        //    {
        //        throw new Exception("Type is Null.");
        //    }

        //    baseComparer = DynamicComparer<object>.CustomComparer(Type, Name);

        //    return new DynamicComparer<object>((a,b) => {
        //            var result = baseComparer.Compare(a, b);
        //            return Order == SortingOrder.Descendant ? -result : result;
        //        });
        //}
    }

    /// <summary>
    ///  Sort type.
    /// </summary>
    public enum SortingOrder
    {
        None       = 0,
        Ascendant  = 1,
        Descendant = 2,
    }

    /// <summary>
    /// TypeConverter for SortData.
    /// </summary>
    public class SortDataTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(Type sourceType)
        {
            return base.CanConvertFrom(sourceType);
        }

        public override object ConvertFromInvariantString(string value)
        {
            return (SortingOrder)Enum.Parse(typeof(SortingOrder), value, true);
        }
    }

    /// <summary>
    /// Comparer for Sorting.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    sealed class DynamicComparer<T> : IComparer<T>
    {
        private Func<T, T, int> compare;

        public DynamicComparer(Func<T, T, int> compare)
        {
            this.compare = compare;
        }

        public Func<T, T, int> ComparerImpl {
            get => compare;
            set => compare = value;
        }

        public static DynamicComparer<object> CustomComparer(Type type, string name)
        {
            // 対象のプロパティが登録されているか、確認する
            
            System.Reflection.PropertyInfo[] props = type.GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                
                if (prop.Name.CompareTo(name) != 0)
                {
                    continue;
                }
                var cmp = StringComparer.OrdinalIgnoreCase;
                
                return new DynamicComparer<object>(
                        (a, b) => {
                            a = prop.GetValue(a);
                            b = prop.GetValue(b);
                            var result = cmp.Compare(a, b);
                            return result;
                            //return (!isAscending)?-result:result;
                        }
                    );
            }
            throw new ArgumentException("間違ってるよ");
        }

        public int Compare(T x, T y)
        {
            return compare(x, y);
        }
    }

    static class ItemsUtil
    {
        private const char IndexBeginOp = '[';
        private const char IndexEndOp = ']';
        private const char PropertyOfOp = '.';

        public static object GetValueByPath(object obj, string path)
        {
            object result = obj;
            var tokens = path?.Split(IndexBeginOp, PropertyOfOp).ToList();
            foreach (var token in tokens)
            {
                if (result == null)
                    break;

                //  Property
                if (!token.Contains(IndexEndOp.ToString()))
                {
                    result = GetPropertyValue(result, token);
                }
                // Index
                else
                {
                    result = GetIndexValue(result, token.Replace(IndexEndOp.ToString(), ""));
                }
            }
            return result;
        }

        private static object GetPropertyValue(object obj, string propertyName)
        {
            try
            {
                return obj?.GetType().GetRuntimeProperty(propertyName)?.GetValue(obj);
            }
            catch
            {
                return null;
            }
        }

        private static object GetIndexValue(object obj, string index)
        {
            object result = null;
            var indexOperator = obj?.GetType().GetRuntimeProperty("Item");
            if (indexOperator != null)
            {
                var indexParameters = indexOperator.GetIndexParameters();
                // Looking up suitable index operator
                foreach (var parameter in indexParameters)
                {
                    bool isIndexOpWorked = true;
                    try
                    {
                        var indexVal = Convert.ChangeType(index, parameter.ParameterType);
                        result = indexOperator.GetValue(obj, new object[] { indexVal });
                    }
                    catch
                    {
                        isIndexOpWorked = false;
                    }

                    // If the index operator worked, skip looking up others
                    if (isIndexOpWorked)
                        break;
                }
            }
            return result;
        }
    }
}
