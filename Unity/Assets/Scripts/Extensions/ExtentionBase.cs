using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using SUIFW;
using XLua;

[XLua.LuaCallCSharp]
public static class ExtentionBase
{
    public static bool IsNull(this System.Object obj)
    {
        return obj == null;
    }
    public static bool NotNull(this System.Object obj)
    {
        return obj != null;
    }
    public static bool IsNull(this UnityEngine.Object obj)
    {
        return obj == null;
    }
    public static bool NotNull(this UnityEngine.Object obj)
    {
        return obj != null;
    }
    public static bool IsNull(this UnityEngine.MonoBehaviour obj)
    {
        return obj == null;
    }
    public static bool NotNull(this UnityEngine.MonoBehaviour obj)
    {
        return obj != null;
    }
    public static bool IsEmpty<T>(this T[] str)
    {
        return str.IsNull() || str.Length == 0;
    }
    public static string _Log<T>(this T[] arrary)
    {
        if (arrary == null) return "null";
        else if (arrary.Length == 0) return "length = 0";
        var arraryDes = "arrary length = {0} \n".format(arrary.Length);
        for (int i = 0; i < arrary.Length; i++)
        {
            arraryDes = "{0} [{1}] = {2} \n"._Format(arraryDes, i, arrary[i]);
        }
        return arraryDes;
    }

    public static bool NotEmpty<T>(this T[] str)
    {
        return str.NotNull() && str.Length > 0;
    }

    public static bool IsEmpty<T>(this ICollection<T> str)
    {
        return str.IsNull() || str.Count == 0;
    }
    public static int GetCount<T>(this ICollection<T> str)
    {
        if (str.IsEmpty()) return 0;
        return str.Count;
    }

    public static bool NotEmpty<T>(this ICollection<T> str)
    {
        return str.NotNull() && str.Count > 0;
    }
    public static bool IsEmpty(this string str)
    {
        return str.IsNull() || str == "" || str == "nil" || str == "null";
    }

    public static bool NotEmpty(this string str)
    {
        return str.NotNull() && str != "" && str != "nil" && str != "null";
    }
    public static string[] _Split(this string str, string separator, StringSplitOptions opt = StringSplitOptions.None)
    {
        return str.Split(new string[] { separator }, opt);
    }

    public static string[] _SplitAdv(this string str, string separator, StringSplitOptions opt = StringSplitOptions.None)
    {
        if (str.Contains(separator))
            return str.Split(new string[] { separator }, opt);
        else
            return new string[] { str };
    }

    public static bool _Contains(this List<string> str, IEnumerable<string> strings)
    {
        foreach (var tem in strings)
        {
            if (!str.Contains(tem))
                return false;
        }
        return true;
    }

    public static bool _Contains(this string str, IEnumerable<string> strings)
    {
        foreach (var tem in strings)
        {
            if (str.Contains(tem))
                return true;
        }
        return false;
    }

    public static bool _EndsWith(this string str, IEnumerable<string> strings)
    {
        foreach (var tem in strings)
        {
            if (str.EndsWith(tem))
                return true;
        }
        return false;
    }

    public static void _CombinePrefix(this string[] strs, string prefix)
    {
        for (int i = 0; i < strs.Length; i++)
        {
            strs[i] = Path.Combine(prefix, strs[i]);
            //Debug.LogError(strs[i]);
        }
    }

    public static string GetPercent(this string str, float percent)
    {
        if (percent >= 1) return str;
        var length = (int)(str.Length * percent);
        var text = str.Substring(0, length);
        return text;
    }

    public static T GetRandom<T>(this List<T> list)
    {
        if (list.IsEmpty())
            return default(T);
        if (list.Count == 1)
        {
            return list[0];
        }
        var index = Random.Next(list.Count);
        //Debug.LogError("list GetRandom  {0}/{1}"._Format(index, list.Count));
        return list[index];

    }

    public static T GetAt<T>(this List<T> list, int index)
    {
        if (list.IsEmpty())
            return default(T);
        if (index >= 0 && index < list.Count)
        {
            return list[index];
        }
        return default(T);
    }

    public static T _GetAt<T>(this T[] list, int index)
    {
        if (list != null)
        {
            if (index >= 0 && index < list.Length)
            {
                return list[index];
            }
        }
        return default(T);
    }

    /// <summary>
    /// 获取最后一个对象
    /// </summary>
    public static T _GetLast<T>(this List<T> list)
    {
        if (list.NotEmpty())
        {
            return list[list.Count - 1];
        }
        return default(T);
    }
    /// <summary>
    /// 获取最后一个对象
    /// </summary>
    public static T _LastOrDefault<T>(this T[] list)
    {
        if (list.NotEmpty())
        {
            return list[list.Length - 1];
        }
        return default(T);
    }

    public static int _GetCount<T>(this List<T> list, Predicate<T> predicate)
    {
        int count = 0;
        if (list.NotEmpty())
        {
            list.ForEach((each) =>
            {
                if (predicate.Invoke(each))
                    count++;
            });
        }
        return count;
    }
    public static T GetAt<T>(this T[] list, int index)
    {
        if (index >= 0 && list != null && index < list.Length)
        {
            return list[index];
        }
        return default(T);
    }

