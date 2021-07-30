﻿/***
 * 
 *     
 *           主题： xxx    
 *    Description: 
 *           功能： yyy
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


public class StartProject : MonoBehaviour {

	void Start () {
        
        NgameLua.Hotfix();

        NgameLua.DoFileString("Main");
        LuaEnvManager.Instanse.InitGlobalLuaFunc();
        UIPathHelper.Init();
        
        //加载登陆窗体
        UIManager.Instance.ShowUIForms(ProConst.LOGON_FROMS);
	}
	
}
