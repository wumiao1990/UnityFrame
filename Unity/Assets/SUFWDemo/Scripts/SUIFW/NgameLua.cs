//==============================================================
//  Copyright (C) 2017 
//  Create by ChengBo at 2017/6/23 10:28:41.
//  Version 1.0
//  Administrator 
//==============================================================
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public class NgameLua
{
    private static NgameLuaPathResolver pathResolver = new NgameLuaPathResolver();
    public static LuaEnv _uiEnv = null;
    private static Dictionary<string, byte[]> _luaFiles = new Dictionary<string, byte[]>();

    /// <summary>
    /// 全局环境配置lua，不要清理
    /// </summary>
    private static LuaTable m_gameEnv = null;

    public static void Clear()
    {
        _luaFiles.Clear();
    }

    public static void FullLuaGC()
    {
        Debug.LogFormat("lua full gc");
        if (_uiEnv != null)
        {
            _uiEnv.FullGc();
            _uiEnv.Tick();
        }
    }

    public static void FullGC()
    {
        if (_uiEnv != null)
        {
            _uiEnv.Tick();
            _uiEnv.FullGc();
        }

    }

    /// <summary>
    /// 清理一些lua相关的功能，例如hotfix，还有一些静态引用的部分
    /// </summary>
    public static void CleanSomethingForLua()
    {
        Clear();
    }
    
    /// <summary>
    /// 清理lua env
    /// </summary>
    public static void CleanLuaEnv()
    {
        Debug.LogFormat("CleanLuaEnv");

        Clear();
        if (_uiEnv != null)
        {
            _uiEnv.Dispose();
        }
        _uiEnv = null;
    }

    static NgameLua()
    {
        LazyInit();
    }

    private static void LazyInit()
    {
        if (_uiEnv == null)
        {
            _uiEnv = new LuaEnv();
            _uiEnv.AddLoader(NgameLuaLoader);
            _luaFiles.Clear();

            Debug.LogFormat("lua env LazyInit");
        }
    }

    public static LuaTable Load(string luaFile)
    {
        LazyInit();

        byte[] content = null;
        LuaTable table = null;

        if (_luaFiles.ContainsKey(luaFile))
        {
            content = _luaFiles[luaFile];
        }
        else
        {
            content = NgameLuaLoader(ref luaFile);
            if (content != null) _luaFiles[luaFile] = content;
        }

        if (content != null)
        {
            table = _uiEnv.NewTable();

            LuaTable meta = _uiEnv.NewTable();
            meta.Set("__index", _uiEnv.Global);
            table.SetMetaTable(meta);
            meta.Dispose();

            table.Set("self", table);
            _uiEnv.Global.Set(luaFile, table);
            _uiEnv.DoString(content, luaFile, table);
        }
        return table;
    }

    public static LuaTable Require(string luaFile)
    {
        LazyInit();

        byte[] content = null;

        if (_luaFiles.ContainsKey(luaFile))
        {
            content = _luaFiles[luaFile];
        }
        else
        {
            content = NgameLuaLoader(ref luaFile);
            if (content != null) _luaFiles[luaFile] = content;
        }

        if (content != null)
        {
            return _uiEnv.DoString(content, luaFile, null)[0] as LuaTable;
        }
        return null;
    }

    public static LuaTable Require(LuaEnv env, string luaFile)
    {
        byte[] content = null;
        if (_luaFiles.ContainsKey(luaFile))
        {
            content = _luaFiles[luaFile];
        }
        else
        {
            content = NgameLuaLoader(ref luaFile);
            if (content != null) _luaFiles[luaFile] = content;
        }

        if (content != null)
        {
            return env.DoString(content, luaFile, null)[0] as LuaTable;
        }
        return null;
    }

    public static object[] DoFileString(string luaFile)
    {
        LazyInit();

        byte[] content = null;

        if (_luaFiles.ContainsKey(luaFile))
        {
            content = _luaFiles[luaFile];
        }
        else
        {
            content = NgameLuaLoader(ref luaFile);
            if (content != null) _luaFiles[luaFile] = content;
        }

        if (content != null)
        {
            return _uiEnv.DoString(content, luaFile);
        }
        else
        {
            Debug.LogErrorFormat("DoFileString failed - content is nil, {0}", luaFile);
        }
        return null;
    }
    public static byte[] ReadFile(string luaFile)
    {
        return NgameLuaLoader(ref luaFile);
    }
    public static object[] DoString(string content)
    {
        LazyInit();

        return _uiEnv.DoString(content);
    }

    public static byte[] NgameLuaLoader(ref string luaFile)
    {
        return pathResolver.ReadLua(luaFile);
    }

    public static void Hotfix()
    {
#if HOTFIX_ENABLE
        DoFileString("Hotfix/fix");
#endif
    }

    public static int GetLuaMemory()
    {
        if (_uiEnv != null)
        {
            return _uiEnv.Memroy;
        }

        return 0;
    }

    private static float s_timeInterval = 2;
    private static float s_timeElapsed = 0;

    public static void Upadte(float fTime)
    {
        s_timeElapsed += fTime;
        if (s_timeElapsed >= s_timeInterval)
        {
            s_timeElapsed -= s_timeInterval;

            if (_uiEnv != null)
            {
                _uiEnv.Tick();
            }
        }
    }
}
