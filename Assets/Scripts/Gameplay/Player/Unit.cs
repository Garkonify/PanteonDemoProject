using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Unit : GridMember
{
    public UnitSO myUnit;
    IMove mover; // Units Movement script reference (Every Unit must have a move script)
    IInteraction interaction; // Units Interaction script reference (Every Unit must have a interaction script)
    public Action onInputReceived;
    private void Awake()
    {
        mover = GetComponent<IMove>();
        interaction = GetComponent<IInteraction>();
        healthPoints = myUnit.hp;
    }
    public override PoolableSO GivePoolData()
    {
        //Easy way to access PoolData (For Instanciating Purposes)
        return myUnit;
    }

    public override void RightClickOrderFree(GridNode node)
    {
        onInputReceived?.Invoke();
        base.RightClickOrderFree(node);
        //Move To Node;
        mover.Move(node);
    }
    public override void RightClickOrderOccupied(GridNode node)
    {
        onInputReceived?.Invoke();
        base.RightClickOrderOccupied(node);
        //Interact With Node (Probably Only Attack);
        interaction.Interact(node);
    }
}
