using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridNode : MonoBehaviour
{
    private bool isAvailable = true;
    public GridMember placedObject; //Connected object reference
    public int xIndexInGrid, yIndexInGrid; 
    /// <summary>
    /// returns if grid available for placing / moving 
    /// </summary>
    /// <returns></returns>
    public bool IsAvailable() => isAvailable;
    public void Place(GridMember placable) { GetComponent<SpriteRenderer>().color = Color.red; isAvailable = false; placedObject = placable; }
    public void Remove() { GetComponent<SpriteRenderer>().color = Color.white; isAvailable = true; placedObject = null; }

    #region PathFindingStuff

    public int g; //gScore of node
    public int h; //hScore of node
    public int f
    {
        get { return g + h; }
    }
    public GridNode parentNode; // No touchy (used for pathfinding not constant)


    #endregion



}
