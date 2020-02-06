using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NASA
{    
    public class MyCollection<T> : IEnumerable<T>
    {
        private List<T> storage;
        public MyCollection()
        {
            storage = new List<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            T[] tempStorage = storage.ToArray();
            for (int i = 0; i < tempStorage.Length; i++)
            {
                yield return tempStorage[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
    