using System;
using System.Collections;
using System.Collections.Generic;

namespace FreeIndexArrayLib.Classes
{
    public class FreeIndexArray<T> : ICollection<T> where T : IComparable
    {
        private Node<T> startNode;

        private Node<T> lastNode;

        private readonly int startIndex;

        private readonly int stopIndex;

        public int Count { get; private set; }

        public int MaxLength => stopIndex - startIndex + 1;

        public bool IsReadOnly => false;

        public T this[int index]
        {
            get
            {
                return Find(index).Value;
            }
            set
            {
                if (value == null)
                    throw new NullReferenceException();

                Find(index).Value = value;
            }
        }

        public FreeIndexArray(int startIndex, int stopIndex)
        {
            if (stopIndex < startIndex)
                throw new FreeIndexArrayIndexValuesException();

            this.startIndex = startIndex;
            this.stopIndex = stopIndex;
        }

        private Node<T> Find(int index)
        {
            if (index < startIndex || index >= startIndex + Count)
                throw new ArgumentOutOfRangeException();

            if (startNode is null)
                throw new NullReferenceException();

            int mid = (2 * startIndex + Count - 1) / 2;

            if (index <= mid)
            {
                var currentNode = startNode;

                for (int i = startIndex; i <= mid; i++)
                {
                    if (i == index)
                        return currentNode;

                    currentNode = currentNode.Next;
                }
            }
            else
            {
                var currentNode = lastNode;

                for (int i = startIndex + Count - 1; i > mid; i--)
                {
                    if (i == index)
                        return currentNode;

                    currentNode = currentNode.Prev;
                }
            }

            throw new FreeIndexArrayUnableToFindValueException();
        }       

        public void Add(T item)
        {
            if (item == null)
                throw new NullReferenceException();

            if (Count >= MaxLength)
                throw new FreeIndexArrayIsFullException();

            Node<T> newNode = new Node<T>(item);

            if (startNode is null)
                startNode = newNode;
            else
            {
                lastNode.Next = newNode;
                newNode.Prev = lastNode;
            }

            lastNode = newNode;
            Count++;
        }

        public bool Remove(T item)
        {
            if (item == null)
                throw new NullReferenceException();

            if (startNode is null)
                return false;

            if (startNode.Value.CompareTo(item) == 0)
            {
                if (startNode.Next is null)
                {
                    Clear();
                    return true;
                }
                else
                {
                    startNode.Next.Prev = null;
                    startNode = startNode.Next;

                    Count--;
                    return true;
                }
            }

            var currentNode = startNode.Next;

            for (int i = 1; i < Count; i++)
            {
                if (currentNode.Value.CompareTo(item) == 0)
                {
                    if (currentNode.Next is null)
                    {
                        currentNode.Prev.Next = null;
                        lastNode = currentNode.Prev;
                    }
                    else
                    {
                        currentNode.Prev.Next = currentNode.Next;
                        currentNode.Next.Prev = currentNode.Prev;
                    }

                    Count--;
                    return true;
                }

                currentNode = currentNode.Next;
            }

            return false;
        }

        public void Clear()
        {
            startNode = null;
            lastNode = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            if (item == null)
                throw new NullReferenceException();

            if (startNode is null)
                return false;

            foreach (var nodeValue in this)
            {
                if (nodeValue.CompareTo(item) == 0)
                    return true;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array is null)
                throw new ArgumentNullException();

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException();

            if (array.Length - arrayIndex < this.Count)
                throw new ArgumentException();

            if (startNode is null)
                return;

            var currentNode = startNode;

            for (int i = arrayIndex; i < (Count + arrayIndex); i++)
            {
                array[i] = currentNode.Value;

                currentNode = currentNode.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (startNode is null)
                yield break;

            var currentNode = startNode;

            for (int i = 0; i < Count; i++)
            {
                yield return currentNode.Value;

                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
