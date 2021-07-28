using System;
using System.Threading.Tasks;
using libx;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


[DisallowMultipleComponent]
[RequireComponent(typeof(Image))]
public class UIImage : MonoBehaviour
{
    public Sprite sprite => m_Target.sprite;

    private Image m_Target;

    private Sprite m_LastLoadAsset; //最后一次加载的资产

    private string m_loadPath;
    
    private void Awake()
    {
        m_Target = this.GetComponent<Image>();
    }

    private void OnDestroy()
    {
        if (m_LastLoadAsset != null)
        {
            Assets.UnloadAsset(m_loadPath);
        }
        m_LastLoadAsset = null;
    }
    
    public void SetSpritePath(string loadPath)
    {
        m_loadPath = loadPath;
        //顺序是先加载新的资产，最后处理“如果有旧的资产，就卸载”的逻辑。这样可以避免有可能的卸载后立即再次加载，减少资源浪费
        var sprite = ResourcesMgr.Instance.LoadAsset<Sprite>(m_loadPath);
        m_Target.sprite = sprite;

        if(m_LastLoadAsset != null)
        {
            Assets.UnloadAsset(m_loadPath);
            m_LastLoadAsset = null;
        }

        m_LastLoadAsset = sprite;
    }
    
    public async Task SetSpritePathAsync(string loadPath)
    {
        m_loadPath = loadPath;
        //顺序是先加载新的资产，最后处理“如果有旧的资产，就卸载”的逻辑。这样可以避免有可能的卸载后立即再次加载，减少资源浪费
        //await Task.Delay(1);延时1秒执行
        var sprite = await ResourcesMgr.Instance.LoadAssetAsync<Sprite>(m_loadPath);
        m_Target.sprite = sprite;
        
        if (m_LastLoadAsset != null)
        {
            Assets.UnloadAsset(m_loadPath);
            m_LastLoadAsset = null;
        }
        m_LastLoadAsset = sprite;
    }
    
    public void SetSpritePathAsync(string loadPath, Action callback)
    {
        m_loadPath = loadPath;
        this.SetSpritePathAsync(loadPath)
            .ToObservable()
            .ObserveOnMainThread()
            .SubscribeOnMainThread()
            .Subscribe(_ =>
            {
                callback?.Invoke();
            }, () =>
            {
                callback?.Invoke();
            });
    }

    public void ClearLoadedSprite()
    {
        if (m_LastLoadAsset != null)
            Assets.UnloadAsset(m_loadPath);
        m_LastLoadAsset = null;
    }
}
