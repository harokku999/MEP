using System;

namespace Epam.Mep.Tree.Extensions
{
    public static class NodeExtensions
    {
        public static void PreOrderTravelsal<T>(this INode<T> root, Action<INode<T>, int> visitor, int depth = 0)
        {
            visitor(root, depth);

            foreach (INode<T> child in root)
            {
                PreOrderTravelsal(child, visitor, depth++);
            }
        }
    }
}
