using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Grid : Singleton<Grid>
{


    [SerializeField] int width, height;
    [SerializeField] Vector2 gridNodeSize;
    [SerializeField] Vector2 gridOriginOffset;

    [SerializeField] GameObject gridNode;

    private GridNode[,] gridArray;
    private void Start()
    {
        CreateGrid();
    }
    private void CreateGrid()
    {
        gridArray = new GridNode[width, height];
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = CreateGridNode();
                gridArray[x, y].xIndexInGrid = x;
                gridArray[x, y].yIndexInGrid = y;
                Transform t = gridArray[x, y].transform;
                t.localPosition = GetNodeLocalPosition(x, y);
            }
        }
    }
    /// <summary>
    /// Creates a GridNode and returns it
    /// </summary>
    /// <returns></returns>
    private GridNode CreateGridNode()
    {
        GameObject instedGridNode = Instantiate(gridNode, transform);
        if (instedGridNode.TryGetComponent(out GridNode node))
            return node;
        Debug.LogWarning("GridNode component cant be found on GridPrefab adding automatically");
        node = instedGridNode.AddComponent<GridNode>();
        return node;
    }
    /// <summary>
    /// Returns center position of a given index
    /// </summary>
    /// <param name="x">Row index</param>
    /// <param name="y">Column index</param>
    /// <returns></returns>
    public Vector3 GetNodeLocalPosition(int x, int y)
    {
        float xPos = (x * gridNodeSize.x) + gridOriginOffset.x;
        float yPos = (y * gridNodeSize.y) + gridOriginOffset.y;
        return new Vector3(xPos, yPos);
    }
    public Vector3 GetNodeGlobalPosition(int x, int y)
    {
        Vector3 pos = GetNodeLocalPosition(x, y);
        return pos + transform.position;
    }
    public GridNode AskNearNode(GridNode node)
    {

        if (node.yIndexInGrid > 1)
        {
            return gridArray[node.xIndexInGrid, node.yIndexInGrid - 1];
        }

        return null;
    }

    private bool GridIndexes(GridNode node, out int xIndex, out int yIndex)
    {
        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                if (gridArray[x, y] == node)
                {
                    xIndex = x; yIndex = y; return true;
                }

            }
        }
        xIndex = -1; yIndex = -1; return false;
    }
    public bool TryGetGridNode(Vector2 pos, out GridNode node)
    {
        int x = Mathf.FloorToInt((pos.x + (gridNodeSize.x / 2) - transform.localPosition.x) / gridNodeSize.x);
        int y = Mathf.FloorToInt((pos.y + (gridNodeSize.y / 2) - transform.localPosition.y) / gridNodeSize.y);
        if (x >= 0 && y >= 0 && x < gridArray.GetLength(0) && y < gridArray.GetLength(1))
        {
            node = gridArray[x, y];
            return true;
        }
        else
        {
            node = null;
            return false;
        }
    }
    public List<GridNode> GetNeighbourNodes(GridNode node)
    {
        List<GridNode> neigbourNodes = new List<GridNode>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;
                int xCheck = node.xIndexInGrid + x;
                int yCheck = node.yIndexInGrid + y;
                if (xCheck >= 0 && xCheck < gridArray.GetLength(0) && yCheck >= 0 && yCheck < gridArray.GetLength(1))
                {
                    neigbourNodes.Add(gridArray[xCheck, yCheck]);
                }
            }
        }

        return neigbourNodes;
    }

}
