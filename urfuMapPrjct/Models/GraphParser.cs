using UrfuMapProject.Models;

namespace UrfuMapProject.Data.Models;

public class GraphParser
{
    public static Dictionary<string, Graph> MakeInstitutionsGraphs(string relativePathOfDirectory)
    {
        var result = new Dictionary<string,Graph>();
        var fullPathToDirectory = Path.GetFullPath(relativePathOfDirectory);
        foreach (var pathToGraph in Directory.GetFiles(fullPathToDirectory))
        {
            var parsedTxt = ParseTxt(pathToGraph);
            var graph = MakeGraph(parsedTxt);
            var fileName = Path.GetFileName(pathToGraph);
            result[fileName.Substring(0, fileName.Length - 4)] = graph;
        }

        return result;
    }
    public static Graph MakeGraph(List<List<string>> parsedTxt)
    {
        var listVertex = new List<Vertex>();
        foreach (var vertex in parsedTxt)
        {
            listVertex.Add(new Vertex(vertex[0],int.Parse(vertex[1]),int.Parse(vertex[2]),
                vertex[3].Split(',').Select(n => int.Parse(n)).ToList(), new List<Edge>(), vertex[4], vertex[5]));
        }

        foreach (var vertex in listVertex)
        {
            foreach (var index in vertex.NeighboursIndexes)
            {
                vertex.Edges.Add(new Edge(vertex,listVertex[index]));
            }
        }

        return new Graph(listVertex);

    }

    public static MiniGraph MakeMiniGraph(List<List<string>> parsedTxt)
    {
        var listVertex = new List<MiniVertex>();
        foreach (var vertex in parsedTxt)
        {
            listVertex.Add(new MiniVertex(vertex[0],
                vertex[1].Split(',').Select(n => int.Parse(n)).ToList(), new List<MiniEdge>(), vertex[2]));
        }

        foreach (var vertex in listVertex)
        {
            foreach (var index in vertex.NeighboursIndexes)
            {
                if (listVertex[index].Name == "У" || vertex.Name == "У")
                {
                    vertex.Edges.Add(new MiniEdge(vertex, listVertex[index], 1000));
                }

                vertex.Edges.Add(new MiniEdge(vertex,listVertex[index],1));
            }
        }

        return new MiniGraph(listVertex);   
    }

    public static List<List<string>> ParseTxt(string pathToTxt)
    {
        List<List<string>> result = new List<List<string>>();
        string[] lines = System.IO.File.ReadAllLines(pathToTxt);
        
        foreach (var line in lines)
        {
            var parsedLine = new List<string>();
            
            foreach (var word in line.Split(';'))
            {
                parsedLine.Add(word);
            }
            
            result.Add(parsedLine);
        }

        return result;
    }
}
