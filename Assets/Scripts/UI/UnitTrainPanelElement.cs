using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitTrainPanelElement : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TMP_Text unitname;

    public void Init(TrainerBuilding building, TrainData unit)
    {
        gameObject.SetActive(true);
        icon.sprite = unit.unitToTrain.icon;
        unitname.text = unit.unitToTrain.name;
        Button button = GetComponent<Button>();
        TrainerBuilding tempBuilding = building;
        TrainData tempunit = unit;
        button.onClick.AddListener(() => tempBuilding.Train(tempunit));
    }
    public void DeInit()
    {
        gameObject.SetActive(false);
        Button button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
    }
}
