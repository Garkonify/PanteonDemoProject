using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create new Trainer Building")]
public class TrainerBuildingSO : BuildingSO
{
    public List<TrainData> trainableUnits;




}
[System.Serializable]
public class TrainData
{
    public UnitSO unitToTrain;
    public float cost;
    public float time;
}

