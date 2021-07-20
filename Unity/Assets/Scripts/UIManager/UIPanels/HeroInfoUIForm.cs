/***
 * 
 *     
 *           主题： 英雄信息显示窗体
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

public class HeroInfoUIForm : BaseUIForm {

	public override void OnReady()
	{
		//窗体性质
		CurrentUIType.UIForms_Type = UIFormType.Fixed;  //固定在主窗体上面显示
	}
	
	public override void Display()
	{
		base.Display();
	}
	
}
