using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildingButtonPopulator : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    private void Start()
    {
        Populate();
    }
    public void Populate()
    {
        List<BuildingSO> buildingList = GameHandler.instance.buildingList;
        for (int i = 0; i < buildingList.Count; i++)
        {
            Button instedButton = Instantiate(buttonPrefab, transform).GetComponent<Button>();
            BuildingSO tempBuildingSO = buildingList[i];
            instedButton.gameObject.GetComponent<Image>().sprite = tempBuildingSO.icon;
            instedButton.onClick.AddListener(() => { PlayerController.instance.SelectBuilding(tempBuildingSO); });
        }
    }
}
