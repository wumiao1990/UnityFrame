//using System;
using UnityEngine;
using UnityEngine.UI;

public class MaskScreenManager 
{
    protected static MaskScreenManager instance;
    public static MaskScreenManager GetInstance()
    {
        if (instance == null)
        {
            instance = new MaskScreenManager();
        }
        return instance;
    }

    GameObject goAutoMask;
    /// <summary>
    /// 打开遮罩面板
    /// </summary>
    /// <param name="screen">遮罩所需的参数,会根据param来设置关联的页面</param>
    public void Show(ScreenBase screen)
    {
        if (goAutoMask == null)
        {
            ResourcesMgr.GetInstance().LoadAsset<GameObject>("UIAutoMask", (ao) => {
                goAutoMask = ao;
                AttachEvent(screen);
            });
        }
        else
        {
            AttachEvent(screen);
        }
    }

    void AttachEvent(ScreenBase screen)
    {
        // 以防界面当帧打开后又立即关闭,导致screen为null报错
        if (screen == null || screen.CtrlBase == null) return;
        var go = Object.Instantiate(goAutoMask, screen.mPanelRoot.transform);
        go.transform.SetAsFirstSibling();
        go.name = "UIAutoMask_Created by Mask ScreenManager";
        Button btnMask = go.GetComponent<Button>();
        if (btnMask != null)
        {
            btnMask.onClick.AddListener(screen.OnClickMaskArea);
        }
    }
}