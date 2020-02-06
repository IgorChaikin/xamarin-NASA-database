using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.ObjectModel;
using System.Text;

namespace App7
{
    public enum Type
    {
        Added,
        Removed,
        Find,
        NoFind
    };
    public class MyCollection<T> :Collection<T>, IEnumerable<T>
    {
        public event EventHandler<Event> Changed;
        public T[] data;
        protected int count = 0;
        protected int gen;
        public MyCollection()
        {
            gen = 0;
            count = 0;
        }
        public MyCollection(T[] collection)
        {
            data = collection;
            gen = 0;
            count = collection.Length;
        }

        public new void Add(T item)
        {
            if (data == null)
            {
                data = new T[1];
                data[0] = item;
            }
            if (count < data.Length)
            {
                data[count] = item;
            }
            else
            {
                T[] newData = new T[data.Length * 2];
                data.CopyTo(newData, 0);
                newData[count] = item;
                data = newData;
            }
            count++;
            gen++;
            EventHandler<Event> temp = Changed;
            if (temp != null)
            {
                temp(this, new Event(Type.Added));
            }
        }

        public virtual void Del(int index)
        {
            if (index >= count || index < 0)
                throw new IndexOutOfRangeException("Incorrect input of index!");
            T temp2 = data[count - 1], temp1;
            for (int i = count - 1; i > index; i--)
            {
                temp1 = temp2;
                temp2 = data[i - 1];
                data[i - 1] = temp1;
            }
            data[count - 1] = default(T);
            count--;
            gen++;
            EventHandler<Event> temp = Changed;
            if (temp != null)
            {
                temp(this, new Event(Type.Removed));
            }
        }
        public virtual int IndexOf(string number)
        {
            EventHandler<Event> temp = Changed;
            if (temp != null)
            {
                if (number=="true")
                    temp(this, new Event(Type.Find));
                else
                    temp(this, new Event(Type.NoFind));
            }
            return -1;
        }
        public IEnumerator<T> GetEnumerator() => new MyEnumerator(this);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        private class MyEnumerator : IEnumerator<T>
        {
            private readonly MyCollection<T> _this;
            private int position = -1;
            private readonly int gen;
            public MyEnumerator(MyCollection<T> col)
            {
                _this = col;
                gen = col.gen;
            }
            public bool MoveNext()
            {
                if (gen != _this.gen)
                    throw new InvalidOperationException("Error!");
                if (position == _this.count - 1)
                    return false;
                position++;
                return true;
            }
            public void Reset() => position = -1;
            public void Dispose() { }
            public T Current { get { return _this.data[position]; } }
            Object IEnumerator.Current => Current;
        }
    }
}
