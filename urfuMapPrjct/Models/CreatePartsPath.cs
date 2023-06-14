using System.Drawing;
using UrfuMapProject.Controllers;
using UrfuMapProject.Models;

namespace UrfuMapProject.Data.Models;

public class CreatePartsPath
{
    public static List<Point> FindPathFromInst(Graph graph, string instA, string instB)
    {
        Vertex aVertex = null;
        Vertex bVertex = null;
        foreach (var i in graph.ListVertex)
        {
            if (i.Name.ToLower().Equals(instA.ToLower()))
                aVertex = i;
            else if (i.Name.ToLower().Equals(instB.ToLower()))
                bVertex = i;
        }

        var result = FindingRoutes.Dijkstra(graph, aVertex, bVertex);

        return result.Select(x => new Point(x.X, x.Y)).ToList();
    }

    public static List<Tuple<string, List<Vertex>>> CreateAllPath(HomeController.UserData userData,
        Dictionary<string, Graph> graphs, MiniGraph miniGraph)
    {
        var instA = userData.pointAprefix;
        var instB = userData.pointBprefix;
        var pointA = userData.pointA;
        var pointB = userData.pointB;
        
        var way = FindingRoutes.MiniDijkstra
            (miniGraph, FindMiniVirtex(miniGraph, instA), FindMiniVirtex(miniGraph, instB));
        
        var nowStartVertexName = pointA;
        var nowGraphName = pointA != ""?instA + pointA[0]: instA ; //Р1-институт+этаж 
        var absEndVertexName = pointB;
       
        var nowGrah = graphs[nowGraphName];
        var nowStartVertex = FindVirtex(nowGrah, nowStartVertexName);
        
        
        string nowEndVertexName;
        Vertex nowEndVertex;
        List <Vertex>nowPath;
        
        var resultVertex = new List<Tuple<string, List<Vertex>>>();
        
        for (int i = 0; i < way.Count - 1; i++)
        {
            nowEndVertexName = way[i + 1].Name;
            nowEndVertex = FindVirtex(nowGrah, nowEndVertexName);

            if (nowEndVertex == null)//И - ИМЯ, И2 - ПЕРЕХОД
            {
                var bestLadderPath = FindBestLadder(nowGrah, FindVirtex(nowGrah, nowStartVertexName));
                resultVertex.Add(new Tuple<string, List<Vertex>>(nowGraphName,bestLadderPath ));
                nowStartVertexName = bestLadderPath.Last().Name;
                
                foreach (var key in graphs.Keys)
                {
                    if (key.Substring(0, key.Length - 1).Equals(nowGraphName.Substring(0, nowGraphName.Length - 1))
                        && FindVirtex(graphs[key],nowEndVertexName)!= null)
                    {
                        nowGraphName = key;
                        nowGrah = graphs[key];
                        nowStartVertex = FindVirtex(nowGrah, nowStartVertexName);
                        nowEndVertex = FindVirtex(nowGrah, nowEndVertexName);
                        break;
                    }
                }
            }

            nowPath = FindingRoutes.Dijkstra(nowGrah, nowStartVertex, nowEndVertex);
            resultVertex.Add(new Tuple<string, List<Vertex>>(nowGraphName,nowPath));
            nowGraphName = nowPath.Last().Transition;
            nowGrah = graphs[nowGraphName];
            nowStartVertexName = way[i].Name;
            nowStartVertex = FindVirtex(nowGrah, nowStartVertexName);
        }
        
        nowEndVertexName = absEndVertexName;
        nowEndVertex = FindVirtex(nowGrah, absEndVertexName);

        if (nowEndVertex == null)//И - ИМЯ, И2 - ПЕРЕХОД
        {
            var bestLadderPath = FindBestLadder(nowGrah, FindVirtex(nowGrah, nowStartVertexName));
            resultVertex.Add(new Tuple<string, List<Vertex>>(nowGraphName,bestLadderPath ));
            nowStartVertexName = bestLadderPath.Last().Name;
                
            foreach (var key in graphs.Keys)
            {
                if (key.Substring(0, key.Length - 1).Equals(nowGraphName.Substring(0, nowGraphName.Length - 1))
                    && FindVirtex(graphs[key],nowEndVertexName)!= null)
                {
                    nowGraphName = key;
                    nowGrah = graphs[key];
                    nowStartVertex = FindVirtex(nowGrah, nowStartVertexName);
                    nowEndVertex = FindVirtex(nowGrah, nowEndVertexName);
                    break;
                }
            }
        }
        
        nowPath = FindingRoutes.Dijkstra(nowGrah, nowStartVertex, nowEndVertex);
        resultVertex.Add(new Tuple<string, List<Vertex>>(nowGraphName,nowPath));

        return resultVertex;
    }
    
    public static Vertex? FindVirtex(Graph graph, string name)
    {
        foreach (var vertex in graph.ListVertex)
        {
            if (vertex.Name.ToLower().Equals(name.ToLower())) return vertex;
        }

        return null;
    }

    public static MiniVertex? FindMiniVirtex(MiniGraph graph, string name)
    {
        foreach (var vertex in graph.ListVertex)
        {
            if (vertex.Name.ToLower().Equals(name.ToLower())) return vertex;
        }

        return null;
    }

    public static List<Vertex> FindBestLadder(Graph graph, Vertex start)
    {
        List<Vertex> ladders = new List<Vertex>();
        foreach (var vertex in graph.ListVertex)
        {
            if (vertex.Transition.ToLower().Equals("л"))
                ladders.Add(vertex);
        }

        List<Vertex> bestPath = new List<Vertex>();
        int bestLength = 1000000000;
        foreach (var end in ladders)
        {
            var nowPath = FindingRoutes.Dijkstra(graph, start, end);
            if (nowPath.Count < bestLength)
            {
                bestLength = nowPath.Count;
                bestPath = nowPath;
            }
        }

        return bestPath;
    }
}
