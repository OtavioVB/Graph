using Graph.Abstractions;
using System;
using Prompt = System.Console;

namespace Graph.Console.Sample;

public sealed class Program
{
    static void Main(string[] args)
    {
        var graph = new Graph<char>();

        graph.AddVertex('A');
        graph.AddVertex('B');
        graph.AddVertex('C');
        graph.AddVertex('D');
        graph.AddVertex('E');
        graph.AddDirectedEdge('A', 'B');
        graph.AddDirectedEdge('B', 'D');
        graph.AddDirectedEdge('A', 'C');
        graph.AddDirectedEdge('C', 'E');

        graph.PrintGraph();

        graph.PrintShortestWayBFS('A', 'E');

        Prompt.WriteLine();
    }
}
