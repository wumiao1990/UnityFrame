using System;
using UnityEngine;
using UnityEditor;

public class ResourcesMgr : MonoSingleton<ResourcesMgr>
{
    [HideInInspector]
    public Canvas ctrlCanvas;

    public void LoadAsset<T>(string address, Action<GameObject> action)
    {
        var go = Resources.Load<GameObject>(address);
        if (action != null)
        {
            action.Invoke(go);
        }
    }


}