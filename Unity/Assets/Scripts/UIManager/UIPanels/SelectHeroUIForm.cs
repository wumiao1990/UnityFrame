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
using UITools;
using UnityEngine;
using UnityEngine.UI;

public class SelectHeroUIForm : BaseUIForm
{
    #region 控件绑定变量声明，自动生成请勿手改

    [ControlBinding]
    public Image imgTest;

    #endregion


    public override void OnReady()
    {
        Log.SyncLogCatchToFile();
        //窗体的性质
        CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;
    }
    
    public override void Display()
    {
        base.Display();
        
        //注册进入主城的事件
        RigisterButtonObjectEvent("BtnConfirm",
            p =>
            {
                SceneMgr.Instance.Load("MainScene");
            }

        );

        //注册返回上一个页面
        RigisterButtonObjectEvent("BtnClose",
            m=>CloseUIForm()
        );
        
        imgTest.SetSpritePath("MainMenu/Icon_Character"); // 同步
        //imgTest.SetSpritePathAsync("MainMenu/Icon_Character");//异步
        
        ResourcesMgr.Instance.EffectPerfabOnLoad(imgTest.gameObject,"effect_test", 0);
    }
}
