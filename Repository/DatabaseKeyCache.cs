using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogs.Repository
{
    internal class DatabaseKeyCache<T>
    {
        private Dictionary<int, T> _forward = new Dictionary<int, T>();
        private Dictionary<T, int> _reverse = new Dictionary<T, int>();

        public IEnumerable<int> Keys
        {
            get { return _forward.Keys; }
        }

        public IEnumerable<T> Items
        {
            get { return _reverse.Keys; }
        }

        internal int this[T item]
        {
            get { return _reverse[item]; }
        }

        internal T this[int id]
        {
            get { return _forward[id]; }
        }

        public void Add(int id, T item)
        {
            AddItem(id, item);
        }

        protected virtual void AddItem(int id, T item)
        {
            _forward[id] = item;
            _reverse[item] = id;
        }

        public void Remove(int id)
        {
            T item = _forward[id];
            RemoveItem(id, item);
        }

        protected virtual void RemoveItem(int id, T item)
        {
            _forward.Remove(id);
            _reverse.Remove(item);
        }

        public void Remove(T item)
        {
            int id = _reverse[item];
            RemoveItem(id, item);
        }

        public bool ContainsItem(T item)
        {
            return _reverse.ContainsKey(item);
        }

        public bool ContainsKey(int item)
        {
            return _forward.ContainsKey(item);
        }

        public bool TryGetItem(int id, out T item)
        {
            return _forward.TryGetValue(id, out item);
        }

        public bool TryGetKey(T item, out int id)
        {
            return _reverse.TryGetValue(item, out id);
        }
    }
}