using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileUtility
{
    public static void DeleteDirectory(string dirPath, bool isRecursive, bool forceDelete = true)
    {
        if (forceDelete) {
            RemoveFileAttributes (dirPath, isRecursive);
        }
        //Directory.Delete(dirPath, isRecursive);
        DeleteFilesAndFoldersRecursively(dirPath, isRecursive);
    }
    
    public static void RemoveFileAttributes(string dirPath, bool isRecursive) {
        foreach (var file in Directory.GetFiles (dirPath)) {
            File.SetAttributes (file, FileAttributes.Normal);
        }
        if(isRecursive) {
            foreach (var dir in Directory.GetDirectories (dirPath)) {
                RemoveFileAttributes (dir, isRecursive);
            }
        }
    }
    
    public static void DeleteFilesAndFoldersRecursively(string dir, bool isRecursive)
    {
        foreach (string file in Directory.GetFiles(dir))
        {
            File.Delete(file);
        }

        if (isRecursive)
        {
            foreach (string subDir in Directory.GetDirectories(dir))
            {
                DeleteFilesAndFoldersRecursively(subDir, isRecursive);
            }
        }
            
        //延迟10ms执行删除文件夹操作，如果不延迟，容易出现打开explorer或者之前操作过对应的文件夹，
        //导致IO异常，所以延迟10ms， 猜测是因为延迟windows释放文件句柄
        System.Threading.Thread.Sleep(10); 
        Directory.Delete(dir);
    }
}