    public static T GetUniqueRandom<T>(this List<T> list, T preItem)
    {
        if (list.IsEmpty())
            return default(T);
        if (list.Count == 1)
        {
            return list[0];
        }
        var preIndex = list.IndexOf(preItem);
        var index = preIndex;
        while (index == preIndex)
        {
            index = Random.Next(list.Count);
        }
        return list[index];
    }

    public static System.Random Random = new System.Random(Guid.NewGuid().GetHashCode());

    /// <summary>
    /// 将字符转换为字符数组字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string[] ToStringArray(this string str)
    {
        string[] strArray = null;
        if (str.NotEmpty())
        {
            strArray = new string[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                strArray[i] = str[i].ToString();
                //XiimoonLog.LogErrorFormat(strArray[i]);
            }
        }
        else
        {
            strArray = new string[0];
        }
        return strArray;
    }


    /// <summary>
    /// 将字符转换为字符数组字符串
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static byte[] ToBytes(this string str)
    {
        return Encoding.UTF8.GetBytes(str);
    }
    /// <summary>
    /// 将字符转换为字符数组字符串
    /// </summary>
    public static int ToInt(this string str)
    {
        try
        {
            return int.Parse(str);
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("int.Parse 转换失败, {0}", str);
        }
        return 0;
    }
    public static int[] ToInts
        (this string data, params char[] separator)
    {
        try
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }
            if (separator.IsEmpty())
            {
                separator = new char[] { ',' };
            }
            string[] strArray = data.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            int[] intArray = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; ++i)
            {
                intArray[i] = int.Parse(strArray[i]);
            }
            return intArray;
        }
        catch (Exception)
        {
            return new int[0];
        }
    }
    /// <summary>
    /// 将字符转换为字符数组字符串
    /// </summary>
    public static uint ToUInt(this string str)
    {
        try
        {
            return uint.Parse(str);
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("int.Parse 转换失败, {0}", str);
        }
        return 0;
    }

    public static List<int> _ToIntList(this short[] data)
    {
        var result = new List<int>();
        for (int i = 0; i < data.Length; i++)
        {
            result.Add(data[i]);
        }
        return result;
    }

    public static List<T> _ToList<T>(this T[] data)
    {
        if (data.IsEmpty()) return new List<T>();
        return new List<T>(data);
    }

    public static HashSet<T> _ToHash<T>(this T[] data)
    {
        if (data.IsEmpty()) return new HashSet<T>();
        return new HashSet<T>(data);
    }
    public static HashSet<int> _ToHashInt(this short[] data)
    {
        var hash = new HashSet<int>();
        if (data.NotEmpty())
        {
            foreach (var tem in data)
            {
                hash.Add(tem);
            }
        }
        return hash;
    }

    /// <summary>
    /// 字符串格式化
    /// </summary>
    public static string format(this string str, params object[] args)
    {
        return string.Format(str, args);
    }
    /// <summary>
    /// 字符串格式化
    /// </summary>
    public static string _Format(this string str, params object[] args)
    {
        return string.Format(str, args);
    }

    /// <summary>
    /// 获取文件名
    /// </summary>
    public static string _GetFileName(this string filePath)
    {
        var fileName = "";
        if (filePath.NotEmpty())
        {
            var index = filePath.LastIndexOf('/') + 1;
            fileName = filePath.Substring(index);
            index = fileName.IndexOf('.') + 1;
            if (index > 0)
            {
                fileName = fileName.Substring(0, index);
            }
        }
        return fileName;
    }

   

    public static List<FileInfo> _GetFiles(this DirectoryInfo dir)
    {
        var list = new List<FileInfo>();
        var dires = new List<DirectoryInfo>();
        dires.Add(dir);
        while (dires.NotEmpty())
        {
            list.AddRange(dires[0].GetFiles());
            dires.AddRange(dires[0].GetDirectories());
            dires.RemoveAt(0);
        }
        //Debug.LogError(list.Count);
        return list;
    }

   
    public static List<T> Clone<T>(this List<T> data)
    {
        return new List<T>(data);
    }

    static string colorformat = "<color={0}>{1}</color>";
    public static string ToColor(this string content, string color = "red")
    {
        return string.Format(colorformat, color, content);
    }

    public static string ToColor(this bool flag)
    {
        return string.Format(colorformat, flag ? "green" : "red", flag ? "YES" : "NO");
    }

    public static List<T> _ToList<T>(this HashSet<T> hash)
    {
        var list = new List<T>();
        foreach (var tem in hash)
        {
            list.Add(tem);
        }
        return list;
    }

    public static List<T> _ToList<T>(this LuaTable luaTable)
    {
        var resultList = new List<T>();
        var count = luaTable.Length + 1;
        for (var i = 1; i < count; i++)
        {
            resultList.Add(luaTable.Get<int, T>(i));
        }
        return resultList;
    }

    public static object[] _DoFunction(this LuaTable luaTable, string funcName, params object[] args)
    {
        if (luaTable != null)
        {
            var luaFuntion = luaTable.Get<LuaFunction>(funcName);
            if (luaFuntion != null)
            {
                var result = luaFuntion.Call(args);
                return result;
            }
            else
                Debug.LogErrorFormat("LuaFunction is null ,but call lua funtion  {0}.", funcName);
        }
        else
            Debug.LogErrorFormat("luaTable is null ,but call lua funtion  {0}.", funcName);
        return null;
    }

}
