using UnityEngine;
using System.Collections;
//[ExecuteInEditMode]
public class SetRenderQueue : MonoBehaviour 
{
    public int mRendererQueue;
	Renderer[] mRd;
	void Start()
	{
        if (Application.isPlaying)
        {
            mRd = GetComponentsInChildren<Renderer>(true);
            UpdateRenderQ();
        }
    }

    // Update is called once per frame
    void OnEnable() 
	{
        if (Application.isPlaying)
        {
            if (mRd == null || mRd.Length == 0)
            {
                mRd = GetComponentsInChildren<Renderer>(true);
                UpdateRenderQ();
            }
        }
       
    }

    void  UpdateRenderQ()
    {
		if(mRd != null && mRd.Length>0)
		{
            for (int i = 0, count = mRd.Length; i < count; i++)
            {
                Renderer tmpR = mRd[i];
                if (tmpR != null && tmpR.sharedMaterials != null && tmpR.sharedMaterials.Length > 0)
                {
                    Material[] matList = tmpR.sharedMaterials;
                    for (int j = 0, num = matList.Length; j < num; j++)
                    {
                        Material tmpM = matList[j];
                        if (tmpM != null)
                        {
                            tmpM.renderQueue = tmpM.shader.renderQueue + mRendererQueue;
                        }
                        
                        //Debug.Log("Material : "tmpM.name + "renderQueue=" +tmpM.renderQueue);
                    }
                }
            }
		}
    }

#if UNITY_EDITOR
    void OnDestroy()
    {
        if (!Application.isPlaying)
        {
            return;
        }
        if (mRd != null && mRd.Length > 0)
        {
            for (int i = 0, count = mRd.Length; i < count; i++)
            {
                Renderer tmpR = mRd[i];
                if (tmpR != null && tmpR.sharedMaterials != null && tmpR.sharedMaterials.Length > 0)
                {
                    Material[] matList = tmpR.sharedMaterials;
                    for (int j = 0, num = matList.Length; j < num; j++)
                    {
                        Material tmpM = matList[j];
                        if (tmpM != null)
                        {
                            tmpM.renderQueue = tmpM.shader.renderQueue - mRendererQueue;
                        }
                        
                        //Debug.Log("Material : "tmpM.name + "renderQueue=" +tmpM.renderQueue);
                    }
                }
            }
        }
    }
#endif
}