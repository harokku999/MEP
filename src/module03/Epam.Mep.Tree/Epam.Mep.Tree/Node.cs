using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Epam.Mep.Tree
{
    public class Animal { }public class Dog : Animal { }


    public class Node<T> : INode<T>
    {
        public INode<T> Parent { get; set; }

        public IList<INode<T>> Children { get; }

        public T Value { get; set; }

        public INode<T> this[int index]
        {
            get { return Children[index]; }
            set { Children.Insert(index, value); }
        }

        public Node(T value)
        {
            Value = value;
            Children = new List<INode<T>>();
        }

        public Node(T value, INode<T> parent) 
            : this(value)
        {
            Parent = parent;
        }

        public IEnumerator<INode<T>> GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public INode<T> AddChild(INode<T> child)
        {
            Children.Add(child);
            child.Parent = this;

            return child;
        }

        public IEnumerable<INode<T>> GetSiblings()
        {
            if (Parent != null)
            {
                return Parent.Children.Where(node => node != this);
            }

            return Enumerable.Empty<INode<T>>(); // never return with null
        }

        public bool IsLeaf => !Children.Any();

        public bool IsRoot => 
    }
}
