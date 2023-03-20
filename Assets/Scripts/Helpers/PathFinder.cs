using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PathFinder
{
    /// <summary>
    /// Returns the path made out of GridNodes 
    /// </summary>
    /// <param name="startNode">Starting Node probably the node object is on</param>
    /// <param name="targetNode">Node for path to finalize at</param>
    /// <param name="goNearest">Used for interaction distance if set pathfinding will stop when come near enough, -1 is deafult value means need to go on top of the node</param>
    /// <returns></returns>
    public static List<GridNode> Find(GridNode startNode, GridNode targetNode, float goNear = -1)
    {
        List<GridNode> openNodes = new List<GridNode>();
        HashSet<GridNode> closedNodes = new HashSet<GridNode>();
        openNodes.Add(startNode);

        while (openNodes.Count > 0)
        {
            GridNode current = openNodes[0];
            for (int i = 1; i < openNodes.Count; i++)
            {
                if (openNodes[i].f < current.f || openNodes[i].f == current.f && openNodes[i].h < current.h)
                {
                    current = openNodes[i];
                }
            }
            openNodes.Remove(current);
            closedNodes.Add(current);
            current.h = GetDistanceBetweenNodes(current, targetNode);
            if (current.h < goNear)
            {
                return RetracePath(startNode, current);
            }
            if (current == targetNode)
            {
                //CONGRATULATIONS !! found it
                return RetracePath(startNode, targetNode);
            }


            foreach (GridNode neighbour in Grid.instance.GetNeighbourNodes(current))
            {
                if (!neighbour.IsAvailable() || closedNodes.Contains(neighbour)) continue;

                int newMovementCostToNeighbour = current.g + GetDistanceBetweenNodes(current, neighbour);
                if (newMovementCostToNeighbour < neighbour.g || !openNodes.Contains(neighbour))
                {
                    neighbour.g = newMovementCostToNeighbour;
                    neighbour.h = GetDistanceBetweenNodes(neighbour, targetNode);
                    neighbour.parentNode = current;
                    if (!openNodes.Contains(neighbour))
                    {
                        openNodes.Add(neighbour);
                    }
                }

            }

        }
        return null;
    }

    static List<GridNode> RetracePath(GridNode startNode, GridNode endNode)
    {
        List<GridNode> path = new List<GridNode>();
        GridNode currentNode = endNode;
        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parentNode;
        }
        path.Reverse();

        return path;
    }


    public static int GetDistanceBetweenNodes(GridNode nodeA, GridNode nodeB)
    {
        int Xdis = Mathf.Abs(nodeA.xIndexInGrid - nodeB.xIndexInGrid);
        int Ydis = Mathf.Abs(nodeA.yIndexInGrid - nodeB.yIndexInGrid);
        if (Xdis > Ydis) return 14 * Ydis + 10 * (Xdis - Ydis);
        return 14 * Xdis + 10 * (Ydis - Xdis);
    }

}
