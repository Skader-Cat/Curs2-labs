using ConsoleTables;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using static Laba5Console.Graph;
using System.Collections;
using System.Security.Permissions;

namespace Laba5Console
{
    [JsonConverter(typeof(GraphConverter))]
    internal partial class Graph
    {
        public List<Node> Nodes;
        public List<string> Alphabet;
        public int nodesCount;
        List<String> startNodes = new List<String>();

        public Graph()
        {
            this.Nodes = new List<Node>();
            this.Alphabet = new List<string>();
        }

        public class Node
        {
            public string Name;
            public bool IsFinal;
            public Dictionary<Node, List<String>> leave_nodes;
            public Node(string name)
            {
                this.Name = name;
                if (this.Name.ToLower().EndsWith("f"))
                {
                    this.IsFinal = true;
                }
                leave_nodes = new Dictionary<Node, List<String>>();
            }

            public static Node CreateNode()
            {
                Console.WriteLine("Введите название узла: ");
                string name = Console.ReadLine();
                Node node = new Node(name);
                return node;
            }
        }

        public void addEdge(Node first, Node second, String edgeName)
        {
            first.leave_nodes.Add(second, new List<string> { edgeName });
        }

        private List<String> createAlphabet()
        {
            Console.Write("Введите допустимый алфавит (через пробел, маленькие буквы): ");
            return (Console.ReadLine() + " E").ToLower().Split(' ').ToList();
        }

        public void CreateGraph()
        {
            Nodes = new List<Node>();
            Console.Write("Введите количество вершин графа: ");
            while (!int.TryParse(Console.ReadLine(), out this.nodesCount))
            {
                Console.WriteLine("Некорректный ввод. Введите число:");
            }

            for (int i = 0; i < this.nodesCount; i++)
            {
                Node node = Node.CreateNode();
                Nodes.Add(node);
            }

            this.Alphabet = createAlphabet();
            foreach (var current_node in Nodes)
            {
                Console.WriteLine($"Текущий узел: {current_node.Name}");
                Console.WriteLine("Добавьте выходные узлы для текущего. Если больше узлов нет, введите 'no':");
                Console.WriteLine("Текущий узел | Cледующий узел");
                while (true)
                {
                    Console.Write($">>> {current_node.Name}  -------> ");
                    String next_node_name = Console.ReadLine();

                    Node next_node = Nodes.Find(x => x.Name.Contains(next_node_name));
                    if (next_node_name == "no")
                    {
                        Console.WriteLine($"Отлично, у узла {current_node.Name} выходных узлов всего: {current_node.leave_nodes.Count}");
                        break;
                    }
                    if (next_node != null)
                        while (true)
                        {
                            Console.Write("Введите название грани: \n>> ");
                            String edge_name = Console.ReadLine().ToLower();
                            if (!Alphabet.Contains(edge_name))
                            {
                                Console.WriteLine("Данного названия нет в алфавите.");
                            }
                            else
                            {
                                try
                                {
                                    if (next_node.Name == current_node.Name)
                                    {
                                        Node node = new Node(current_node.Name);
                                        node.IsFinal = current_node.IsFinal;
                                        node.leave_nodes.Add(current_node, new List<string> { edge_name });

                                        current_node.leave_nodes.Add(node, new List<string> { edge_name });
                                    }
                                    current_node.leave_nodes.Add(next_node, new List<String> { edge_name });

                                    break;
                                }
                                catch
                                {
                                    Console.Write($"В узел {next_node.Name} уже существует путь: ");
                                    current_node.leave_nodes[next_node].ForEach(item => Console.Write($"{item} "));
                                    Console.WriteLine("\nДобавляю ещё один путь.");
                                    current_node.leave_nodes[next_node].Add(edge_name);
                                    Console.Write($"{current_node.Name} -- ");
                                    current_node.leave_nodes[next_node].ForEach(x => Console.Write($"{x} "));
                                    Console.WriteLine($"--> {next_node.Name}");
                                    break;
                                }
                               
                            }
                        }
                    else
                    {
                        Console.WriteLine("Данный узел не обнаружен!");
                    }
                }
            }
        }
        public void SaveAsJson(String filename)
        {
            var directoryPath = "Graphs";
            var jsonFilePath = Path.Combine(directoryPath, filename);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(jsonFilePath, json);
        }

        public static Graph LoadAsJson(String filename)
        {
            string filePath = Path.Combine("Graphs", filename);

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File {filename} does not exist.");
                return null;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                Graph graph = JsonConvert.DeserializeObject<Graph>(json);
                return graph;
            }
            catch (JsonReaderException e)
            {
                Console.WriteLine($"Error reading JSON file: {e.Message}");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error loading graph from file: {e.Message}");
                return null;
            }
        }
        public void PrintInfo()
        {
            var table = new ConsoleTable("Текущий узел", "Путь", "Выходной узел");
            foreach (var node in Nodes)
            {
                foreach (var leave_node in node.leave_nodes)
                {
                    String path = "";
                    leave_node.Value.ForEach(item => path += ("+" + item));
                    path = path.Remove(path.IndexOf("+"), path.IndexOf("+") + 1);
                    table.AddRow(node.Name, path, leave_node.Key.Name);
                }
            }
            table.Write();
        }

