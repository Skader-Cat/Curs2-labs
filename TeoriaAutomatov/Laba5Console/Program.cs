using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ConsoleTables;
using Newtonsoft.Json;
using static Laba5Console.Graph;

namespace Laba5Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Graph graph = new Graph();          
            graph.CreateGraph();
            graph.PrintInfo();
            graph.SaveAsJson("tmp.json");
            graph.SaveAsJson("mygraph.json");
            graph.MakeTableS();
            */

            
            Graph graph = Graph.LoadAsJson("mygraph.json");
            graph.PrintInfo();
            graph.MakeTableS();
            graph.makeTablesP(graph.MakeTableS());
            
        }
    }
}
