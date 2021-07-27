using System;
using System.Threading.Tasks;
using UnityEngine.UI;
using XLua;

[LuaCallCSharp]
public static class UImageExtensions
{
    public static void SetSpritePath(this Image image, string loadPath)
    {
        loadPath = ABPathUtilities.GetTexturePath(loadPath);
        var xImage = _GetXImageOrCreate(ref image);
        xImage.SetSpritePath(loadPath);
    }

    public static Task SetSpritePathAsync(this Image image, string loadPath)
    {
        loadPath = ABPathUtilities.GetTexturePath(loadPath);
        var xImage = _GetXImageOrCreate(ref image);
        return xImage.SetSpritePathAsync(loadPath);
    }

    public static void SetSpritePathAsync(this Image image, string loadPath, Action callback)
    {
        loadPath = ABPathUtilities.GetTexturePath(loadPath);
        var xImage = _GetXImageOrCreate(ref image);
        xImage.SetSpritePathAsync(loadPath, callback);
    }

    public static void ClearLoadedSprite(this Image image)
    {
        var xImage = image.GetComponent<UIImage>();
        if (xImage == null)
            return;
        xImage.ClearLoadedSprite();
    }


    private static UIImage _GetXImageOrCreate(ref Image image)
    {
        var ximg = image.GetComponent<UIImage>();
        if(ximg == null)
        {
            ximg = image.gameObject.AddComponent<UIImage>();
        }
        return ximg;
    }

}

