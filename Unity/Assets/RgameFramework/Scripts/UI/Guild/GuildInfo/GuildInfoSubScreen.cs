using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuildInfoSubScreen : SubScreenBase
{
    GuildInfoSubCtrl mCtrl;// 创建公会子控件

    public GuildInfoSubScreen(GuildInfoSubCtrl subCtrl) :base(subCtrl)
    {
        
    }

    protected override void Init()
    {
        mCtrl = mCtrlBase as GuildInfoSubCtrl;
        mCtrl.btnClose.onClick.AddListener(OnCloseClick);
        mCtrl.btnJumpTask.onClick.AddListener(OnOpenTaskClick);
    }

    void OnCloseClick()
    {
        GameUIManager.GetInstance().CloseUI(typeof(GuildScreen));
    }

    void OnOpenTaskClick()
    {
        GameUIManager.GetInstance().OpenUI(typeof(TaskScreen));
    }

}
