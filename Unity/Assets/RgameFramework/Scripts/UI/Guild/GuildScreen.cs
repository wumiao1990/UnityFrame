using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GuildScreen : ScreenBase
{
    GuildCtrl mCtrl;

    GuildCreateSubScreen mSubCreate;// 创建公会界面逻辑
    GuildInfoSubScreen mSubInfo;        // 公会详情界面逻辑

    public GuildScreen(UIOpenScreenParameterBase param = null) : base(UIConst.UIGuild) { }

    protected override void OnLoadSuccess()
    {
        base.OnLoadSuccess();
        mCtrl = mCtrlBase as GuildCtrl;

        // 监听公会创建成功事件
        mCtrl.AutoRelease(Events.OnGuildCreated.Subscribe(OnGuildCreated));

        // 有公会就打开公会详情 没有就打开创建界面
        bool bHaveGuild = PlayerData.GetInstance().HaveGuild();
        // 处理子界面的显示隐藏
        mCtrl.subInfo.gameObject.SetActive(bHaveGuild);
        mCtrl.subCreate.gameObject.SetActive(!bHaveGuild);

        // 处理子界面逻辑初始化
        if (bHaveGuild)
        {
            mSubInfo = new GuildInfoSubScreen(mCtrl.subInfo);
        }
        else
        {
            mSubCreate = new GuildCreateSubScreen(mCtrl.subCreate);
        }
    }

    void OnGuildCreated(bool bCreated)
    {
        // 这里可以直接从PlayerData拿数据，也可以通过参数传递进来，就看你定义的时候是否给事件定义参数了
        //bCreated = PlayerData.GetInstance().HaveGuild();
        // 处理子界面的显示隐藏
        mCtrl.subInfo.gameObject.SetActive(bCreated);
        mCtrl.subCreate.gameObject.SetActive(!bCreated);

        // 处理子界面逻辑初始化
        if (bCreated)
        {
            mSubInfo = new GuildInfoSubScreen(mCtrl.subInfo);
        }
        else
        {
            mSubCreate = new GuildCreateSubScreen(mCtrl.subCreate);
        }
    }


}
