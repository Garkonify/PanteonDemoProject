using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class TrainerBuilding : Building
{

    public void Train(TrainData unitToTrain, GridNode node)
    {
        if (GameHandler.instance.hasEnoughMoney(unitToTrain.cost))
        {
            GameHandler.instance.LoseMoney(unitToTrain.cost);
        PlayerController.instance.Create(unitToTrain.unitToTrain, node);
        }
    }
    public void Train(TrainData unitToTrain)
    {
        if (Grid.instance.TryGetGridNode(transform.position, out GridNode node))
            Train(unitToTrain, node);
    }
}
