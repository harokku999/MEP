using System.IO;
using System.Linq;
using System;
using Epam.Mep.Tree;
using Epam.Mep.Tree.Extensions;

namespace Epam.Map.Tree.Ui
{
    class Program
    {
        static void Main(string[] args)
        {

            INode<string> root = new Node<string>(@"c:\Projects\Mentoring\src\module02\task03\Restaurant");

            Fill(root);


            root.PreOrderTravelsal((node, depth) => 
            {
                var nodeName = node.Value.Split('\\').Last();
                var decoration = new string(' ', depth * 2);

                Console.WriteLine(decoration + nodeName);
            });


            Console.ReadKey();
        }

        static void Fill(INode<string> parent)
        {
            if (!File.Exists(parent.Value))
            {
                foreach (var directory in Directory.GetDirectories(parent.Value))
                {
                    Fill(parent.AddChild(new Node<string>(directory)));
                }

                foreach(var file in Directory.GetFiles(parent.Value))
                {
                    Fill(parent.AddChild(new Node<string>(file)));
                }
            }
        }
    }
}
