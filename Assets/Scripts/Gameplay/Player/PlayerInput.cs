using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerInput : Singleton<PlayerInput>
{
   // public Action<Vector2> onMouseDown; 
    public Action<GridNode> onClickAvailableGrid; 
    public Action<GridNode> onClickOccupiedGrid; 
    public Action<GridNode> onRightClickAvailableGrid; 
    public Action<GridNode> onRightClickOccupiedGrid; 
    public Action<GridNode> onMouseHoverAvailableGrid; 
    public Action<GridNode> onMouseHoverOccupiedGrid;
    public Action onMouseRightClick;
    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButtonDown(0) )
            {
                if (Grid.instance.TryGetGridNode(Camera.main.ScreenToWorldPoint(Input.mousePosition), out GridNode node))
                {
                    if (node.IsAvailable())
                    {
                        onClickAvailableGrid?.Invoke(node);
                    }
                    else
                    {
                        onClickOccupiedGrid?.Invoke(node);
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                onMouseRightClick?.Invoke();
                if (Grid.instance.TryGetGridNode(Camera.main.ScreenToWorldPoint(Input.mousePosition), out GridNode node))
                {
                    
                    if (node.IsAvailable())
                    {
                        onRightClickAvailableGrid?.Invoke(node);
                    }
                    else
                    {
                        onRightClickOccupiedGrid?.Invoke(node);
                    }
                }
            }


            if (Grid.instance.TryGetGridNode(Camera.main.ScreenToWorldPoint(Input.mousePosition), out GridNode hoveredNode))
            {
                if (hoveredNode.IsAvailable())
                {
                    onMouseHoverAvailableGrid?.Invoke(hoveredNode);
                }
                else
                {
                    onMouseHoverOccupiedGrid?.Invoke(hoveredNode);
                }
            }
        }

    }
}
