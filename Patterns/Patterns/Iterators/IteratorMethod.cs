using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Patterns.Iterators
{
    public class BinaryTree<T> : IEnumerable<Node<T>>
    {
        private readonly Node<T> root;

        public BinaryTree(Node<T> root)
        {
            this.root = root ?? throw new ArgumentNullException(nameof(root));
        }

        public IEnumerable<Node<T>> InOrder
        {
            get
            {
                IEnumerable<Node<T>> Traverse(Node<T> current)
                {
                    if (current.Left != null)
                        foreach (var left in Traverse(current.Left))
                            yield return left;

                    yield return current;

                    if (current.Right != null)
                        foreach (var right in Traverse(current.Right))
                            yield return right;
                }

                foreach (var node in Traverse(root))
                    yield return node;
            }
        }

        public IEnumerator<Node<T>> GetEnumerator()
        {
            return InOrder.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InOrder.GetEnumerator();
        }
    }

    public static class IteratorMethod
    {
        public static void Start()
        {
            var root = new Node<int>(1, new Node<int>(2), new Node<int>(3));
            var tree = new BinaryTree<int>(root);

            Console.WriteLine(string.Join(", ", tree.Select(n => n.Value)));
        }
    }
}
