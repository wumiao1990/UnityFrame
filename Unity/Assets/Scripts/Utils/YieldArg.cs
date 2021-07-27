using UnityEngine;
using System.Collections;

public class YieldArg
{
	public object[] args;
	
	public string fn {
		get {
			return (string)arg;
		}
		set{
			arg = value;
		}
	}
	
	public object arg
	{
		get
		{
			if (args.Length > 0)
				return args[0];
			return null;
		}
		set
		{
			if (args == null || args.Length == 0)
				args = new object[1];
			args[0] = value;
		}
	}
	
	//返回值
	public object[] results;
	
	public float normalizedProgress{
		get{
			return Mathf.Clamp01(progress / maxProgress);
		}
	}
	
	public float progress;
	public float maxProgress = 1f;
	
	public T resultTo<T>() {
		return (T)result;
	}
	
	public bool isDone;
	
	public object result
	{
		get
		{
			if (results != null && results.Length > 0)
				return results[0];
			return null;
		}
		set
		{
			if (results == null || results.Length == 0)
				results = new object[1];
			results[0] = value;
		}
	}
	
	public YieldArg() { }
	public YieldArg(params object[] args) {
		this.args = args;
	}
}

public delegate IEnumerator YieldFunc(YieldArg arg);
