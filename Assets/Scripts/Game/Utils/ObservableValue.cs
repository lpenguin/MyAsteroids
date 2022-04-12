using System;
using UnityEngine;

namespace Game.Utils
{
    public class ObservableValue<T> where T: IEquatable<T>
    {
        public delegate void ValueChanged(T value);

        public event ValueChanged OnValueChanged;

        [SerializeField]
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
    
    [Serializable]
    public class ObservableFloat: ObservableValue<float>
    {
        
    }
    
    [Serializable]
    public class ObservableInt: ObservableValue<int>
    {
        
    }
    
    [Serializable]
    public class ObservableVector2: ObservableValue<Vector2>
    {
        
    }
}