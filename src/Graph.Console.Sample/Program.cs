using Graph.Abstractions;
using System;
using Prompt = System.Console;

namespace Graph.Console.Sample;

public sealed class Program
{
    static void Main(string[] args)
    {
        var graph = new Graph<char>();

        graph.AddBidiretionalEdge('1', '2');
        graph.AddBidiretionalEdge('2', '5');
        graph.AddDirectedEdge('2', '3');
        graph.AddDirectedEdge('3', '4');
        graph.AddDirectedEdge('4', '5');
        graph.AddDirectedEdge('4', '1');

        Prompt.WriteLine("\nGraph");
        graph.PrintGraph();

        Prompt.WriteLine("\nBreadth-First Search");
        graph.PrintBFS('1');

        Prompt.WriteLine("\nDepth-First Search");
        graph.PrintDFS('1');

        Prompt.WriteLine("\nShortest Way Breadth-First Search");
        graph.PrintShortestWayBFS('1', '5');

        var eccentricity = graph.GetAllVertexEccentricity();

        Prompt.WriteLine("\nEccentricities");
        foreach (var unique in eccentricity)
        {
            Prompt.WriteLine($"{unique.Key} = {unique.Value};");
        }

        Prompt.WriteLine("\nDetails");
        Prompt.WriteLine($"Ratio = {graph.GetRatio()}");
        Prompt.WriteLine($"Diameter = {graph.GetDiameter()}");

        Prompt.WriteLine();
    }
}
