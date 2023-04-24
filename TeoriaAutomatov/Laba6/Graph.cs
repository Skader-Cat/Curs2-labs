using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba6
{
    internal class Graph
    {
        class Node
        {
            string name;
            bool isFinal;
            List<KeyValuePair<Node, string>> exitNodes = new List<KeyValuePair<Node, string>>();
            
            public Node(string name)
            {
                createNode(name);
            }

            private void createNode(String name)
            {       
                    setName(name);
                    checkIsFinal();
            }
            private void checkIsFinal()
            {
                if (this.name.EndsWith('f'))
                {
                    this.isFinal = true;
                }
                else { this.isFinal = false; }
            }

            private void setName(string name)
            {
                while (true)
                {
                    if (!isNodeNameInNodes(name))
                    {
                        this.name = name;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Узел с таким именем уже существует, введите другое имя: ");
                        name = Console.ReadLine();
                    }
                }
            }

            private static bool isNodeNameInNodes(string name)
            {
                if(nodes.Where(x => x.name == name).Count() > 0) { return true; }
                else { return false; }
            }

            public static Node getNodeOrCreateByName(string name)
            {
                if (isNodeNameInNodes(name))
                {
                    return nodes.Where(x => x.name == name).First();
                }
                else
                {
                    return new Node(name);
                }
            }


            public void addExitNode(KeyValuePair<Node, string> exitNode)
            {
                String name = exitNode.Key.name;
                if (isNodeNameInNodes(name))
                {
                    this.exitNodes.Add(exitNode);
                }
                else
                {
                    this.exitNodes.Add(new KeyValuePair<Node, String>(new Node(name), exitNode.Value));
                }

            }
        }

        static List<Node> nodes = new List<Node>();
        List<String> alphabet = new List<String>();

        public Graph(String alphabet)
        {
            setAlphabet(alphabet);
            nodes = new List<Node>();
        }

        public void addNode(String name, Dictionary<string, string> exitNodes = null)
        {
            Node node = Node.getNodeOrCreateByName(name);
            setExitNodes(node, exitNodes);
            nodes.Add(node);
        }

        private void setExitNodes(Node node, Dictionary<string, string> exitNodes)
        {
            foreach(var pair in exitNodes)
            {
                Node currentNode = Node.getNodeOrCreateByName(pair.Key);
                node.addExitNode(new KeyValuePair<Node, string>(currentNode, pair.Value));
            }
        }

        private void setAlphabet(string alphabet)
        {
            this.alphabet = alphabet.Split(' ').ToList();
        }
    }
}
