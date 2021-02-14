using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class SearchBFS
{
    public static bool Search(GraphNode source, GraphNode destination, ref List<GraphNode> path, int maxSteps)
    {
		// set found bool flag and the current number of steps
		bool found = false;
		int steps = 0;

		// <create queue>
		Queue<GraphNode> nodes = new Queue<GraphNode>();
		// <set source node visited to true>
		source.Visited = true;
		// <enqueue source node>
		nodes.Enqueue(source);

		while (!found && nodes.Count > 0 && steps++ < maxSteps)
		{
			GraphNode node = nodes.Dequeue();//< dequeue node >

			// go through edges of node
			foreach (GraphNode.Edge edge in node.Edges)
			{
				// if nodeB is not visited
				if (edge.nodeB.Visited == false)
				{
					// <set nodeB visited to true>
					edge.nodeB.Visited = true;
					// <set nodeB parent to node>
					edge.nodeB.Parent = node;
					// <enqueue nodeB>
					nodes.Enqueue(edge.nodeB);
				}
				if (edge.nodeB == destination) //(< check if nodeB is the destination node >)
				{
					// <set found to true>
					found = true;
					// <break from foreach>
					break;
				}
			}
		}

		// create a list of graph nodes (path)
		path = new List<GraphNode>();

		// if found is true
		if (found)
		{
			GraphNode node = destination;//< set node to destination >
		    // while node not null
			while (node != null)
			{
				// <add node to path list>
				path.Add(node);
				// <set node to node.Parent>
				node = node.Parent;
			}
			// reverse path
			path.Reverse();
		}
		else
		{
			path = nodes.ToList();
		}

		return found;

	}
}
