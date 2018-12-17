/// <summary>
/// StartSceneState.cs
/// Created by WinMi 2018/10/23
///  开始场景,游戏从这个场景开始
/// </summary>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class StartSceneState : ISceneState
{
    private const string SCENE_NAME = "StartScene";

    public StartSceneState(SceneStateController controller) : base(SCENE_NAME, controller)
    {
    }

    public override void Enter()
    {
        NIDebug.Log("=进入场景=:" + SceneName);

        Coroutine.DispatchService(DoInit());
    }

    private IEnumerator DoInit()
    {
        NIDebug.Log("开始游戏初始化。");

        CsvFileMgr.InitStartCsv();

        yield return Coroutine.DispatchService(ResourceMgr.Instance.LoadEtcText());

        // 用线程去初始化相应的模块
        Thread thread = new Thread(new ParameterizedThreadStart(DoInitModule));

        thread.Start();

        while (thread.IsAlive)
        {
            // 等待线程加载结束
            yield return null;
        }

        NIDebug.Log("完成游戏初始化");

        // StartWnd
        UIMgr.Instance.ShowForms<StartWnd>(StartWnd.FormsName);
    }

    private void DoInitModule(object para)
    {
        //// 载入Etc下的配置表
        //initList.Add(ResourceMgr.Instance.LoadEtcText());

        //initList.Add(ResourceMgr.Instance.Init());

        try
        {
            // 怪物模块初始化
            MonsterMgr.Instance.Init();

            // 战斗模块初始化
            CombatRootMgr.Instance.Init();
        }
        catch (Exception e)
        {
            NIDebug.LogException(e);
        }
    }

    public override void Exit()
    {
        NIDebug.Log("=退出场景=:" + SceneName);
    }
}
