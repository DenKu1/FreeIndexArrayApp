using System;

namespace FreeIndexArrayLib.Classes
{
    public class Node<T>
    {
        public T Value { get; set; }

        public Node<T> Prev { get; set; }

        public Node<T> Next { get; set; }

        public Node(T value)
        {
            if (value == null)
                throw new NullReferenceException();

            Value = value;
        }
    }
}
