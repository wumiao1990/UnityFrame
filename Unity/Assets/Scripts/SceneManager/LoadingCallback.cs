using System;
using System.Collections;
using System.Collections.Generic;
using SUIFW;
using UnityEngine;

public class LoadingCallback : MonoBehaviour {

    private bool isFirstUpdate = true;

    private void Update() {
        if (isFirstUpdate) {
            isFirstUpdate = false;
            
            //清理资源
            UIManager.GetInstance().DestroyAllPanel();
            
            NgameLua.FullLuaGC();
            GC.Collect();
            Resources.UnloadUnusedAssets();
            
            //准备Load下一个场景
            SceneMgr.Instance.LoaderCallback();
        }
    }

}
