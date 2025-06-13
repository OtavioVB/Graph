namespace Graph.Abstractions.Interfaces;

public interface IGraph<T>
{
    public void AddVertex(T vertex);
    public void AddDirectedEdge(T origin, T destination);
    public void AddBidiretionalEdge(T primary, T secondary);
}
