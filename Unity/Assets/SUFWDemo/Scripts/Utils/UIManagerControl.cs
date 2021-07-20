using SDGame;
using System;
using UnityEngine;
using UITools;
using SUIFW;

public class UIManagerControl : UnitySingleton<UIManagerControl>
{

	void Start () {
        // TODO get config from xml
        IBindableUI uiA = Activator.CreateInstance(Type.GetType("UIA")) as IBindableUI;
        GameObject prefab = Resources.Load<GameObject>("UI/UIA");
        GameObject go = Instantiate(prefab);
        UIControlData ctrlData = go.GetComponent<UIControlData>();
        if(ctrlData != null)
        {
            ctrlData.BindDataTo(uiA);
        }

        (uiA as BaseUIForm).Source = go;
	}
	
	void Update () {
		
	}
}
