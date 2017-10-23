using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using FirstToolWin.Readers;
using System.Diagnostics;

namespace FirstToolWin.Utilities
{
    [DebuggerStepThrough]
    public class Map<T1, T2> : IEnumerable<KeyValuePair<T1, T2>>
    {
        private Dictionary<T1, T2> _forward = new Dictionary<T1, T2>();
        private Dictionary<T2, T1> _reverse = new Dictionary<T2, T1>();

        public Map()
        {
            this.Forward = new Indexer<T1, T2>(_forward);
            this.Reverse = new Indexer<T2, T1>(_reverse);
        }

        public class Indexer<T3, T4>
        {
            private Dictionary<T3, T4> _dictionary;
            public Indexer(Dictionary<T3, T4> dictionary)
            {
                _dictionary = dictionary;
            }
            public T4 this[T3 index]
            {
                get { return _dictionary[index]; }
                set { _dictionary[index] = value; }
            }
        }

        public void Add(T1 t1, T2 t2)
        {
            _forward.Add(t1, t2);
            _reverse.Add(t2, t1);
        }

        public Indexer<T1, T2> Forward
        { get; private set; }
        public Indexer<T2, T1> Reverse
        { get; private set; }
        public Dictionary<T1, T2>.ValueCollection Values1
        { get { return _forward.Values; } }
        public Dictionary<T2, T1>.ValueCollection Values2
        { get { return _reverse.Values; } }
        public T2 this[T1 index]
        {
            get
            {
                return _forward[index];
            }
            set
            {
                _forward[index] = value;
                _reverse[value] = index;
            }
        }
        public T1 this[T2 index]
        {
            get
            {
                return _reverse[index];
            }
            set
            {
                _reverse[index] = value;
                _forward[value] = index;
            }
        }

        internal bool ContainsKey(T1 t1Key)
        {
            return _forward.ContainsKey(t1Key);
        }
        internal bool ContainsKey(T2 t2Key)
        {
            return _reverse.ContainsKey(t2Key);
        }

        internal void Remove(T1 t1)
        {
            T2 t2 = _forward[t1];
            _forward.Remove(t1);
            _reverse.Remove(t2);
        }

        internal void Remove(T2 t2)
        {
            T1 t1 = _reverse[t2];
            _reverse.Remove(t2);
            _forward.Remove(t1);
        }

        public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator()
        {
            return _forward.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _forward.GetEnumerator();
        }

        internal bool TryGetValue(T1 t1, out T2 t2)
        {
            return _forward.TryGetValue(t1, out t2);
        }
        internal bool TryGetValue(T2 t2, out T1 t1)
        {
            return _reverse.TryGetValue(t2, out t1);
        }
    }

}
