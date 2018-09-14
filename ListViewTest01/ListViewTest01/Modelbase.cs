using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ListViewTest01
{
    public class ModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        protected PropertyChangedEventHandler _propertyChanged;
        public event PropertyChangedEventHandler PropertyChanged
        {
            add => _propertyChanged += value;
            remove => _propertyChanged -= value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        protected virtual void SetProperty<T>(ref T field, T value,
                    [CallerMemberName]string propertyName = null)
        {
            field = value;
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setter"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        protected virtual void SetProperty<T>(Action<T> setter, T value,
                    [CallerMemberName]string propertyName = null)
        {
            setter(value);
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setter"></param>
        /// <param name="getter"></param>
        /// <param name="propertyName"></param>
        protected virtual void SetProperty<T>(Action<T> setter, Func<T> getter,
                    [CallerMemberName]string propertyName = null)
        {
            setter(getter());
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="setter"></param>
        /// <param name="getter"></param>
        /// <param name="propertyName"></param>
        //protected virtual void SetProperty([CallerMemberName]string propertyName = null)
        //{
        //    _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
    }
}
