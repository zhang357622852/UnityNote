///
///

public abstract class Script
{
	public abstract object Excute(params object[] args);
}

public class SCRIPT_2018 : Script
{
	public override object Excute(params object[] args)
	{
		//To Do:
	}
}


public static class ScriptMgr
{
    // 脚本列表
    private static Dictionary<int, Script> mScriptsDic = new Dictionary<int, Script>();

    public static void Init()
    {
        // 收集Script
        Assembly asse = Assembly.GetExecutingAssembly();
        Type[] types = asse.GetTypes();
        foreach (Type t in types)
        {
        	//是否是派生类
            if (t.IsSubclassOf(typeof(Script)))
            {
                string strSptNo = t.Name.Replace("SCRIPT_", "");
                int sptNo;
                if (! int.TryParse(strSptNo, out sptNo))
                {
                    Debug.Log("脚本名字 {0} 非法，必须类似 SCRIPT_1234", t.Name);
                    continue;
                }

                // 已经有该公式名，需要移除
                if (mScriptsDic.ContainsKey(sptNo))
                    mScriptsDic.Remove(sptNo);

                mScriptsDic.Add(sptNo, asse.CreateInstance(t.Name) as Script);
            }
        }
    }

    public static object Excute(int scriptNo, params object[] args)
    {
        Script script;

        if (! mScriptsDic.TryGetValue(scriptNo, out script))
        {
            Debug.Log("调用脚本{0}不存在。", scriptNo);
            return null;
        }

        object ret = script.Excute(args);
        return ret;
    }
}
