using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System;

public class LuaEnvManager : MonoBehaviour
{
    public bool showLog = false;
    public Action updateLua;
    public LuaFunction towerFunction;
    
    private static LuaEnvManager _Instanse;
    public static LuaEnvManager Instanse
    {
        get
        {
            if (_Instanse == null)
            {
                GameObject go = new GameObject("LuaEnvManager");
                _Instanse = go.AddComponent<LuaEnvManager>();
                DontDestroyOnLoad(go);
            }
            return _Instanse;
        }
    }
    // Use this for initialization
    public void InitGlobalLuaFunc()
    {
        //towerFunction = NgameLua._uiEnv.Global.Get<LuaFunction>("LogonUIForm");
        //var result = LuaEnvManager._.towerFunction.Call(new object[] { instanceID }, new Type[] { typeof(bool) });
        
        luaPlayerData = NgameLua._uiEnv.Global.Get<LuaTable>("playerData");
    }
    // Update is called once per frame
    void Update()
    {
        if (updateLua != null)
            updateLua();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            NgameLua.DoString("util.back()");
        }
    }
    LuaTable luaPlayerData;
    public void WritePlayerData(string key, string value)
    {
        if (luaPlayerData != null)
        {
            luaPlayerData.Set(key, value);
        }
    }

    private void OnDestroy()
    {
        Clean();
    }

    public static object[] Call(string func, params object[] args)
    {
        LuaFunction f = NgameLua._uiEnv.Global.GetInPath<XLua.LuaFunction>(func);
        object[] rs = f.Call(args);
        f.Dispose();
        return rs;
    }
    
    /// <summary>
    /// 清理lua的引用
    /// </summary>
    public void Clean()
    {
        updateLua = null;
        towerFunction.Dispose();
        towerFunction = null;
    }
}
