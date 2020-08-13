/***
 * 
 *     
 *           主题： “商城窗体”   
 *    Description: 
 *           功能：
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
using UnityEngine.UI;

namespace DemoProject
{
    public class MarketUIFrom : BaseUIForm
    {
		void Awake ()
        {
		    //窗体性质
		    CurrentUIType.UIForms_Type = UIFormType.PopUp;  //弹出窗体
		    CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Translucence;
		    CurrentUIType.UIForms_ShowMode = UIFormShowMode.ReverseChange;

            //注册按钮事件：退出
            RigisterButtonObjectEvent("Btn_Close",
                P=> CloseUIForm()                
                );

            //注册道具事件：盔甲 
            RigisterButtonObjectEvent("BtnCloth",
                P =>
                {
                    //打开子窗体
                    OpenUIForm(ProConst.PRO_DETAIL_UIFORM);
                    //传递数据
                    string[] strArray = new string[] { "盔甲详情", "盔甲详细介绍。。。" };
                    SendMessage("Props", "cloth", strArray);
                }
                );
        }
		
	}
}