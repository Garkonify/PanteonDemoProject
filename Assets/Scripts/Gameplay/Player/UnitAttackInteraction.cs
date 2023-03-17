using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAttackInteraction : MonoBehaviour, IInteraction
{
    IMove mover;
    Unit myUnit;
    GridNode targetNode;
    float timer = 0;
    private void Awake()
    {
        mover = GetComponent<IMove>();
        myUnit = GetComponent<Unit>();
        myUnit.onInputReceived += Stop;
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (targetNode != null && timer < 0)
        {
            Attack(targetNode);
        }
    }
    void Stop()
    {
        //StopAllCoroutines();
        targetNode = null;
    }
    public void Interact(GridNode node)
    {
        if (PathFinder.GetDistanceBetweenNodes(myUnit.occupyingGrid, node) < myUnit.myUnit.attackRange)
        {
            targetNode = node;
        }
        else
        {
            mover.Move(node, myUnit.myUnit.attackRange);
            mover.onMoveFinalized += () => Interact(node);
        }
    }

    public void Attack(GridNode node)
    {
        if (PathFinder.GetDistanceBetweenNodes(myUnit.occupyingGrid, node) < myUnit.myUnit.attackRange)
        {
            timer = myUnit.myUnit.attackSpeed;
            if (node.placedObject != null && node.placedObject.TakeDamage(myUnit.myUnit.damage))
            {
                
            }
            else
            {
                Stop();
            }

        }
        else
        {
            Interact(node);
        }
    }
}
