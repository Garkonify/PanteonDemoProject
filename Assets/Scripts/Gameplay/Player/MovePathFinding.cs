using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MovePathFinding : MonoBehaviour, IMove
{

    List<GridNode> currentPath = new List<GridNode>();
    Unit myUnit;

    public event Action onMoveFinalized;

    private void Awake()
    {
        myUnit = GetComponent<Unit>();
        myUnit.onInputReceived += Stop;
    }
    void Stop()
    {
        StopAllCoroutines();
    }
    public void Move(GridNode node)
    {
        currentPath = PathFinder.Find(myUnit.occupyingGrid, node);
        if (currentPath != null) StartCoroutine(MoveToPosition());
    }
    public void Move(GridNode node, float range)
    {
        currentPath = PathFinder.Find(myUnit.occupyingGrid, node, range);
        if (currentPath != null) StartCoroutine(MoveToPosition());
    }
    IEnumerator MoveToPosition()
    {
        onMoveFinalized = null;
        while (currentPath.Count > 0)
        {

            if (currentPath[0].IsAvailable())
            {
                myUnit.occupyingGrid.Remove();
                myUnit.occupyingGrid = currentPath[0];
                currentPath[0].Place(myUnit);
                transform.position = currentPath[0].transform.position;
                currentPath.RemoveAt(0);
            }
            else
            {
                Move(currentPath[currentPath.Count - 1]);
            }
            yield return new WaitForSeconds(1 / myUnit.myUnit.moveSpeed);
        }
        onMoveFinalized?.Invoke();
    }

}
