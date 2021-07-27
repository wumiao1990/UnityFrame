using UnityEngine;
using System.Collections;

/// <summary>
/// 挂载到粒子特效上，并且指定的界面上显示
/// </summary>
public class EffectDepth : MonoBehaviour
{

    //指定的UI
    public Canvas target;

    //记录渲染深度
    private int oldRQ;

	void Start () {
	
	}
	
	void Update () {
        AdjustRQ();	
	}

    public void AdjustRQ()
    {
        if (target == null) return;

        if (target.renderOrder == oldRQ)
        {
            return;
        }

        Renderer[] rends = transform.GetComponentsInChildren<Renderer>(true);
        if (rends == null) return;

        for (int i = 0; i < rends.Length; ++i)
        {
            //在指定的UI上设置效果深度
            if (rends[i].material != null)
                rends[i].material.renderQueue = target.renderOrder + 1;
        }

        oldRQ = target.renderOrder;

    }
}
