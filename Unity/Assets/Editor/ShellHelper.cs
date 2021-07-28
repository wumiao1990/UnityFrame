using UnityEngine;
using System.Collections;
using System.Diagnostics;
using UnityEditor;
using System.Collections.Generic;

using Debug = UnityEngine.Debug;

public class ShellHelper
{

    public class ShellRequest
    {
        public event System.Action<int, string> onLog;
        public event System.Action onError;
        public event System.Action onDone;

        public event System.Action onEdge;

        public void Log(int type, string log)
        {
            if (onLog != null)
            {
                onLog(type, log);
            }
            if (type == 1)
            {
                UnityEngine.Debug.LogError(log);
            }
        }

        public void NotifyDone()
        {
            if (onDone != null)
            {
                onDone();
            }
        }

        public void TriggerEdge()
        {
            if (onEdge != null)
            {
                onEdge();
            }
        }

        public void Error()
        {
            if (onError != null)
            {
                onError();
            }
        }
    }


    private static string shellApp
    {
        get
        {
#if UNITY_EDITOR_WIN
            string app = "cmd.exe";
#elif UNITY_EDITOR_OSX
            string app = "bash";
#endif
            return app;
        }
    }


    private static List<System.Action> _queue = new List<System.Action>();


    static ShellHelper()
    {
        _queue = new List<System.Action>();
        EditorApplication.update += OnUpdate;
    }
    private static void OnUpdate()
    {
        lock (_queue)
        {
            for (int i = 0; i < _queue.Count; i++)
            {
                try
                {
                    var action = _queue[i];
                    if (action != null)
                    {
                        action();
                    }
                }
                catch (System.Exception e)
                {
                    UnityEngine.Debug.LogException(e);
                }
            }
            _queue.Clear();
        }
    }

    // threading part complete

    private static System.Threading.ManualResetEvent threadSignal;
    private static int numberOfTasks = 0;
    //static List<System.Threading.EventWaitHandle> threadFinishEvents = new List<System.Threading.EventWaitHandle>();

    /*
    public static void ClearWaits()
        {
            foreach (var wait in threadFinishEvents)
            {
                wait.Set();
            }

            threadFinishEvents.Clear();
        }
     */


    public static void CreateWait()
    {
        threadSignal = new System.Threading.ManualResetEvent(false);
        numberOfTasks = 0;
    }

    public static bool Wait()
    {
        //System.Threading.WaitHandle.WaitAll(threadFinishEvents.ToArray(), 5 * 60 * 1000);
        return threadSignal.WaitOne(5 * 60 * 1000);
    }

    public static ShellRequest ProcessCommand(string cmd, string workDirectory, List<string> environmentVars = null)
    {
        ShellRequest req = new ShellRequest();

        System.Threading.Interlocked.Increment(ref numberOfTasks);

        //var threadFinish = new System.Threading.ManualResetEvent(false);
        //threadFinishEvents.Add(threadFinish);

        System.Threading.ThreadPool.QueueUserWorkItem(delegate (object state)
        {
            Process p = null;
            try
            {
                ProcessStartInfo start = CreateProcessStartInfo(cmd, workDirectory, environmentVars);

                p = Process.Start(start);
                p.ErrorDataReceived += delegate (object sender, DataReceivedEventArgs e)
                {
                    UnityEngine.Debug.LogError(e.Data);
                };
                p.OutputDataReceived += delegate (object sender, DataReceivedEventArgs e)
                {
                    UnityEngine.Debug.LogError(e.Data);
                };
                p.Exited += delegate (object sender, System.EventArgs e)
                {
                    UnityEngine.Debug.LogError(e.ToString());
                };

                bool hasError = false;
                do
                {
                    string line = p.StandardOutput.ReadLine();
                    if (line == null)
                    {
                        break;
                    }
                    line = line.Replace("\\", "/");

                    lock (_queue)
                    {
                        _queue.Add(delegate ()
                        {
                            req.Log(0, line);
                        });
                    }
                } while (true);

                while (true)
                {
                    string error = p.StandardError.ReadLine();
                    if (string.IsNullOrEmpty(error))
                    {
                        break;
                    }
                    hasError = true;

                    lock (_queue)
                    {
                        _queue.Add(delegate ()
                        {
                            req.Log(1, error);
                        });
                    }
                }
                p.Close();

                lock (_queue)
                {
                    if (hasError)
                    {
                        _queue.Add(delegate ()
                        {
                            req.Error();
                        });
                    }
                    else
                    {
                        _queue.Add(delegate ()
                        {
                            req.NotifyDone();
                        });

                        //直接在线程中触发事件，这个callback中不能用序列化
                        req.TriggerEdge();
                    }
                }
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogException(e);
                if (p != null)
                {
                    p.Close();
                }
            }
            finally
            {
                if (System.Threading.Interlocked.Decrement(ref numberOfTasks) == 0)
                {
                    threadSignal.Set();
                }
            }
        });
        return req;
    }

