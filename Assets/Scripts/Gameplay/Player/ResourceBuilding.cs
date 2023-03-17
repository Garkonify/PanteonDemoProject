using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceBuilding : Building
{
    private void Start()
    {
        StartCoroutine(Timer());
    }
    IEnumerator Timer()
    {
        while (true)
        {
            ResourceBuildingSO resourceBuildingSO = myBuilding as ResourceBuildingSO;
            yield return new WaitForSeconds(1);
            GameHandler.instance.GainMoney(resourceBuildingSO.amount);
        }
    }
}
