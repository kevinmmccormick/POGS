using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pogs.Repository
{
    internal abstract class TrackingDatabaseKeyCache<T> : DatabaseKeyCache<T>
    {
        HashSet<T> _changedItems = new HashSet<T>();

        internal bool HasChanged(T item)
        {
            if (!this.ContainsItem(item))
                throw new ArgumentException("This {0} does not contain the item specified.", this.GetType().Name);

            return HasChangedCore(item);
        }

        protected virtual bool HasChangedCore(T item)
        {
            return _changedItems.Contains(item);
        }

        protected override void AddItem(int id, T item)
        {
            BeginTracking(item);
            base.AddItem(id, item);
        }

        protected override void RemoveItem(int id, T item)
        {
            EndTracking(item);
            base.RemoveItem(id, item);
        }

        protected abstract void EndTracking(T item);

        protected abstract void BeginTracking(T item);

        public void SetCheckPoint()
        {
            _changedItems.Clear();
        }

        protected void SetItemDirty(T item)
        {
            if (!_changedItems.Contains(item))
                _changedItems.Add(item);
        }

        internal protected void SetItemClean(T item)
        {
            _changedItems.Remove(item);
        }
    }
}