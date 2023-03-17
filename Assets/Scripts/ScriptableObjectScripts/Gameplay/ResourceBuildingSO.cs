using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create new Resource Building")]
public class ResourceBuildingSO : BuildingSO
{
    public ResourceType resourceType;
    public float amount;
}
public enum ResourceType { Gold }