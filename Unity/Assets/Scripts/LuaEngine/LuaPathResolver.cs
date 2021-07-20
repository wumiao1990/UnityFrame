using System.IO;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class NgameLuaPathResolver
{
    /*
    编辑器状态
        把Lua路径转为AssetPath
        AssetDataBase从指定路径读取lua源文件
    AB模式
        把Lua路径转为AB路径
        从AssetBundle中读取byte[]
        区分32位和64位机器不同的路径
     */

    //Regex flatten = new Regex(@"(?<=(.*/.*){1,})(/)");

    string LUA_LOCATION
    {
        get
        {
#if _AB_MODE_
            // if (UtilForMacro.Is64BitArchitecture())
            // {
            //     return "script/luas64/{0}";
            // }
            // else
            // {
            //     return "script/luas32/{0}";
            // }
            return "script/luas32_64/{0}";
#else
            return Application.dataPath + "/ABRes/Lua/{0}.lua";
#endif
        }
    }

    public byte[] ReadLua(string luaPath)
    {
        byte[] content = null;

#if _AB_MODE_
        //luaPath = flatten.Replace(luaPath, "+");

        var holder = AssetBundleManager.Instance;
        var asset = holder.LoadAsset<TextAsset>(holder.MonoObj, string.Format(LUA_LOCATION, luaPath), true);

        if (asset != null) content = asset.bytes;
#else
        var realPath = string.Format(LUA_LOCATION, luaPath);
        if (File.Exists(realPath))
        {
            content = File.ReadAllBytes(realPath);
        }
#endif
        return content;
    }
}