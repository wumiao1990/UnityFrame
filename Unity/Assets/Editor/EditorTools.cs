using System.Collections;
using System.Collections.Generic;
using libx;
using UnityEditor;
using UnityEngine;

public class EditorTools
{
    [MenuItem("Tools/LuaC + 打资源包")]
    private static void DoLuac()
    {
        CI.DoLuac();
        MenuItems.BuildAssetBundles();
    }
}
