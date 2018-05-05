using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework9
{
    class MyListAddedEventArgs <T> : EventArgs
    {
        public T NewValue { get; private set; }
        public MyListAddedEventArgs (T newValue)
        {
            this.NewValue = newValue;
        }
    }
    class MyCollection<T> : ICollection<T>
    {
        private List<T> list = new List<T>();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly => throw new NotImplementedException();

        public event EventHandler AddingNew;

        public event EventHandler<MyListAddedEventArgs<T>> OnAdded;
        
        private void OnAddingNew (object sender, EventArgs e)
        {
            this.AddingNew(sender, e);
        }
        private void OnListChanged(object sender, EventArgs e)
        {
            this.OnAddingNew(sender, e);
        }
        public void Add(T item)
        {
            if (OnAdded!=null)
            {
                OnAdded(this, new MyListAddedEventArgs<T>(item));
            }
        }

        public void Clear()
        {
            this.OnAdded(this, EventArgs.Empty);
            this.list.Clear();
        }

        public bool Contains(T item)
        {
            return this.list.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        public bool Remove(T item)
        {
            this.OnListChanged(this, EventArgs.Empty);
            return this.list.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var list = new MyList<int>;
            list.OnAdded += (s, a) => Console.WriteLine(a.NewValue);

        }
    }
}
