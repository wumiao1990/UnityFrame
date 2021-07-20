/***
 * 
 *     
 *           主题： 登陆窗体   
 *    Description: 
 *           功能： 
 *                  
 *     
 *     
 *    
 *   
 */
using System.Collections;
using UITools;
using SUIFW;
using UnityEngine;
using UnityEngine.UI;


public class LogonUIForm : BaseUIForm
{
    #region 控件绑定变量声明，自动生成请勿手改
#pragma warning disable 0649
    [ControlBinding]
    public Button Btn_OK;
    [ControlBinding]
    public Text TxtTitle;

#pragma warning restore 0649
    #endregion

    public override void OnReady()
    {
        //定义本窗体的性质(默认数值，可以不写)
        base.CurrentUIType.UIForms_Type = UIFormType.Normal;
        base.CurrentUIType.UIForms_ShowMode = UIFormShowMode.Normal;
        base.CurrentUIType.UIForm_LucencyType = UIFormLucenyType.Lucency;
    }
    
    public override void Display()
    {
        base.Display();
        
        Debug.LogError("Display");
        
        /* 给按钮注册事件 */
        //RigisterButtonObjectEvent("Btn_OK", LogonSys);
        //Lamda表达式写法
        RigisterButtonObjectEvent("Btn_OK", 
            p=>OpenUIForm(ProConst.SELECT_HERO_FORM)
        );
        
        //string strDisplayInfo = LauguageMgr.GetInstance().ShowText("LogonSystem");

        if (TxtTitle)
        {
            TxtTitle.text = Show("LogonSystem");
        }
//            if (TxtLogonNameByBtn)
//            {
//                TxtLogonNameByBtn.text = Show("LogonSystem");
//            }

        //ActiveUpdate = true;

        CoroutineMgr.Instance.StartCoroutine(CoroutineTest());
    }

    IEnumerator CoroutineTest()
    {
        yield return new WaitForSeconds(1);
        Debug.LogError("CoroutineTest");
    }

    public override void Freeze()
    {
        base.Freeze();
        Debug.LogError("Freeze");
    }

    public override void Redisplay()
    {
        base.Redisplay();
        Debug.LogError("Redisplay");
    }

    public override void Hiding()
    {
        base.Hiding();
        Debug.LogError("Hiding");
    }
    
    protected override void onUpdate()
    {
        base.onUpdate();
        Debug.LogError("onUpdate");
    }
}
