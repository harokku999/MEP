using System.Collections.Generic;

namespace Epam.Mep.Tree
{
    public interface INode<T> : IEnumerable<INode<T>>
    {
        INode<T> Parent { get; set; }
        IList<INode<T>> Children { get; }
        T Value { get; set; }

        INode<T> AddChild(INode<T> child);

        IEnumerable<INode<T>> GetSiblings();

        bool IsLeaf { get; }

        bool IsRoot { get; }
    }
}
