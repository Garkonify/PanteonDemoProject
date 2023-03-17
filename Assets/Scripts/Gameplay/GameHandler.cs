using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : Singleton<GameHandler>
{
    public List<BuildingSO> buildingList;
    float money = 150;
    public Action<int> onMoneyChange;
    public int Money
    {
        get { return (int)money; }
    }
    private void Start()
    {
        onMoneyChange?.Invoke((int)Money);
    }
    public void GainMoney(float amount)
    {
        MoneyChange(amount);
    }
    public void LoseMoney(float amount)
    {
        if (hasEnoughMoney(amount))
        {
            MoneyChange(-amount);
        }

    }
    void MoneyChange(float amount)
    {
        money += amount;
        money = Mathf.Clamp(money, 0, float.MaxValue);
        onMoneyChange?.Invoke((int)Money);
    }
    public bool hasEnoughMoney(float amount) => money >= amount;
}
