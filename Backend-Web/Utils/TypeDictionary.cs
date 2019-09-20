using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend_Web.Utils
{
    public class TypeDictionary<TValue> : Dictionary<Type, TValue>
    {
        public TValue Get<T>()
        {
            return this[typeof(T)];
        }

        public void Add<T>(TValue value)
        {
            Add(typeof(T), value);
        }

        public bool Remove<T>()
        {
            return Remove(typeof(T));
        }

        public bool TryGetValue<T>(out TValue value)
        {
            return TryGetValue(typeof(T), out value);
        }

        public bool ContainsKey<T>()
        {
            return ContainsKey(typeof(T));
        }
    }
}