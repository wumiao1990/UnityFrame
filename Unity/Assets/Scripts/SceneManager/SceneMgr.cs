using System.Threading.Tasks;
using libx;
using SDGame;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class SceneMgr : UnitySingleton<SceneMgr>
{
    //同步加载场景
    public async void LoadScene(string sceneName)
    {
        string scenePath = ABPathUtilities.GetScenePath(sceneName);
        await SceneAsync(scenePath);
        UnLoadScene(scenePath);
    }
    
    //同步加载场景
    public void LoadSceneAsync(string sceneName)
    {
        string scenePath = ABPathUtilities.GetScenePath(sceneName);
        SceneAssetRequest sceneAssetRequest = Assets.LoadSceneAsync(scenePath, false);
        sceneAssetRequest.completed += (arq) =>
        {
            UnLoadScene(scenePath);
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
