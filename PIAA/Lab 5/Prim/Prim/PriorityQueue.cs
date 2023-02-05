using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Prim
{
    public class MinHeap<T>
        where T : IComparable<T>
    {
        private List<T> _buffer = new List<T>();

        public int Size => _buffer.Count;

        private int GetParent(int index)
            => index <= 0 ? -1 : (index - 1) / 2;

        private int GetLeft(int index)
            => 2 * index + 1;

        private int GetRight(int index)
            => 2 * index + 2;

        public void HeapifyUp(int index)
        {
            var parent = GetParent(index);
            if (parent >= 0 && _buffer[index].CompareTo(_buffer[parent]) < 0)
            {
                T temp = _buffer[index];
                _buffer[index] = _buffer[parent];
                _buffer[parent] = temp;

                HeapifyUp(parent);
            }
        }

        private void HeapifyDown(int index)
        {
            var smallest = index;

            var left = GetLeft(index);
            var right = GetRight(index);

            if (left < Size && _buffer[left].CompareTo(_buffer[index]) < 0)
            {
                smallest = left;
            }

            if (right < Size && _buffer[right].CompareTo(_buffer[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != index)
            {
                T temp = _buffer[index];
                _buffer[index] = _buffer[smallest];
                _buffer[smallest] = temp;

                HeapifyDown(smallest);
            }

        }

        public void Insert(T item)
        {
            _buffer.Add(item);
            HeapifyUp(_buffer.Count - 1);
        }

        public T Peek =>
            _buffer.Count > 0 ?
                _buffer[0] : default;

        public T PopMin
        {
            get
            {
                if (_buffer.Count == 0)
                    throw new ArgumentException("Nothing to pop!");

                T item = _buffer[0];
                _buffer[0] = _buffer[_buffer.Count - 1];
                _buffer.RemoveAt(_buffer.Count - 1);

                HeapifyDown(0);
                return item;
            }
        }

    }
}
