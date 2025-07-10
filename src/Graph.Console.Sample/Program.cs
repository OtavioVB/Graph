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
        graph.AddVertex('F');
        graph.AddBidiretionalEdge('A', 'B');
        graph.AddBidiretionalEdge('B', 'D');
        graph.AddBidiretionalEdge('A', 'C');
        graph.AddBidiretionalEdge('C', 'E');
        graph.AddBidiretionalEdge('C', 'F');
        graph.AddBidiretionalEdge('F', 'E');

        graph.PrintGraph();

        graph.PrintShortestWayBFS('A', 'E');

        var eccentricity = graph.GetAllVertexEccentricity();

        foreach (var unique in eccentricity)
        {
            Prompt.WriteLine($"{unique.Key} = {unique.Value};");
        }

        Prompt.WriteLine($"Ratio = {graph.GetRatio()}");
        Prompt.WriteLine($"Diameter = {graph.GetDiameter()}");

        Prompt.WriteLine();
    }
}
