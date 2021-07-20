using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;
using XLua;

public static class UIPathHelper
{
	public const string UI_PATH = "UIMain/Panels/{0}";
	private const string LUA_PATH = "UIMain/{0}";
	
	private static LuaTable m_luaTable;
	private static Func<string, string> m_getPathFunc;

	private static readonly string LUAPATH = "UIPathConfig";
	
	public static void Init()
	{
		if (m_luaTable != null)
			m_luaTable.Dispose();
		m_luaTable = null;

		m_luaTable = NgameLua.Load(LUAPATH);
		if (m_luaTable == null)
		{
			Debug.LogErrorFormat("Lua file not exist : {0}", LUAPATH);
			return;
		}

		m_getPathFunc = m_luaTable.GetInPath<Func<string, string>>("UIPathConfig.GetPath");
	}

	
	/// <summary>
	/// 获取lua的路径配置，配置在UIPathConfig.lua中，
	/// 如果没有配置，或者初始化失败，那么就使用默认的LUA_PATH的路径
	/// </summary>
	/// <param name="panelName"></param>
	/// <returns></returns>
	public static string GetLuaPath(string panelName)
	{
		string path = null;
		if (m_getPathFunc != null)
		{
			path = m_getPathFunc.Invoke(panelName);
		}

		if (string.IsNullOrEmpty(path))
		{
			path = string.Format(LUA_PATH, panelName);
		}
		
		return path;
	}
}
