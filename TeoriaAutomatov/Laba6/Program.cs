namespace Laba6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graph graph = new Graph("a b");
            graph.addNode("q0", new Dictionary<string, string>{{"q0", "1" }});
            graph.addNode("q1", new Dictionary<string, string> { { "q2", "0" } });
            graph.addNode("q2", new Dictionary<string, string> { { "q0", "0" }, {"q3", "1"} });
        }
    }
}