using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class GridMember : MonoBehaviour, IPoolable, ISelectable
{
    public GridNode occupyingGrid;
    public virtual PoolableSO GivePoolData()
    {
        return null;
    }

    public virtual void OnDeselected()
    {

    }

    public virtual void OnSelected()
    {

    }

    protected int healthPoints;
    public virtual bool TakeDamage(int amount)
    {
        healthPoints -= amount;
        if (healthPoints <= 0)
        {
            occupyingGrid.Remove();
            gameObject.SetActive(false);
            PoolManager.instance.Give(gameObject);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Used for Right Click on a empty grid node
    /// </summary>
    /// <param name="node"></param>
    public virtual void RightClickOrderFree(GridNode node)
    {

    }
    /// <summary>
    /// Used for Right click on a occupied grid node
    /// </summary>
    /// <param name="node"></param>
    public virtual void RightClickOrderOccupied(GridNode node)
    {

    }
}
