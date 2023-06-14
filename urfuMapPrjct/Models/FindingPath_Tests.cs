/*using NUnit.Framework;

namespace WebProject.Data.Models;
[TestFixture]
public class FindingPath_Tests
{
    [Test]
    public void OutdoorsTestEasy()
    {
        var path = @"C:\Users\gglol\RiderProjects\WebSolution\WebProject\Data\PointsInInstitutes\Outdoors.txt";
        var start = 0;
        var end = 3;

        var parsed = GraphParser.ParseTxt(path);
        var graph = GraphParser.MakeGraph(parsed);
        var resultV = FindingRoutes.Dijkstra(graph, graph.ListVertex[start], graph.ListVertex[end]);
        var result = resultV.Select(x => x.IndexVertex).ToList();
        var a = new List<string> { "0", "1", "3" };
        
        Assert.AreEqual(a,result);
    }
    
    [Test]
    public void OutdoorsTestHard()
    {
        var path = @"C:\Users\gglol\RiderProjects\WebSolution\WebProject\Data\PointsInInstitutes\Outdoors.txt";
        var start = 0;
        var end = 33;

        var parsed = GraphParser.ParseTxt(path);
        var graph = GraphParser.MakeGraph(parsed);
        var resultV = FindingRoutes.Dijkstra(graph, graph.ListVertex[start], graph.ListVertex[end]);
        var result = resultV.Select(x => x.IndexVertex).ToList();
        var a = new List<string> { "0", "34", "33" };
        
        Assert.AreEqual(a,result);
    }
    
}*/