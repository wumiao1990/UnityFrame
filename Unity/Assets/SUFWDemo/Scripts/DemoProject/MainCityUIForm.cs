/***
 * 
 *     
 *           主题： 主城窗体
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


public class MainCityUIForm : BaseUIForm
{
	
	public override void OnReady()
	{
		//窗体性质
		CurrentUIType.UIForms_ShowMode = UIFormShowMode.HideOther;
	}
	
	public override void Display()
	{
		base.Display();
		
		//事件注册
		RigisterButtonObjectEvent("BtnMarket",
			p => OpenUIForm(ProConst.MARKET_UIFORM)           
		);
	}

}
