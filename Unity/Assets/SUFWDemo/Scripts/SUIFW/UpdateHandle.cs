//==============================================================
//  Copyright (C) 2017 
//  Create by ChengBo at 2017/4/18 18:48:44.
//  Version 1.0
//  Administrator 
//==============================================================

using UnityEngine;

using System;
public class UpdateHandle : MonoBehaviour
{
    public Action UpdateDelegete;
    public Action LateUpdateDelegete;
    public Action FixedUpdateDelegete;
    // Update is called once per frame
    void Update()
    {
        if (UpdateDelegete != null)
        {
            UpdateDelegete.Invoke();
        }
    }

    void LateUpdate()
    {
        if (LateUpdateDelegete != null)
        {
            LateUpdateDelegete.Invoke();
        }
    }

    private void FixedUpdate()
    {
        if (FixedUpdateDelegete != null)
        {
            FixedUpdateDelegete.Invoke();
        }

    }


}

