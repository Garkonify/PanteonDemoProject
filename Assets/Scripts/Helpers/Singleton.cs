using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    public static T instance;
    public virtual void Awake()
    {
        if (instance == null) instance = this as T;
        else
        {
            Debug.LogWarning("One or more instances of " + GetType() + " found Destroying " + transform.name);
            Destroy(gameObject);
        }
    }
}