    private static ProcessStartInfo CreateProcessStartInfo(string cmd, string workDirectory, List<string> environmentVars){
        ProcessStartInfo start = new ProcessStartInfo(shellApp);
#if UNITY_EDITOR_OSX
        string splitChar = ":";
        start.Arguments = "-c";
#elif UNITY_EDITOR_WIN
        string splitChar = ";";
        start.Arguments = "/c";
#endif
        if (environmentVars != null)
        {
            foreach (string var in environmentVars)
            {
                start.EnvironmentVariables["PATH"] += (splitChar + var);
            }
        }

        start.Arguments += (" \"" + cmd + " \"");
        start.CreateNoWindow = true;
        start.ErrorDialog = true;
        start.UseShellExecute = false;
        start.WorkingDirectory = workDirectory;

        if (start.UseShellExecute)
        {
            start.RedirectStandardOutput = false;
            start.RedirectStandardError = false;
            start.RedirectStandardInput = false;
        }
        else
        {
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.RedirectStandardInput = true;

#if UNITY_EDITOR_OSX
            start.StandardOutputEncoding = System.Text.UTF8Encoding.UTF8;
            start.StandardErrorEncoding = System.Text.UTF8Encoding.UTF8;
#elif UNITY_EDITOR_WIN
            start.StandardOutputEncoding = System.Text.Encoding.GetEncoding("GBK");
            start.StandardErrorEncoding = System.Text.Encoding.GetEncoding("GBK");
#endif
        }
        return start;
    }

    public static void ProcessCommand_SingleThread(string cmd, string workDirectory = "", List<string> environmentVars = null)
    {
        Process p = null;
        try {
            ProcessStartInfo start = CreateProcessStartInfo(cmd, workDirectory, environmentVars);
            p = Process.Start(start);
            p.ErrorDataReceived += (sender, e) => {
                UnityEngine.Debug.LogError("--<ErrorDataReceived>--" + e.Data);
            };
            p.OutputDataReceived += (sender, e) => {
                UnityEngine.Debug.Log("--<OutputDataReceived>--" + e.Data);
            };
            p.Exited += (sender, e) => {
                UnityEngine.Debug.Log("--<Exited>--" + e.ToString());
            };

            while (true) {
                string line = p.StandardOutput.ReadLine();
                if (string.IsNullOrEmpty(line)) {
                    break;
                }
                line = line.Replace("\\", "/");
                UnityEngine.Debug.Log("--[log]--" + line);
            }
            while (true) {
                string error = p.StandardError.ReadLine();
                if (string.IsNullOrEmpty(error)) {
                    break;
                }
                UnityEngine.Debug.LogError("--[error]--" + error);
            }
        } catch (System.Exception e) {
            UnityEngine.Debug.LogError("------ProcessCommand exception !!!!------");
            UnityEngine.Debug.LogException(e);
        } finally {
            if (p != null) {
                p.Close();
            }
        }
    }

    private List<string> _enviroumentVars = new List<string>();

    public void AddEnvironmentVars(params string[] vars)
    {
        for (int i = 0; i < vars.Length; i++)
        {
            if (vars[i] == null)
            {
                continue;
            }
            if (string.IsNullOrEmpty(vars[i].Trim()))
            {
                continue;
            }
            _enviroumentVars.Add(vars[i]);
        }
    }

    public ShellRequest ProcessCMD(string cmd, string workDir)
    {
        return ShellHelper.ProcessCommand(cmd, workDir, _enviroumentVars);
    }

}
