using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using libx;
using UnityEngine;

public class LoadPerfabOnShow : MonoBehaviour
{
    public string prefabUrl = "";
    public Canvas targetGb;

    public float m_DestroyTime = 0f;

    public bool isForceClose = false;
    public float m_StartTime = 0;

    public GameObject EffectGo = null;

    private SetRenderQueue setrq = null;


    void Awake()
    {
        setrq = this.GetComponent<SetRenderQueue>();
    }

    private void OnEnable()
    {
        StartLoad();
    }

    public void StartLoad()
    {
        if (string.IsNullOrEmpty(prefabUrl))
        {
            return;
        }
        
        if (EffectGo != null && !isForceClose)
        {
            EffectGo.SetActive(true);
            m_StartTime = 0;
            return;
        }
        
        if(EffectGo == null)
        {
            string[] efx = prefabUrl.Split('/');
            string preName = efx[efx.Length - 1];
            Transform t = this.gameObject.transform.Find(preName);
            if (!t)
            {
                CoroutineMgr.Instance.MyStartCoroutine(LoadPrefab());
                //loadPrefabTask();
            }
            else
            {
                EffectGo = t.gameObject;
                EffectGo.SetActive(true);
            }
        }
    }
    
    void OnDisable()
    {
        if (m_DestroyTime > 0)
        {
            if (!isForceClose && m_StartTime > 0 && m_StartTime < m_DestroyTime)
            {
                isForceClose = true;
            }
            m_StartTime = m_DestroyTime;
        }
        if (EffectGo)
        {
            EffectGo.SetActive(false);
        }
    }

    void Update()
    {
        if (m_DestroyTime > 0)
        {
            m_StartTime += Time.deltaTime;
            if (m_StartTime > m_DestroyTime)
            {
                if (EffectGo)
                {
                    EffectGo.SetActive(false);
                }
                m_StartTime = 0;
            }
        }
    }

    async void loadPrefabTask()
    {
        GameObject go = await ResourcesMgr.Instance.InstantiateTask(prefabUrl);
        if (EffectGo || !this)
        {
            Destroy(go);
            return;
        }
        SetGameObject(go);
    }
    
    IEnumerator LoadPrefab()
    {
        YieldArg yarg = new YieldArg(prefabUrl);
        
        CoroutineMgr.Instance.MyStartCoroutine(ResourcesMgr.Instance.InstantiateAsync(yarg));
        
        while (!yarg.isDone) { yield return null; }
        if (EffectGo || !this)
        {
            Destroy((GameObject)yarg.result);
            yield return null;
        }
        GameObject go = (GameObject)yarg.result;
        SetGameObject(go);
    }

    void SetGameObject(GameObject go)
    {
        EffectGo = go;
        if (EffectGo && this != null)
        {
            
            EffectGo.transform.parent = this.transform;
            EffectGo.transform.localPosition = Vector3.zero;
            EffectGo.transform.localScale = Vector3.one;
            if (setrq)
            {
                reEnableSetRenderQueue();
            }
            else
            {
                EffectDepth efd = EffectGo.GetComponent<EffectDepth>();
                if (!efd)
                {
                    efd = EffectGo.AddComponent<EffectDepth>();
                }
                if (targetGb)
                {
                    Canvas parentWidget = targetGb.GetComponent<Canvas>();
                    if (parentWidget)
                    {
                        efd.target = parentWidget;
                    }
                }
            }
        }
    }
    
    void reEnableSetRenderQueue()
    {
        setrq.enabled = false;
        setrq.enabled = true;
    }
}
