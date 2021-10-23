using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPathfinding : MonoBehaviour
{
	public GameObject start, end;
	public StarGrid grid;

	void Awake()
	{
		grid = GetComponent<StarGrid>();
		//FindPath(start.transform.position, end.transform.position);
	}

	public void FindPath(Vector3 startPos, Vector3 targetPos)
	{
		StarNode startNode = grid.NodeFromWorldPoint(startPos);
		StarNode targetNode = grid.NodeFromWorldPoint(targetPos);

		List<StarNode> openSet = new List<StarNode>();
		HashSet<StarNode> closedSet = new HashSet<StarNode>();
		openSet.Add(startNode);

		while (openSet.Count > 0)
		{
			StarNode node = openSet[0];
			for (int i = 1; i < openSet.Count; i++)
			{
				if (openSet[i].fCost < node.fCost || openSet[i].fCost == node.fCost)
				{
					if (openSet[i].hCost < node.hCost)
						node = openSet[i];
				}
			}

			openSet.Remove(node);
			closedSet.Add(node);

			if (node == targetNode)
			{
				RetracePath(startNode, targetNode);
				return;
			}

			foreach (StarNode neighbour in grid.GetNeighbours(node))
			{
				if (!neighbour.walkable || closedSet.Contains(neighbour))
				{
					continue;
				}

				int newCostToNeighbour = node.gCost + GetDistance(node, neighbour);
				if (newCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
				{
					neighbour.gCost = newCostToNeighbour;
					neighbour.hCost = GetDistance(neighbour, targetNode);
					neighbour.parent = node;

					if (!openSet.Contains(neighbour))
						openSet.Add(neighbour);
				}
			}
		}
	}

	void RetracePath(StarNode startNode, StarNode endNode)
	{
		List<StarNode> path = new List<StarNode>();
		StarNode currentNode = endNode;

		while (currentNode != startNode)
		{
			path.Add(currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse();

		grid.path = path;

	}

	int GetDistance(StarNode nodeA, StarNode nodeB)
	{
		int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
		int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

		if (dstX > dstY)
			return 14 * dstY + 10 * (dstX - dstY);
		return 14 * dstX + 10 * (dstY - dstX);
	}
}