        private List<Node> findCorrectNodesE(Node current_node, String find_symb)
        {
            List<Node> correct_nodes = new List<Node>();
            var temp_node = current_node;
            int elem_pos = 0;
            List<Node> nodes_to_remove = new List<Node>();
            String text = "";
            while (correct_nodes.Count < Nodes.Count)
            {
                var nodes_with_correct_name = temp_node.leave_nodes.Where(n => (n.Value.Contains(find_symb) || n.Value.Contains("e"))).Select(n => n.Key);
                if (nodes_with_correct_name.Any())
                {
                    var nodes_with_name_e = temp_node.leave_nodes.Where(n => n.Value.Contains("e"));
                    foreach(var item in nodes_with_name_e)
                    {
                        nodes_to_remove.Add(item.Key);
                    }
                    foreach (var nodeWithNameE in nodes_with_correct_name)
                    {
                        correct_nodes.Add(nodeWithNameE);
                    }
                    temp_node = correct_nodes[elem_pos];
                    elem_pos++;
                }
                else
                {
                    if (elem_pos < correct_nodes.Count)
                    {
                        temp_node = correct_nodes[elem_pos];
                        elem_pos++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return correct_nodes;
        }


        public List<Node> findPath(Node startNode, string symbol)
        {
            Graph temp_graph = Graph.LoadAsJson("tmp.json");
            var correct_nodes = new List<Node>();
            Queue<Node> nodes_queue = new Queue<Node>();
            var newStartNode = temp_graph.Nodes.Find(n => n.Name == startNode.Name);
            nodes_queue.Enqueue(newStartNode);
            int iteration = 0;
            while (nodes_queue.Any() && iteration < 15)
            {
                iteration++;
                var temp_node = nodes_queue.Dequeue();

                var contains_symbol = temp_node.leave_nodes.Where(n => n.Value.Any(s => s.Replace("e", "") == symbol));
                var contains_e = temp_node.leave_nodes.Where(n => n.Value.Any(s => s.Contains("e")));
                if (contains_symbol.Any())
                {
                    foreach (var node in contains_symbol)
                    {
                        var originalNode = Nodes.Find(n => n.Name == node.Key.Name);
                        node.Key.leave_nodes = node.Key.leave_nodes.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(s => s + node.Value.FirstOrDefault()).ToList());
                        correct_nodes.Add(originalNode);
                        if (!(node.Key.Name == temp_node.Name))
                        {
                            nodes_queue.Enqueue(node.Key);
                        }
                    }
                }
                if (contains_e.Any())
                {
                    foreach (var node in contains_e)
                    {
                        if (node.Value.Any(x => x.Replace("e", "") == symbol))
                        {
                            var originalNode = Nodes.Find(n => n.Name == node.Key.Name);
                            node.Key.leave_nodes = node.Key.leave_nodes.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Select(s => s + node.Value).ToList());
                            correct_nodes.Add(originalNode);
                            nodes_queue.Enqueue(node.Key);
                        }
                        else
                        {
                            nodes_queue.Enqueue(node.Key);
                        }
                    }
                }
            }
            return correct_nodes;
        }

        public Dictionary<string, List<List<string>>> MakeTableS()
        {
            var table = new ConsoleTable("Si", "Доступные узлы");
            Dictionary<string, List<Node>> Si = new Dictionary<string, List<Node>>();
            Dictionary<string, List<List<Node>>> fullSi = new Dictionary<string, List<List<Node>>>();
            Dictionary<string, List<List<string>>> extendedSi = new Dictionary<string, List<List<string>>>();
            foreach (var node in Nodes)
            {

                var correct_nodes = findCorrectNodesE(node, "e");
                correct_nodes.Add(node);
                Si.Add(node.Name.Replace(node.Name.First(), 'S'), correct_nodes.Distinct().ToList());
            }

            

            foreach (var node in Si)
            {
                table.AddRow(node.Key, String.Join(",", node.Value.Select(x => x.Name)));
            }
            table.Write();

            var si_table = new ConsoleTable("Si, ");
            si_table.AddColumn(Alphabet.GetRange(0, Alphabet.Count-1));

            var s0 = Si.First().Value;
            var findVerticals = Si.Where(pair => pair.Value.Any(node => s0.Contains(node))).Select(pair => pair.Key).ToList();
            findVerticals.ForEach(x => startNodes.Add(x));



            foreach (var node in Nodes)
            {
                List<List<Node>> nodes = new List<List<Node>>();
                foreach (var letter in Alphabet.Where(x => x != "e"))
                {
                    nodes.Add(findPath(node, letter).Distinct().ToList());
                }
                fullSi.Add(node.Name.Replace(node.Name.First(), 'S'), nodes);
            }

            foreach (var node in fullSi)
            {
                List<string> node_names = new List<string>();
                node_names.Add(node.Key);
                foreach(var value in node.Value)
                {
                    var temp_arr = String.Join(",", value.Select(x => x.Name));
                    node_names.Add(temp_arr);
                }
                si_table.AddRow(node_names.ToArray());
            }
            si_table.Write();

            foreach (var node in Nodes)
            {
                List<List<string>> nodes = new List<List<string>>();
                foreach (var letter in Alphabet.Where(x => x != "e"))
                {
                    var temp_arr = findPath(node, letter).Distinct().ToList();
                    var elems = new List<string>();
                    foreach (var elem in Si) {
                        if(elem.Value.All(n => temp_arr.Contains(n)))
                        {
                            elems.Add(elem.Key);
                        }
                    }
                    nodes.Add(elems);
                }
                extendedSi.Add(node.Name.Replace(node.Name.First(), 'S'), nodes);
            }

            var extendedSi_table = new ConsoleTable("Si ");
            extendedSi_table.AddColumn(Alphabet.GetRange(0, Alphabet.Count - 1));

            foreach (var node in extendedSi)
            {
                List<string> node_names = new List<string>();
                node_names.Add(node.Key);
                foreach (var value in node.Value)
                {
                    var temp_arr = String.Join(",", value.Select(x => x));
                    node_names.Add(temp_arr);
                }
                extendedSi_table.AddRow(node_names.ToArray());
            }
            extendedSi_table.Write();
            return extendedSi;
        }

        private List<String> getCellP(List<String> PiValues, int columnCounter, Dictionary<string, List<List<string>>> Si)
        {
            List<string> flattenedList = new List<string>();
            foreach (var value in PiValues)
            {
                flattenedList.AddRange(Si[value][columnCounter]);
            }
            return flattenedList.OrderBy(x => x).Distinct().ToList();
        }

        public bool checkInputChain(Graph graph)
        {
            Stack<Char> inputChain = new Stack<Char>();
            Console.WriteLine("Введите слово для проверки: ");
            foreach(var input in Console.ReadLine().Reverse())
            {
                inputChain.Push(input);
            }

            var startNode = graph.Nodes.First();
            Queue<Node> nodesQueue= new Queue<Node>();
            nodesQueue.Enqueue(startNode);
            while (inputChain.Count > 0)
            {
                var currentLetter = inputChain.Pop();
                var currentNode = nodesQueue.Dequeue();
                if (currentNode.leave_nodes.Values.Any(x => x.Contains(new string(currentLetter, 1))))
                {
                    var next_node = currentNode.leave_nodes.Where(pair => pair.Value.Contains(new string(currentLetter, 1))).Select(pair => pair.Key).ToList();
                    next_node.ForEach(x => nodesQueue.Enqueue(x));
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        public void makeTablesP(Dictionary<string, List<List<string>>> Si)
        {
            Dictionary<String, List<String>> Pi = new Dictionary<String, List<String>>();

            Dictionary<String, List<String>> P = new Dictionary<String, List<String>>();
            P.Add("P0", startNodes);

            for (int i = 0; i < P.Count; i++)
            {
                var item = P.ElementAt(i);
                List<String> columns = new List<String>();
                for (int index = 0; index < Alphabet.Count - 1; index++)
                {
                    var cell = getCellP(item.Value, index, Si);
                    if (cell.Count == 0)
                    {
                        columns.Add("");
                    }
                    else
                    {
                        string value = P.FirstOrDefault(x => x.Value.SequenceEqual(cell)).Key;
                        if (value == null)
                        {
                            var nextP = "P" + (P.Count);
                            P.Add(nextP, cell);
                            columns.Add(nextP);
                        }
                        else
                        {
                            columns.Add(value);
                        }
                    }
                }
                Pi.Add(item.Key, columns);
            }
            
            var table = new ConsoleTable("Pi");
            table.AddColumn(Alphabet.GetRange(0, Alphabet.Count - 1));

            foreach(var item in Pi)
            {
                var listForPrint = new List<string>();
                listForPrint.Add(item.Key);
                item.Value.ForEach(x => listForPrint.Add(x));
                table.AddRow(listForPrint.ToArray());
            }
            table.Write();

            Graph graphFinal = new Graph();
            List<Node> nodesFinal = new List<Node>();
            foreach(var item in Pi)
            {
                Node node = new Node(item.Key);
                nodesFinal.Add(node);
            }

            foreach(var item in Pi)
            {
                for (var i = 0; i < item.Value.Count; i++) {
                    {
                        var first = nodesFinal.Find(x => x.Name == item.Key);
                        var second = nodesFinal.Find(x => x.Name == item.Value[i]);
                        if ((first != null) && (second != null))
                        {
                            addEdge(nodesFinal.Find(x => x.Name == item.Key), nodesFinal.Find(x => x.Name == item.Value[i]), Alphabet[i]);
                        }
                    }
                }
            }
            graphFinal.Alphabet = Alphabet;
            graphFinal.Nodes = nodesFinal;

            while (true)
            {
                Console.WriteLine(checkInputChain(graphFinal));
            }
        }
    }
}
