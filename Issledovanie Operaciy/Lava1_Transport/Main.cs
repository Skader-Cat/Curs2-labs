using Laba1_Transport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lava1_Transport
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            
            TransportTable table = new TransportTable(new List<int> { 300, 250, 200 }, new List<int> { 160, 120, 140, 200, 170},
            new string[]
                {"1 4 2 1 3",
                 "6 2 3 5 1",
                 "2 3 4 1 4"}
            );
            


           
            
            //var currentCell = graph.nodes[2];
            //var result = TableMinimizer.findCycles(graph, currentCell);


            table.printTable(table.tableCosts, table.consumers, table.suppliers);
            TableMinimizer.initionalResursDistribution(table);
            var coords = TableMinimizer.findCoordsOfMaxCostInTable(table.tableCosts, table.consumers);
            var sum = 0;
            table.printTable(table.tableCosts, table.consumers, table.suppliers);
            TableMinimizer.setCoefs(table);
            TableMinimizer.fillDeltaCost(table);
            table.printTable(table.tableCosts, table.consumers, table.suppliers);

            bool flag = true;
            Graph graph = new Graph(table);
            sum = graph.nodes.Select(x => x.node.resurs * x.node.defaultCost).Sum();
            Console.WriteLine("Сумма: " + sum);

            while (flag)
            {
                var startNode = TableMinimizer.findCorrectNode(table, graph.nodes);
                if (startNode == null)
                {
                    break;
                }
                if(TableMinimizer.findCycles(table, startNode, graph) == false)
                {
                    break;
                }
                TableMinimizer.fillDeltaCost(table);
                table.printTable(table.tableCosts, table.consumers, table.suppliers);
                sum = graph.nodes.Select(x => x.node.resurs * x.node.defaultCost).Sum();
                Console.WriteLine("Сумма: " + sum);
                TableMinimizer.setCoefs(table);
                TableMinimizer.fillDeltaCost(table);
            }
            table.printTable(table.tableCosts, table.consumers, table.suppliers, "multiply");
            sum = graph.nodes.Select(x => x.node.resurs * x.node.defaultCost).Sum();
            Console.WriteLine("Финальная сумма: " + sum);
        }
    }
}
