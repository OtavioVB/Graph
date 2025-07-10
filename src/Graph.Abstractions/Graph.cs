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

    public int GetDiameter()
    {
        var eccentricities = GetAllVertexEccentricity();

        return eccentricities.Values.Max();
    }

    public int GetRatio()
    {
        var eccentricities = GetAllVertexEccentricity();

        return eccentricities.Values.Min();
    }

    public Dictionary<T, int> GetAllVertexEccentricity()
    {
        var eccentricity = new Dictionary<T, int>();

        foreach (var vertex in _adjacencyList.Keys)
        {
            eccentricity.Add(vertex, GetVertexEccentricity(vertex));
        }

        return eccentricity;
    }

    public int GetVertexEccentricity(T vertex)
    {
        var queue = new Queue<T>();
        var distance = new Dictionary<T, int>();

        distance[vertex] = 0;
        queue.Enqueue(vertex);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var neighbor in _adjacencyList[current])
            {
                if (!distance.ContainsKey(neighbor))
                {
                    distance[neighbor] = distance[current] + 1;
                    queue.Enqueue(neighbor);
                }
            }
        }

        int eccentricity = distance.Count == 1 ? int.MaxValue : distance.Values.Max();

        return eccentricity;
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
