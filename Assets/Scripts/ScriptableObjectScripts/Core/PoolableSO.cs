using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolableSO : ScriptableObject
{
    [Tooltip("Prefab for Pooling (Pool will return an instance of this)")] public GameObject prefab;

    [Tooltip("Icon used for both ui and in game")] public Sprite icon;
    public string objectName;
}
