using System;

namespace Game.Utils
{
    public class ObservableValue<T> where T: IEquatable<T>
    {
        public delegate void ValueChanged(T value);

        public event ValueChanged OnValueChanged;

        private T _value;
        
        public ObservableValue():this(default)
        {
             
        }
        
        public ObservableValue(T initial)
        {
            _value = initial;
        }

        public T Get()
        {
            return _value;
        }
        
        public void Set(T value)
        {
            bool changed = !_value.Equals(value);
            if (changed)
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }
    }
}