using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : GridMember, IPoolable
{
    [SerializeField] public BuildingSO myBuilding;



    private void Awake()
    {
        healthPoints = myBuilding.hp;
    }

    public override PoolableSO GivePoolData()
    {
        return myBuilding;
    }
}
