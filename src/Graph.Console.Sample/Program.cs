using Graph.Abstractions;
using System;
using Prompt = System.Console;

namespace Graph.Console.Sample;

public sealed class Program
{
    static void Main(string[] args)
    {
        var graph = new Graph<int>();

        graph.AddVertex(1);
        graph.AddVertex(2);
        graph.AddVertex(3);
        graph.AddBidiretionalEdge(1, 3);
        graph.AddDirectedEdge(1, 2);

        graph.PrintGraph();

        /*
         * 1 -> 3 2
         * 2 ->
         * 3 -> 1
         */

        graph.PrintBFS(1);

        Prompt.WriteLine();
    }
}
