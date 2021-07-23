﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour {

    [SerializeField]
    private Image image;

    private bool isFirstUpdate = true;
    private void Awake() {
        
    }

    private void Update()
    {
        image.fillAmount = SceneMgr.Instance.GetLoadingProgress();
    }
}
