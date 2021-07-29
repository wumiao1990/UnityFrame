using System.Collections;
using System.Threading.Tasks;
using libx;
using UnityEngine;
using XLua;

public class OnGameObjectDestroy : MonoBehaviour
{
    public string resName;
    public bool isInvalid = false;
    void OnDestroy()
    {
        if (false == isInvalid)
        {
            Assets.UnloadAsset(resName);
        }
    }
}

[LuaCallCSharp]
public class ResourcesMgr : MonoSingleton<ResourcesMgr>
{
    //异步时每次等待时间
    public float _corouWaitingTime = 0.05f;
    private bool yieldLoading;
    
    #region Assets
    /// <summary>
    /// 加载资源，path需要是全路径
    /// </summary>
    /// <param name="path"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T LoadAsset<T>(string path) where T : UnityEngine.Object
    {
        AssetRequest assetRequest = Assets.LoadAsset(path, typeof (T));
        return (T) assetRequest.asset;
    }
    
    /// <summary>
    /// 异步加载资源，path需要是全路径
    /// </summary>
    /// <param name="path"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public Task<T> LoadAssetAsync<T>(string path) where T : UnityEngine.Object
    {
        TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
        AssetRequest assetRequest = Assets.LoadAssetAsync(path, typeof (T));
            
        //如果已经加载完成则直接返回结果（适用于编辑器模式下的异步写法和重复加载）
        if (assetRequest.isDone)
        {
            tcs.SetResult((T) assetRequest.asset);
            return tcs.Task;
        }

        //+=委托链，否则会导致前面完成委托被覆盖
        assetRequest.completed += (arq) => { tcs.SetResult((T) arq.asset); };
        return tcs.Task;
    }
    
    //---------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 实列化物体
    /// </summary>
    /// <param name="resName">名字</param>
    /// <returns></returns>
    public GameObject Instantiate(string resPathName)
    {
        GameObject go = LoadAsset<GameObject>(resPathName);
        GameObject goObjClone = Instantiate(go);
        if (goObjClone == null)
        {
            Debug.LogError(GetType() + "/LoadAsset()/克隆资源不成功，请检查。 path=" + resPathName);
        }
        goObjClone.AddComponent<OnGameObjectDestroy>().resName = resPathName;
        return goObjClone;
    }
    
    //---------------------------------------------------------------------------------------------------------
    /// <summary>
    /// 异步实列化物体
    /// </summary>
    /// <param name="resName">名字</param>
    /// <returns></returns>
    public IEnumerator InstantiateAsync(YieldArg yarg)
    {
        while (yieldLoading)
            yield return null;
        yieldLoading = true;
        
        yarg.progress = 1f;
        yarg.maxProgress = 1.1f;
        AssetRequest assetRequest = Assets.LoadAssetAsync(yarg.fn, typeof (GameObject));
        while (!assetRequest.isDone)
        {
            yarg.progress += assetRequest.progress * 0.1f;
            yield return null;
        }
        
        GameObject goObjClone = Instantiate((GameObject)assetRequest.asset);
        if (goObjClone == null)
        {
            Debug.LogError(GetType() + "/InstantiateAsync()/克隆资源不成功，请检查。 path=" + yarg.fn);
        }
        goObjClone.AddComponent<OnGameObjectDestroy>().resName = yarg.fn;
        yarg.result = goObjClone;
        yarg.isDone = true;
        
        yieldLoading = false;
    }
    
    /// <summary>
    /// 异步实列化物体,使用Task
    /// </summary>
    /// <param name="yarg"></param>
    /// <returns></returns>
    public async Task<GameObject> InstantiateTask(string resName)
    {
        GameObject go = await LoadAssetAsync<GameObject>(resName);
        GameObject goObjClone = Instantiate(go);
        if (goObjClone == null)
        {
            Debug.LogError(GetType() + "/InstantiateAsync()/克隆资源不成功，请检查。 path=" + resName);
        }
        goObjClone.AddComponent<OnGameObjectDestroy>().resName = resName;
        return goObjClone;
    }
    
    /// <summary>
    /// 加载特效
    /// </summary>
    /// <param name="go">特效父节点</param>
    /// <param name="Url">Path</param>
    /// <param name="destroyTime">加载后多久删除</param>
    public void EffectPerfabOnLoad(GameObject go, string effectPath, float destroyTime = 0)
    {
        effectPath = ABPathUtilities.GetEffectPath(effectPath);
        LoadPerfabOnShow lpos = go.GetComponent<LoadPerfabOnShow>();
        if (!lpos)
        {
            lpos = go.AddComponent<LoadPerfabOnShow>();
            lpos.EffectGo = null;
            lpos.prefabUrl = effectPath;
            lpos.StartLoad();
        }
        lpos.m_DestroyTime = destroyTime;
        lpos.isForceClose = false;
        lpos.m_StartTime = 0;
        
    }

    #endregion
    
}//Class_end
