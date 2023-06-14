using UrfuMapProject.Models;

namespace UrfuMapProject.Data.Models;

public class Graph
{
    public List<Vertex> ListVertex;

    public Graph(List<Vertex> listVertex)
    {
        ListVertex = listVertex;
    }
}
public class Edge
{
    public Vertex Start;
    public Vertex End;
    public double Length;

    public Edge(Vertex start, Vertex end)
    {
        Start = start;
        End = end;
        Length = Math.Sqrt((start.X - end.X) * (start.X - end.X)
                           + (start.Y - end.Y) * (start.Y - end.Y));
    }
}
public class Vertex
{
    public string IndexVertex;
    public int X;
    public int Y;
    public List<int> NeighboursIndexes;
    public List<Edge>? Edges;
    public string Name;
    public string Transition;


    public Vertex(string indexApex, int x, int y, List<int> neighbours, List<Edge>? edges, string name, string transition)
    {
        IndexVertex = indexApex;
        X = x;
        Y = y;
        NeighboursIndexes = neighbours;
        Edges = edges;
        Name = name;
        Transition = transition;
    }
}