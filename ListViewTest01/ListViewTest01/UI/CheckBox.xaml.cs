using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ListViewTest01.UI
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CheckBox : ContentView
	{
		public CheckBox()
		{
			InitializeComponent ();

            _fontSize = TxtLabel.FontSize;

            TxtLabel.FontSize      = _fontSize;
            ChkLabel.FontSize      = _fontSize;
            ChkLabel.WidthRequest  = _fontSize;
            ChkLabel.HeightRequest = _fontSize;
            
            var TapGesture = new TapGestureRecognizer();
            TapGesture.Tapped += TapGestureOnTapped;
            GestureRecognizers.Add(TapGesture);
        }

        public static BindableProperty CheckedProperty  = BindableProperty.Create(
                                                                    propertyName      : nameof(Checked),
                                                                    returnType        : typeof(bool),
                                                                    declaringType     : typeof(CheckBox),
                                                                    defaultValue      : false,
                                                                    defaultBindingMode: BindingMode.TwoWay,
                                                                    propertyChanged   : CheckedValueChanged);

        public static BindableProperty TextProperty     = BindableProperty.Create(
                                                                    propertyName      : nameof(Text),
                                                                    returnType        : typeof(String),
                                                                    declaringType     : typeof(CheckBox),
                                                                    defaultValue      : null,
                                                                    defaultBindingMode: BindingMode.TwoWay,
                                                                    propertyChanged   : TextValueChanged);

        public static BindableProperty FontSizeProperty = BindableProperty.Create(
                                                                    propertyName      : nameof(FontSize),
                                                                    returnType        : typeof(Double),
                                                                    declaringType     : typeof(CheckBox),
                                                                    defaultValue      : 0.0,
                                                                    defaultBindingMode: BindingMode.TwoWay,
                                                                    propertyChanged   : FontSizeValueChanged);

        private double _fontSize;
        public double FontSize
        {
            get => _fontSize;
            set
            {
                if(_fontSize == value)
                {
                    return;
                }
                _fontSize = value;
                SetValue(FontSizeProperty, value);
                OnPropertyChanged();
            }
        }

        private static void FontSizeValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var target = bindable as CheckBox;
            if (target == null)
            {
                return;
            }

            var size = (Double)newValue;

            target.TxtLabel.FontSize        = size;
            target.ChkLabel.FontSize        = size-2;
            target.ChkLabel.WidthRequest    = size;
            target.ChkLabel.HeightRequest   = size;
        }


        public bool Checked
        {
            get
            {
                return (bool)GetValue(CheckedProperty);
            }
            set
            {
                SetValue(CheckedProperty, value);
                OnPropertyChanged();
                RaiseCheckedChanged();
            }
        }

        private static void CheckedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if((bool)newValue)
            {
                //((CheckBox)bindable).ChkLabel.Text = "fa-check";
                ((CheckBox)bindable).ChkFrame.BackgroundColor = Color.Pink;
            }
            else
            {
                //((CheckBox)bindable).ChkLabel.Text = "";
                ((CheckBox)bindable).ChkFrame.BackgroundColor = Color.White;
            }
        }

        public event EventHandler CheckedChanged;
        private void RaiseCheckedChanged()
        {
            CheckedChanged?.Invoke(this, EventArgs.Empty);
        }

        private Boolean _IsEnabled = true;
        public new Boolean IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                _IsEnabled = value;
                OnPropertyChanged();
                this.Opacity = value ? 1 : .5;
                base.IsEnabled = value;
            }
        }

        public event EventHandler Clicked;
        private void TapGestureOnTapped(object sender, EventArgs eventArgs)
        {
            if (IsEnabled)
            {
                Checked = !Checked;
                Clicked?.Invoke(this, EventArgs.Empty);
            }
        }

        private static void TextValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            ((CheckBox)bindable).TxtLabel.Text = (String)newValue;
        }

        public event EventHandler TextChanged;
        private void RaiseTextChanged()
        {
            TextChanged?.Invoke(this, EventArgs.Empty);
        }

        public String Text
        {
            get { return (String)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                OnPropertyChanged();
                RaiseTextChanged();
            }
        }
    }
}