using System;
using System.Collections;
using System.Threading.Tasks;
using libx;
using SDGame;
using SUIFW;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class SceneMgr : UnitySingleton<SceneMgr>
{
    private Action onLoaderCallback;
    private SceneAssetRequest loadingAsyncOperation;

    public void Load(string sceneName) {
        
        onLoaderCallback = () =>
        {
            CoroutineMgr.Instance.MyStartCoroutine(LoadSceneAsync(sceneName));
        };

        // Load the loading scene
        UILoadingFrom uiLobbyLoadingFrom = UIManager.GetInstance().ShowUIForms(ProConst.UI_LOADINGFROM) as UILoadingFrom;
        uiLobbyLoadingFrom.OnEventCallBack = () =>
        {
            LoadSceneAsync("LoadingScene", () =>{});    
        };
    }
    
    private IEnumerator LoadSceneAsync(string sceneName) {
        yield return null;

        string scenePath = ABPathUtilities.GetScenePath(sceneName);
        loadingAsyncOperation = Assets.LoadSceneAsync(scenePath, false);

        while (!loadingAsyncOperation.isDone) {
            yield return null;
        }
    }
    
    public float GetLoadingProgress() {
        if (loadingAsyncOperation != null) {
            return loadingAsyncOperation.progress;
        } else {
            return 1f;
        }
    }

    public void LoaderCallback() {
        // Triggered after the first Update which lets the screen refresh
        // Execute the loader callback action which will load the target scene
        if (onLoaderCallback != null) {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
    
    
    //同步加载场景
    public async void LoadScene(string sceneName)
    {
        string scenePath = ABPathUtilities.GetScenePath(sceneName);
        SceneAssetRequest sceneAssetRequest = await SceneAsync(scenePath);
    }
    
    //异步加载场景
    public void LoadSceneAsync(string sceneName, Action callback = null)
    {
        string scenePath = ABPathUtilities.GetScenePath(sceneName);
        SceneAssetRequest sceneAssetRequest = Assets.LoadSceneAsync(scenePath, false);
        sceneAssetRequest.completed += (arq) =>
        {
            if (callback != null)
            {
                callback();
                callback = null;
            }
        };
    }

    /// <summary>
    /// 加载场景，path需要是全路径
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private Task<SceneAssetRequest> SceneAsync(string path)
    {
        TaskCompletionSource<SceneAssetRequest> tcs = new TaskCompletionSource<SceneAssetRequest>();
        SceneAssetRequest sceneAssetRequest = Assets.LoadSceneAsync(path, false);
        sceneAssetRequest.completed = (arq) => { tcs.SetResult(arq as SceneAssetRequest); };
        return tcs.Task;
    }

    /// <summary>
    /// 卸载场景，path需要是全路径
    /// </summary>
    /// <param name="path"></param>
    private void UnLoadScene(string path)
    {
        Assets.UnloadScene(path);
    }
}
