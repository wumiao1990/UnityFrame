using System.IO;
using libx;
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
            if (Assets.runtimeMode)
            {
                return "Assets/ABRes/PlayGround/script/luas32_64/{0}.txt";    
            }
            return Application.dataPath + "/ABRes/Lua/{0}.lua";
        }
    }

    public byte[] ReadLua(string luaPath)
    {
        byte[] content = null;
        
        if (Assets.runtimeMode)
        {
            //luaPath = flatten.Replace(luaPath, "+");

            var holder = ResourcesMgr.Instance;
            var asset = holder.LoadAsset<TextAsset>(string.Format(LUA_LOCATION, luaPath));

            if (asset != null)
            {
                content = asset.bytes;
            }
            return content;
        }
        
        var realPath = string.Format(LUA_LOCATION, luaPath);
        if (File.Exists(realPath))
        {
            content = File.ReadAllBytes(realPath);
        }

        return content;
    }
}