using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rholiver.Mvvm.Infrastructure
{
    public class WeakCollection<T> : ICollection<T>
    {
        private readonly ICollection<WeakReference> _collection;

        public WeakCollection() {
            _collection = new List<WeakReference>();
        }

        public IEnumerator<T> GetEnumerator() {
            return AliveObjects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Add(T item) {
            _collection.Add(new WeakReference(item));
        }

        public void Clear() {
            _collection.Clear();
        }

        public bool Contains(T item) {
            return _collection.Any(w => w.Target == (object) item);
        }

        public void CopyTo(T[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public bool Remove(T item) {
            var items = _collection.Where(w => w.Target == (object) item).ToArray();
            return items.Any(w => _collection.Remove(w));
        }

        public int Count {
            get { return AliveObjects.Count(); }
        }

        public bool IsReadOnly {
            get { return false; }
        }

        private IEnumerable<T> AliveObjects {
            get { return _collection.Where(w => w.IsAlive).Select(w => (T) w.Target); }
        }
    }
}