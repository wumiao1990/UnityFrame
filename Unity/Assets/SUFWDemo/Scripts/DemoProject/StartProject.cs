/***
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

namespace DemoProject
{
	public class StartProject : MonoBehaviour {

		void Start () {
            Log.Write(GetType()+"/Start()/");
            //加载登陆窗体
            UIManager.GetInstance().ShowUIForms(ProConst.LOGON_FROMS);         			
		}
		
	}
}