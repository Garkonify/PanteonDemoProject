using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class InfoPanel : Singleton<InfoPanel>
{
    [SerializeField] TMP_Text goldText;

    private void Start()
    {
        GameHandler.instance.onMoneyChange += (int amount) => UpdateGold(amount);
        selectedObjectIcon.gameObject.SetActive(false);
        selectedObjectName.gameObject.SetActive(false);
    }
    private void UpdateGold(int amount)
    {
        goldText.text = "Gold: " + amount;
    }

    public void Select(GridMember selectedObject)
    {
        Debug.Log(selectedObject.GetType());
        HideAllPanels();
        ShowGeneralData(selectedObject);
        switch (selectedObject)
        {
            case Unit unit:
                ShowUnitData(unit);
                break;
            case ResourceBuilding resourceBuilding:
                ShowResourceBuildingData(resourceBuilding);
                break;
            case TrainerBuilding trainerBuilding:
                ShowTrainPanel(trainerBuilding);
                break;
        }
    }

    public void Deselect()
    {
        HideAllPanels();
    }

    [SerializeField] Image selectedObjectIcon;
    [SerializeField] TMP_Text selectedObjectName;
    public void ShowGeneralData(GridMember selectedObject)
    {
        selectedObjectIcon.gameObject.SetActive(true);
        selectedObjectName.gameObject.SetActive(true);
        selectedObjectIcon.sprite = selectedObject.GivePoolData().icon;
        selectedObjectName.text = selectedObject.GivePoolData().name;
    }
    private void ShowUnitData(Unit unit)
    {


    }
    private void ShowResourceBuildingData(ResourceBuilding resourceBuilding)
    {


    }

    [SerializeField] GameObject trainPanel;
    private void ShowTrainPanel(TrainerBuilding trainerBuilding)
    {
        trainPanel.SetActive(true);
        TrainerBuildingSO buildingSO = trainerBuilding.myBuilding as TrainerBuildingSO;
        for (int i = 0; i < trainPanel.transform.childCount; i++)
        {
            trainPanel.transform.GetChild(i).GetComponent<UnitTrainPanelElement>().DeInit();
        }
        for (int i = 0; i < buildingSO.trainableUnits.Count; i++)
        {
            trainPanel.transform.GetChild(i).GetComponent<UnitTrainPanelElement>().Init(trainerBuilding, buildingSO.trainableUnits[i]);
        }
    }
    void HideAllPanels()
    {
        selectedObjectIcon.gameObject.SetActive(false);
        selectedObjectName.gameObject.SetActive(false);
        trainPanel.SetActive(false);
    }


}
