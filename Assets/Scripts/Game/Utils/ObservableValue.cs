using System;
using UnityEngine;

namespace Game.Utils
{
    public class ObservableValue<T> where T: IEquatable<T>
    {
        public delegate void ValueChanged(T value);

        public event ValueChanged OnValueChanged;

        private T _value;

        public T Value
        {
            get => _value;
            set
            {
                bool changed = !_value.Equals(value);
                _value = value;
                if (changed)
                {
                    OnValueChanged?.Invoke(value);    
                }
            }
        }

        public ObservableValue():this(default)
        {
        }
        
        public ObservableValue(T initial)
        {
            _value = initial;
        }
    }
    
    [Serializable]
    public class ObservableFloat: ObservableValue<float>
    {
        public ObservableFloat(float value)
            : base(value){}

    }
    
    [Serializable]
    public class ObservableInt: ObservableValue<int>
    {
        public ObservableInt(int value)
            : base(value){}
    }
    
    [Serializable]
    public class ObservableVector2: ObservableValue<Vector2>
    {
        public ObservableVector2(Vector2 value)
            : base(value){}
    }
}