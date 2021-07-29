using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class CI
{
    public static void DoLuac()
    {
        try
        {
            var sourceDir = Application.dataPath + "/ABRes/Lua";
            var dstDir = Application.dataPath + "/ABRes/PlayGround/script";
            // var dstDir32 = dstDir + "/luas32";
            // var dstDir64 = dstDir + "/luas64";
            var dstDir32_64 = dstDir + "/luas32_64";

            var projectBase = Directory.GetParent(Application.dataPath).ToString();

#if UNITY_EDITOR_WIN
            // string luac32Build = projectBase + "/Tools/lua-5.3.4_Win32/luac53.exe";
            // string luac64Build = projectBase + "/Tools/lua-5.3.4_Win64/luac53.exe";
            string luac32_64Build = projectBase + "/Tools/lua-5.3.5_Win32_64/luac.exe";
#elif UNITY_EDITOR_OSX
            // string luac32Build = projectBase + "/Tools/lua-5.3.4_darwin32/luac";
            // string luac64Build = projectBase + "/Tools/lua-5.3.4_darwin64/luac";
            string luac32_64Build = projectBase + "/Tools/lua-5.3.5_darwin32_64/luac";
#endif


            // if (Directory.Exists(dstDir32))
            // {
            //     FileUtility.DeleteDirectory(dstDir32, true);
            // }

            // if (Directory.Exists(dstDir64))
            // {
            //     FileUtility.DeleteDirectory(dstDir64, true);
            // }

            if (Directory.Exists(dstDir32_64))
            {
                FileUtility.DeleteDirectory(dstDir32_64, true);
            }

            ShellHelper.CreateWait();
            try
            {
#if UNITY_EDITOR_OSX
                // ShellHelper.ProcessCommand(string.Format("chmod 777 {0} ", luac32Build), "");
                // ShellHelper.ProcessCommand(string.Format("chmod 777 {0} ", luac64Build), "");
                ShellHelper.ProcessCommand(string.Format("chmod 777 {0} ", luac32_64Build), "");
#endif

                var luaFiles = Directory.GetFiles(sourceDir, "*.lua", SearchOption.AllDirectories);
                foreach (var eachLua in luaFiles)
                {
                    //lua file name cannot contain +
                    if (eachLua.Contains("+"))
                    {
                        throw new ArgumentException(string.Format("Lua File Name cannot contain '+' [{0}].", eachLua));
                    }

                    var flatten = eachLua.Substring(sourceDir.Length);

                    // var targetPath32 = dstDir32 + flatten;
                    // {
                    //     var dstParentDir = Directory.GetParent(targetPath32);
                    //     if (!dstParentDir.Exists)
                    //     {
                    //         Directory.CreateDirectory(dstParentDir.ToString());
                    //     }
                    // }

                    // var targetPath64 = dstDir64 + flatten;
                    // {
                    //     var dstParentDir = Directory.GetParent(targetPath64);
                    //     if (!dstParentDir.Exists)
                    //     {
                    //         Directory.CreateDirectory(dstParentDir.ToString());
                    //     }
                    // }

                    var targetPath32_64 = dstDir32_64 + flatten;
                    {
                        var dstParentDir = Directory.GetParent(targetPath32_64);
                        if (!dstParentDir.Exists)
                        {
                            Directory.CreateDirectory(dstParentDir.ToString());
                        }
                    }

                    // {
                    //     ShellHelper.ShellRequest req = ShellHelper.ProcessCommand(string.Format("{0} -o {1} {2}", luac32Build, targetPath32, eachLua), "");
                    //     req.onLog += delegate (int arg1, string arg2)
                    //     {
                    //         if (arg1 > 0)
                    //         {
                    //             XiimoonLog.LogErrorFormat(arg2);

                    //             throw new Exception("Luac Error.");
                    //         }
                    //     };
                    //     req.onEdge += delegate ()
                    //     {
                    //         File.Move(targetPath32, targetPath32.Replace(".lua", ".txt"));
                    //     };
                    // }

                    // {
                    //     ShellHelper.ShellRequest req = ShellHelper.ProcessCommand(string.Format("{0} -o {1} {2}", luac64Build, targetPath64, eachLua), "");
                    //     req.onLog += delegate (int arg1, string arg2)
                    //     {
                    //         if (arg1 > 0)
                    //         {
                    //             XiimoonLog.LogErrorFormat(arg2);

                    //             throw new Exception("Luac Error.");
                    //         }
                    //     };

                    //     req.onEdge += delegate ()
                    //     {
                    //         File.Move(targetPath64, targetPath64.Replace(".lua", ".txt"));
                    //     };
                    // }

                    {
                        ShellHelper.ShellRequest req = ShellHelper.ProcessCommand(string.Format("{0} -o {1} {2}", luac32_64Build, targetPath32_64, eachLua), "");
                        req.onLog += delegate (int arg1, string arg2)
                        {
                            if (arg1 > 0)
                            {
                                Debug.LogErrorFormat(arg2);

                                throw new Exception("Luac Error.");
                            }
                        };

                        req.onEdge += delegate ()
                        {
                            File.Move(targetPath32_64, targetPath32_64.Replace(".lua", ".bytes"));
                        };
                    }
                }
            }
            finally
            {
                if (!ShellHelper.Wait())
                {
                    Debug.LogErrorFormat("DoLuac 超时!");
                }
            }

            UnityEditor.AssetDatabase.Refresh();
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat(e.ToString());
            throw new Exception(e.Message);
        }
    }
}
