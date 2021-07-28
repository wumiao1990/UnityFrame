using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EditorTools
{
    [MenuItem("XLua/LuaC")]
    private static void DoLuac()
    {
        CI.DoLuac();
    }
}
