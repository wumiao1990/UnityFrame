using System;
using System.Collections;
using SUIFW;
using UnityEngine;

public class UILoadingFrom : BaseUIForm
{    
    public Action OnEventCallBack;
    public AnimationEventController animEvent;
    
    public override void OnReady()
    {
        base.OnReady();
        //窗体性质
        CurrentUIType.UIForms_Type = UIFormType.Normal;
    }

    private float animTime = 0;
    public override void Display()
    {
        base.Display();
        Animator anim = gameObject.GetComponent<Animator>();
        animEvent = gameObject.GetComponent<AnimationEventController>();
        animTime = anim.GetCurrentAnimatorStateInfo(0).length;
        
        animEvent.ClearAnimationClipEvent();
        animEvent.AddAnimationClipEvent((tag) =>
        {
            if (OnEventCallBack != null)
            {
                OnEventCallBack.Invoke();
                OnEventCallBack = null;
            }
        });
    }

    public void ClosePanel()
    {
        CoroutineMgr.Instance.StartCoroutine(CloseAnimation(animTime));
    }
    
    IEnumerator CloseAnimation(float closeDelaytime)
    {
        yield return  new WaitForSeconds(closeDelaytime);

        CloseUIForm();
    }
}