using System.Diagnostics;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UrfuMapProject.Models;
using UrfuMapProject.Data.Models;
using System.Xml.XPath;

namespace UrfuMapProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult GetPoints([FromBody] UserData data)
    {
        var graphsPath = @"C:\Users\slugg\source\repos\urfuMapPrjct\urfuMapPrjct\Graphs";
        var miniGraphsPath = @"C:\Users\slugg\source\repos\urfuMapPrjct\urfuMapPrjct\MiniGraph\”Î.txt";

        var dicGraph = GraphParser.MakeInstitutionsGraphs(graphsPath);
        var miniGraph = GraphParser.MakeMiniGraph(GraphParser.ParseTxt(miniGraphsPath));
        var result = CreatePartsPath.CreateAllPath(data, dicGraph, miniGraph);
        var pepega = new List<Tuple<string, List<Object>>>();
        foreach (var pair in result) 
        {
            pepega.Add(new Tuple<string, List<object>>(pair.Item1, pair.Item2.Select(p => new { X = p.X, Y = p.Y }).Cast<object>().ToList()));
        }

        //var tuple = new
        //{
        //    Item1 = "–;1",
        //    Item2 = new List<object> {
        //    new { X = 202, Y = 582 },
        //    new { X = 202, Y = 504 },
        //    new { X = 202, Y = 454 }
        //}
        //};
        //var tuple2 = new
        //{
        //    Item1 = "“;1",
        //    Item2 = new List<object> {
        //    new { X = 286, Y = 408 },
        //    new { X = 286, Y = 384 },
        //     new { X = 286, Y = 278 },
        //      new { X = 286, Y = 192 },
        //       new { X = 286, Y = 130 },
        //        new { X = 232, Y = 130 }
        //}
        //};
        //var json = JsonConvert.SerializeObject(new List<object> { tuple, tuple2 });
        return Ok(pepega);
    }
    //[HttpPost]
    //public Tuple<string,List<Point>> GetPoints([FromBody] UserData data)
    //{

    //    //var graphsPath = @"C:\Users\gglol\RiderProjects\UrfuMapProject\Graphs";
    //    //var miniGraphsPath = @"C:\Users\gglol\RiderProjects\UrfuMapProject\MiniGraph\”Î.txt";

    //    //var dicGraph = GraphParser.MakeInstitutionsGraphs(graphsPath);
    //    //var miniGraph = GraphParser.MakeMiniGraph(GraphParser.ParseTxt(miniGraphsPath));
    //    //var result = CreatePartsPath.CreateAllPath(data, dicGraph, miniGraph);

    //    //var path = @"C:\Users\slugg\source\repos\urfuMapPrjct\urfuMapPrjct\wwwroot\Outdoors.txt";
    //    //var start = data.pointAprefix;
    //    //var end = data.pointBprefix;

    //    //var parsed = GraphParser.ParseTxt(path);
    //    //var graph = GraphParser.MakeGraph(parsed);
    //    //var resultV = CreatePartsPath.FindePathFromInst(graph, start,end);
    //    var tuple = Tuple.Create("string", new List<Point> { new Point(1, 2), new Point(3, 4) });
    //    return tuple;
    //}

    public class UserData
    {
        public string pointAprefix { get; set; }
        public string pointBprefix { get; set; }
        public string pointA { get; set; }
        public string pointB { get; set; }
    }
}
