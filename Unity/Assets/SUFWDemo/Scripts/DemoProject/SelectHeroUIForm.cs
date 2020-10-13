/***
 * 
 *     
 *           主题： PRG 游戏“选择角色”窗体 
 *    Description: 
 *           功能： 选择英雄窗体
 *                  
 *     
 *     
 *    
 *   
 */
using System.Collections;
using System.Collections.Generic;
using SUIFW;
using UnityEngine;

public class SelectHeroUIForm : BaseUIForm
{
    public override void OnReady()
    {
        Log.SyncLogCatchToFile();
        //窗体的性质
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.HideOther;
    }
    
    public override void Display()
    {
        base.Display();
        
        //注册进入主城的事件
        RigisterButtonObjectEvent("BtnConfirm",
            p =>
            {
                OpenUIForm(ProConst.MAIN_CITY_UIFORM);
                OpenUIForm(ProConst.HERO_INFO_UIFORM);
            }

        );

        //注册返回上一个页面
        RigisterButtonObjectEvent("BtnClose",
            m=>CloseUIForm()
        );
    }
}
