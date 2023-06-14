using System.Collections.Generic;
using System.Drawing;
using UrfuMapProject.Models;


namespace UrfuMapProject.Data.Models;
class DijkstraData
{
	public Vertex Previous { get; set; }
	public double Price { get; set; }
}

class MiniDijkstraData
{
	public MiniVertex Previous { get; set; }
	public double Price { get; set; }
}

public class FindingRoutes
{
	public static List<Vertex> Dijkstra(Graph graph, Vertex start, Vertex end)
	{
		var notVisited = new List<Vertex>(graph.ListVertex);
		var track = new Dictionary<Vertex, DijkstraData>();
		track[start] = new DijkstraData { Price = 0, Previous = null };

		while (true)
		{
			Vertex toOpen = null;
			var bestPrice = double.PositiveInfinity;
			foreach (var e in notVisited)
			{
				if (track.ContainsKey(e) && track[e].Price < bestPrice)
				{
					bestPrice = track[e].Price;
					toOpen = e;
				}
			}

			if (toOpen == null) return null;
			if (toOpen == end) break;

			foreach (var e in toOpen.Edges.Where(z => z.Start == toOpen))
			{
				var currentPrice = track[toOpen].Price + e.Length;
				var nextNode = e.End;
				if (!track.ContainsKey(nextNode) || track[nextNode].Price > currentPrice)
				{
					track[nextNode] = new DijkstraData { Previous = toOpen, Price = currentPrice };
				}
			}

			notVisited.Remove(toOpen);
		}

		var result = new List<Vertex>();
		while (end != null)
		{
			result.Add(end); //Добовляет в резалт поинты, а не вершины
			end = track[end].Previous;
		}

		result.Reverse();
		return result;
	}

	public static List<MiniVertex> MiniDijkstra(MiniGraph graph, MiniVertex start, MiniVertex end)
	{
		var notVisited = graph.ListVertex;
		var track = new Dictionary<MiniVertex, MiniDijkstraData>();
		track[start] = new MiniDijkstraData { Price = 0, Previous = null };

		while (true)
		{
			MiniVertex toOpen = null;
			var bestPrice = double.PositiveInfinity;
			foreach (var e in notVisited)
			{
				if (track.ContainsKey(e) && track[e].Price < bestPrice)
				{
					bestPrice = track[e].Price;
					toOpen = e;
				}
			}

			if (toOpen == null) return null;
			if (toOpen == end) break;

			foreach (var e in toOpen.Edges.Where(z => z.Start == toOpen))
			{
				var currentPrice = track[toOpen].Price + e.Length;
				var nextNode = e.End;
				if (!track.ContainsKey(nextNode) || track[nextNode].Price > currentPrice)
				{
					track[nextNode] = new MiniDijkstraData { Previous = toOpen, Price = currentPrice };
				}
			}

			notVisited.Remove(toOpen);
		}

		var result = new List<MiniVertex>();
		while (end != null)
		{
			result.Add(end); //Добовляет в резалт поинты, а не вершины
			end = track[end].Previous;
		}

		result.Reverse();
		return result;
	}

	public List<MiniVertex> GetMiniPath(MiniGraph miniGraph, string pointAPrefix, string pointBPrefix)
	{
		MiniVertex start = null;
		MiniVertex end = null;
		foreach (var miniVertex in miniGraph.ListVertex)
		{
			if (miniVertex.Name.ToLower() == pointAPrefix.ToLower())
			{
				start = miniVertex;
			}

			if (miniVertex.Name.ToLower() == pointBPrefix.ToLower())
			{
				end = miniVertex;
			}
		}

		return MiniDijkstra(miniGraph, start, end);

	}

}