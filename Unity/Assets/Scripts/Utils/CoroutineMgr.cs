using UnityEngine;
using System;
using System.Collections;


public class CoroutineMgr : MonoSingleton<CoroutineMgr>
{
    public Coroutine MyStartCoroutine(IEnumerator co)
    {
        return StartCoroutine(co);
    }

    public void MyStopCoroutine(Coroutine co)
    {
        StopCoroutine(co);
    }

    public Coroutine DelayInvoke(Action method, float delayTime)
    {
        return MyStartCoroutine(DelayCoroutine(method, delayTime));
    }

    private IEnumerator DelayCoroutine(Action method, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        if (method != null)
        {
            method();
        }
    }

    //LuaCall
    public void YieldAndCallback(object to_yield, Action callback)
    {
        StartCoroutine(CoBody(to_yield, callback));
    }

    private IEnumerator CoBody(object to_yield, Action callback)
    {
        if (to_yield is IEnumerator)
            yield return StartCoroutine((IEnumerator)to_yield);
        else
            yield return to_yield;
        callback();
    }
    public void LuaInvokeRepeating(string methodName, float time, float repeatRate)
    {
         InvokeRepeating( methodName,  time,  repeatRate);
    }
    public void LuaCancelInvoke()
    {
        CancelInvoke();
    }
}
