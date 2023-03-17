using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] SpriteRenderer silhouette;
    private void Start()
    {
        PlayerInput.instance.onClickAvailableGrid += (GridNode node) => { OnClickAvailableGrid(node); };
        PlayerInput.instance.onClickOccupiedGrid += (GridNode node) => { OnClickOccupiedGrid(node); };
        PlayerInput.instance.onMouseHoverAvailableGrid += (GridNode node) => { silhouette.transform.position = node.transform.position; };
        PlayerInput.instance.onMouseRightClick += () => { OnRightClick(); };
        silhouette.gameObject.SetActive(false);
    }

    private void OnRightClick()
    {
        if (selectedBuilding != null)
        {
            selectedBuilding = null;
            InfoPanel.instance.Deselect();
            silhouette.gameObject.SetActive(false);
        }
    }

    [SerializeField] BuildingSO selectedBuilding;
    [SerializeField] GridMember selectedObject;

    public void SelectBuilding(BuildingSO select)
    {
        if (selectedObject != null) selectedObject = null;
        selectedBuilding = select;
        silhouette.gameObject.SetActive(true);
        silhouette.sprite = selectedBuilding.icon;
    }

    void OnClickAvailableGrid(GridNode node)
    {
        if (selectedBuilding != null)
        {
            if (GameHandler.instance.hasEnoughMoney(selectedBuilding.price))
            {
                GameHandler.instance.LoseMoney(selectedBuilding.price);
                Create(node);
                selectedBuilding = null;
                InfoPanel.instance.Deselect();
                silhouette.gameObject.SetActive(false);
            }
        }
        else if (selectedObject != null)
        {

            selectedObject = null;
        }
    }
    void OnClickOccupiedGrid(GridNode node)
    {
        if (selectedBuilding != null)
        {

        }
        else if (selectedObject != null)
        {
            if (node.placedObject != null)
            {
                selectedObject = node.placedObject;
                InfoPanel.instance.Select(selectedObject);
                PlayerInput.instance.onRightClickAvailableGrid = (GridNode node) => { selectedObject.RightClickOrderFree(node); };
                PlayerInput.instance.onRightClickOccupiedGrid = (GridNode node) => { selectedObject.RightClickOrderOccupied(node); };
            }
        }
        else
        {
            if (node.placedObject != null)
            {
                selectedObject = node.placedObject;
                InfoPanel.instance.Select(selectedObject);
                PlayerInput.instance.onRightClickAvailableGrid = (GridNode node) => { selectedObject.RightClickOrderFree(node); };
                PlayerInput.instance.onRightClickOccupiedGrid = (GridNode node) => { selectedObject.RightClickOrderOccupied(node); };
            }
        }
    }

    private void Create(GridNode node)
    {

        Create(selectedBuilding, node);
    }
    public void Create(PoolableSO poolable, GridNode node)
    {
        if (node.IsAvailable())
        {
            GameObject go = PoolManager.instance.Get(poolable);
            go.SetActive(true);
            go.transform.position = node.transform.position;
            GridMember member = go.GetComponent<GridMember>();
            member.occupyingGrid = node;
            node.Place(member);
        }
        else
        {
            Create(poolable, Grid.instance.AskNearNode(node));
        }
    }
    private void Remove(GridNode node)
    {
        PoolManager.instance.Give(node.placedObject.gameObject);
        node.placedObject.gameObject.SetActive(false);
        node.Remove();
    }
}
