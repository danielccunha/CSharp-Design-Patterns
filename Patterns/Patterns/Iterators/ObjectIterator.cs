using System;

namespace Patterns.Iterators
{
    public class Node<T>
    {
        public T Value { get; set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public Node<T> Parent { get; set; }

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right) : this(value)
        {
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }
    }

    public class InOrderIterator<T>
    {
        private readonly Node<T> root;

        private bool yieldedStart;

        public Node<T> Current { get; set; }

        public InOrderIterator(Node<T> root)
        {
            this.root = root;

            Current = root;

            while (Current.Left != null)
                Current = Current.Left;
        }

        public bool MoveNext()
        {
            if (!yieldedStart)
            {
                yieldedStart = true;
                return true;
            }

            if (Current.Right != null)
            {
                Current = Current.Right;

                while (Current.Left != null)
                    Current = Current.Left;

                return true;
            }
            else
            {
                var p = Current.Parent;

                while (p != null && Current == p.Right)
                {
                    Current = p;
                    p = p.Parent;
                }

                Current = p;
                return Current != null;
            }
        }

        public void Reset()
        {

        }
    }

    public static class ObjectIterator
    {
        public static void Start()
        {
            var root = new Node<int>(1, new Node<int>(2), new Node<int>(3));
            var it = new InOrderIterator<int>(root);

            while (it.MoveNext())
                Console.WriteLine(it.Current.Value);
        }
    }
}
