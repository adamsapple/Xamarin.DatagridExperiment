using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ListViewTest01.UI
{
    /// <summary>
    /// Repeater view.
    /// </summary>
    public class RepeaterView : StackLayout
    {
        /// <summary>
        /// The item template property.
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(RepeaterView), null,
                                                                                                propertyChanged: (bindable, value, newValue) => Populate(bindable));

        /// <summary>
        /// The items source property.
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(RepeaterView), null,
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
                                                                                                        newNcc.CollectionChanged += self.OnCollectionChanged;
                                                                                                    }

                                                                                                    Populate(bindable);
                                                                                                }
                                                                                            );

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
        /// 
        /// </summary>
        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                typeof(object),
                typeof(RepeaterView),
                null,
                propertyChanged:  UpdateSelected);

        /// <summary>
        /// 
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private View SelectedContent { get; set; }

        /// <summary>
        /// SelectedIndexProperty
        /// </summary>
        public static BindableProperty SelectedIndexProperty =
            BindableProperty.Create(
                nameof(SelectedIndex),
                typeof(int),
                typeof(RepeaterView),
                -1);

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
        public string UniqueId { get; set; } = "Id";

        public IEnumerable SelfOrMasterItemsSource => ItemsSource ?? Master?.ItemsSource;

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
                var content = this.ItemTemplate.CreateContent();
                if (!(content is View) && !(content is ViewCell))
                {
                    throw new Exception(
                          $"Invalid visual object {nameof(content)}");
                }

                var view = content is View ? content as View :
                                             ((ViewCell)content).View;
                view.Margin = new Thickness(0);
                view.BindingContext = viewModel;
                if (!view.GestureRecognizers.Any())
                {
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    //tapGestureRecognizer.SetBinding(TapGestureRecognizer.CommandProperty, "TapCommand");
                    tapGestureRecognizer.Command = new Command(x =>
                    {
                        RelaySelectedItem(viewModel, view, true);
                    });
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

        private void RelaySelectedItem(object newItem, View view = null, bool isStart = false)
        {
            if(SelectedItem == newItem && !isStart)
            {
                return;
            }

            if (SelectedContent != null)
            {
                SelectedContent.BackgroundColor = Color.Transparent;
            }

            if (SelectedItem == newItem)
            {
                SelectedItem    = null;
                SelectedContent = null;

                return;
            }

            SelectedItem    = newItem;
            SelectedContent = view;
            SelectedContent?.BackgroundColor = Color.Red;

            Master?.RelaySelectedItem(newItem);
            Slaves.ForEach(x => x.RelaySelectedItem(newItem));
        }

        public void Sort()
        {
            // Only populate once both properties are received
            if (this.ItemsSource == null ||
                this.ItemTemplate == null)
            {
                return;
            }

            var items = this.ItemsSource.Cast<object>().ToList();

            if (SortData.Order != SortingOrder.None)
            {
                try
                {
                    var comparer = this.SortData.GetComparer();

                    items.Sort(comparer);
                }
                catch (Exception)
                {
                }
            }

            foreach (var child in this.Children.Select((Value, Index) => new { Value, Index }))
            {
                child.Value.BindingContext = items[child.Index];
            }

            foreach (var slave in Slaves)
            {
                foreach (var child in slave.Children.Select((Value, Index) => new { Value, Index }))
                {
                    child.Value.BindingContext = items[child.Index];
                }
            }
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
        }


        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (SelectedIndex < 0 || SelectedIndex >= ItemsSource.Cast<object>().Count())
            {
                SelectedItem = -1;
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

        private static void UpdateSelected(BindableObject bindable, object oldvalue, object newvalue)
        {
            var self = bindable as RepeaterView;
            if (self == null)
            {
                return;
            }

            var itemSource = self.SelfOrMasterItemsSource.Cast<object>();

            if (itemSource == null || self.SelectedItem == null)
            {
                self.SelectedIndex = -1;
            }
            else
            {

                var item = self.SelectedItem;

                /// 
                if (itemSource.Contains(item))
                {
                    self.SelectedIndex = itemSource.IndexOf(item);
                }
                else
                {
                    self.SelectedIndex = -1;
                }
            }
            System.Diagnostics.Debug.WriteLine($"SelectedIndex={self.SelectedIndex}");
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            //var self = sender as RepeaterView;
            //if (self == null) {
            //    return;
            //}
            Populate(this);
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

        private RepeaterView Master = null;
        private List<RepeaterView> Slaves = new List<RepeaterView>();

        public void AddSlave(RepeaterView slave) {
            slave.IsSlave = true;
            slave.BindingContext = this.BindingContext;
            slave.Master = this;
            Slaves.Add(slave);
            slave.Refresh();
        }

        private bool IsSlave = false;
    }

    [TypeConverter(typeof(SortDataTypeConverter))]
    public class SortData
    {
        #region ctor
        public SortData()
        {
            Order = SortingOrder.None;
        }

        public SortData(string name, SortingOrder order)
        {
            Name = name;
            Order = order;
        }

        #endregion

        #region Properties
        public SortingOrder Order { get; set; }

        public string Name { get; set; }

        public Type Type { get; set; }
        #endregion

        private IComparer<object> baseComparer;

        //public void SetType(Type type)
        //{
        //    baseComparer = DynamicComparer<object>.CustomComparer(type, Name);
        //}

        public IComparer<object> GetComparer()
        {
            if (Name == null)
            {
                throw new Exception("Name is Null.");
            }
            if (Type == null)
            {
                throw new Exception("Type is Null.");
            }

            baseComparer = DynamicComparer<object>.CustomComparer(Type, Name);

            return new DynamicComparer<object>((a,b) => {
                    var result = baseComparer.Compare(a, b);
                    return Order == SortingOrder.Descendant ? -result : result;
                });
        }

    }

    public enum SortingOrder
    {
        None = 0,
        Ascendant = 1,
        Descendant = 2,
    }

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
}
