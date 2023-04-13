using Laba1_Transport;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Globalization;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using static Laba1_Transport.TransportTable;

namespace Lava1_Transport
{
    internal class Graph
    {
        public class Node
        {
            public (int, int) coords;
            public Cell node;
            public int cost;
            public bool visited;
            public List<Node> leave_nodes;
            public Node(Cell node, (int, int) coords,  int cost = 0)
            {
                this.node = node;
                this.coords = coords;
                this.cost = cost;
                this.visited = false;
                this.leave_nodes = new List<Node>();
            }
        }

        public List<Node> nodes = new List<Node>();

        public Graph(TransportTable transportTable)
        {
            createNodes(transportTable);
        }

        private void nodesClear()
        {
            nodes.Clear();
        }

        private void createListNodesForNode(Node currentNode)
        {
            List<Node> nodesInCurrentRow = nodes.Where(x => 
                                                            x.coords.Item1 == currentNode.coords.Item1 && 
                                                            //x.node.resurs > 0 && 
                                                            x != currentNode).ToList();

            List<Node> nodesInCurrentColumn = nodes.Where(x =>
                                                               x.coords.Item2 == currentNode.coords.Item2 &&
                                                               //x.node.resurs > 0 &&
                                                               x != currentNode).ToList();
            currentNode.leave_nodes.AddRange(nodesInCurrentRow);
            currentNode.leave_nodes.AddRange(nodesInCurrentColumn);
        }

        public void createNodes(TransportTable transportTable)
        {
            var table = transportTable.tableCosts;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    Cell cell = table.Rows[i][j] as Cell;
                    Node node = new Node(cell, (i, j));
                    node.cost = cell.defaultCost * cell.resurs;
                    nodes.Add(node);
                }
            }

            foreach(var node in nodes)
            {
                createListNodesForNode(node);
            }
        }

        public List<List<Node>> findPathByDFS(Node startNode)
        {
            startNode.node.resurs = 100;
            Queue<Node> nodesQueue = new Queue<Node>();
            List<List<Node>> cycles = new List<List<Node>>();
            List<Node> currentCycle = new List<Node>();
            nodesQueue.Enqueue(startNode);
            int columnOrRow = 0;
            int costSum = 0;
            bool flag = false;
            while (nodesQueue.Count > 0)
            {
                Node node = nodesQueue.Dequeue();
                if (!node.visited)
                {
                    flag = true;
                    if (columnOrRow % 2 == 0)
                    {
                        var rowNodes = node.leave_nodes.Where(x => x.coords.Item1 == node.coords.Item1 && (x.node.resurs > 0 || x == startNode) && (!x.visited || x == startNode)).ToList();
                        if (rowNodes.Contains(startNode))
                        {
                            currentCycle.Add(node);
                            cycles.Add(new List<Node>(currentCycle));
                            currentCycle.Clear();
                            break;
                        }
                        rowNodes.ForEach(x => nodesQueue.Enqueue(x));
                        if(rowNodes.Count > 0)
                        {
                            currentCycle.Add(node);
                        }
                        else
                        {
                            columnOrRow--;
                        }
                    }
                    else
                    {
                        var columnNodes = node.leave_nodes.Where(x => x.coords.Item2 == node.coords.Item2 && (x.node.resurs > 0 || x == startNode) && (!x.visited || x == startNode)).ToList();
                        if (columnNodes.Contains(startNode))
                        {
                            currentCycle.Add(node);
                            cycles.Add(new List<Node>(currentCycle));
                            currentCycle.Clear();
                            break;
                        }
                        columnNodes.ForEach(x => nodesQueue.Enqueue(x));
                        if(columnNodes.Count > 0)
                        {
                            currentCycle.Add(node);
                        }
                        else
                        {
                            columnOrRow--;
                        }
                    }
                    nodesQueue = new Queue<Node>(nodesQueue.OrderBy(x => x.cost).Reverse());
                    columnOrRow++;
                    node.visited = true;
                }
            }
            startNode.node.resurs = 0;
            return cycles;
        }
    }
}