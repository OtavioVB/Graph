using Graph.Abstractions.Interfaces;

namespace Graph.Abstractions;

public class Graph<T> : IGraph<T>
    where T : notnull
{
    private readonly IDictionary<T, IList<T>> _adjacencyList;

    public Graph(IDictionary<T, IList<T>>? adjacencyList = null)
    {
        _adjacencyList = adjacencyList ?? new Dictionary<T, IList<T>>();
    }

    public T[] BreadthFirstSearch(T start)
    {
        var queue = new Queue<T>();
        var visited = new HashSet<T>();

        if (!_adjacencyList.ContainsKey(start))
            return [];

        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var neighbor in _adjacencyList[current])
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        return visited.ToArray();
    }

    public T[] GetShortestWayBFS(T start, T end)
    {
        var queue = new Queue<T>();
        var visited = new HashSet<T>();
        var parent = new Dictionary<T, T>();

        if (!_adjacencyList.ContainsKey(start))
            return [];

        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Equals(end))
                break;

            foreach (var neighbor in _adjacencyList[current])
            {
                if (!visited.Contains(neighbor))
                {
                    parent[neighbor] = current;
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        var way = new List<T>();
        var node = end;

        while (!node.Equals(start))
        {
            way.Add(node);
            node = parent[node];
        }

        way.Add(start);
        way.Reverse();

        return way.ToArray();
    }

    public void PrintShortestWayBFS(T start, T end)
    {
        var result = GetShortestWayBFS(start, end);

        foreach (var item in result)
        {
            Console.WriteLine($"Ordem de Navegação = {item};");
        }
    }

    public void PrintBFS(T start)
    {
        var result = BreadthFirstSearch(start);

        foreach (var item in result)
        {
            Console.WriteLine($"Ordem de Navegação = {item};");
        }
    }

    public void AddVertex(T vertex)
    {
        if (!_adjacencyList.ContainsKey(vertex))
            _adjacencyList.Add(vertex, []);
    }

    public void AddDirectedEdge(T origin, T destination)
    {
        AddVertex(origin);
        AddVertex(destination);

        if (_adjacencyList[origin].Any(p => p.Equals(destination)))
            return;

        _adjacencyList[origin].Add(destination);
    }

    public void AddBidiretionalEdge(T primary, T secondary)
    {
        AddDirectedEdge(primary, secondary);
        AddDirectedEdge(secondary, primary);
    }

    public void PrintGraph()
    {
        foreach (var vertex in _adjacencyList.Keys)
        {
            Console.Write($"{vertex} -> ");
            foreach (var neighbor in _adjacencyList[vertex])
            {
                Console.Write($"{neighbor} ");
            }
            Console.WriteLine();
        }
    }
}
