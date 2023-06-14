namespace UrfuMapProject.Models;

public class MiniGraph
{
    public List<MiniVertex> ListVertex;

    public MiniGraph(List<MiniVertex> listVertex)
    {
        ListVertex = listVertex;
    }
}

public class MiniEdge
{
    public MiniVertex Start;
    public MiniVertex End;
    public double Length;

    public MiniEdge(MiniVertex start, MiniVertex end, double length)
    {
        Start = start;
        End = end;
        Length = length;
    }
}

public class MiniVertex
{
    public string Index;
    public List<int> NeighboursIndexes;
    public List<MiniEdge>? Edges;
    public string Name;
    
    public MiniVertex(string IndexVertex,List<int> neighbours,List<MiniEdge>? edges, string name)
    {
        IndexVertex = IndexVertex;
        NeighboursIndexes = neighbours;
        Edges = edges;
        Name = name;
    }

}