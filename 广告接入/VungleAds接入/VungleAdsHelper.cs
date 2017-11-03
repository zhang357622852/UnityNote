using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
public class VungleAdsHelper : MonoBehaviour {

    private static VungleAdsHelper instance = null;

    private void Awake()
    {
        if (instance)
        {
            //GGDebug.LogError("Constructor twice!!!");
        }
        instance = this;
    }

    public static VungleAdsHelper GetInstance()
    {
        return instance;
    }

    string iOSAppID = "5979de8cb165b5fd71002b5d";
	string androidAppID = "5979d4dccc9b98e371002757";
	string windowsAppID = "";

#if UNITY_IPHONE
	Dictionary<string, bool> placements = new Dictionary<string, bool>
	{
		{ "VIDEOAS88133", false },
		{ "DEFAULT60665", false }
	};
#elif UNITY_ANDROID
	Dictionary<string, bool> placements = new Dictionary<string, bool>
	{
		{ "VIDEOOO85502", false },
		{ "DEFAULT49950", false }
	};
#elif UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
	Dictionary<string, bool> placements = new Dictionary<string, bool>
	{
		{ "", false },
		{ "", false }
	};
#endif

	List<string> placementIdList;

	bool adInited = false;


	void Start () {
		//DebugLog("Initializing the Vungle SDK");
		
		placementIdList = new List<string>(placements.Keys);

		string[] array = new string[placements.Keys.Count];
		placements.Keys.CopyTo(array, 0);
		string appID;

#if UNITY_IPHONE
		appID = iOSAppID;
#elif UNITY_ANDROID
		appID = androidAppID;
#elif UNITY_WSA_10_0 || UNITY_WINRT_8_1 || UNITY_METRO
		appID = windowsAppID
#endif

		Vungle.init(appID, array);
		initializeEventHandlers();
	}

	// Called when the player pauses
	void OnApplicationPause(bool pauseStatus) {
		if (pauseStatus) {
			Vungle.onPause ();
		} else {
			Vungle.onResume ();
		}
	}

    public void playVideo() {
		Vungle.playAd(placementIdList[0]);
	}

    public void loadVideo() {
		//Video需要手动调用读取，可以在每局游戏开始前调用
		Debug.Log("[VungleAdsHelper] loadVideo");
		Vungle.loadAd(placementIdList[0]);
	}

    public void playRewardVideo() {
		Vungle.playAd(placementIdList[1]);
	}

	public void loadRewardVideo() {
		//RewardVideo在后台设置了自动读取，此方法可以不需要调用
		Debug.Log("[VungleAdsHelper] loadRewardVideo");
		Vungle.loadAd(placementIdList[1]);
	}

	public bool IsVideoReady() {
		// return placements[placementIdList[0]];
		return Vungle.isAdvertAvailable(placementIdList[0]);
	}

    public bool IsRewardVideoReady() {
		// return placements[placementIdList[1]];
		return Vungle.isAdvertAvailable(placementIdList[1]);
	}

	/* Setup EventHandlers for all available Vungle events */
	void initializeEventHandlers() {

		//Event triggered during when an ad is about to be played
		Vungle.onAdStartedEvent += (placementID) => {
			DebugLog ("Ad " + placementID + " is starting!  Pause your game  animation or sound here.");
		};

		//Event is triggered when a Vungle ad finished and provides the entire information about this event
		//These can be used to determine how much of the video the user viewed, if they skipped the ad early, etc.
		Vungle.onAdFinishedEvent += (placementID, args) => {
			DebugLog ("Ad finished - placementID " + placementID + ", was call to action clicked:" + args.WasCallToActionClicked +  ", is completed view:" 
				+ args.IsCompletedView);

            //*********************************做广告完成逻辑**********************************************//
            //判断 placementID是video还是rewardVideo进行逻辑处理
            
            if (placementID.Equals(placementIdList[0]))
            {
                GameManage.GetInstance().GotoStartup();
            }
            else if (placementID.Equals(placementIdList[1]))
            {
                GameManage.GetInstance().RelifeGame();
            }
            Debug.Log("==================是哪一个广告=========================" + placementID);
		};

		//Event is triggered when the ad's playable state has been changed
		//It can be used to enable certain functionality only accessible when ad plays are available
		Vungle.adPlayableEvent += (placementID, adPlayable) => {
			DebugLog ("Ad's playable state has been changed! placementID " + placementID + ". Now: " + adPlayable);
			placements[placementID] = adPlayable;
		};

		//Fired log event from sdk
		Vungle.onLogEvent += (log) => {
			DebugLog ("Log: " + log);
		};

		//Fired initialize event from sdk
		Vungle.onInitializeEvent += () => {
			adInited = true;
			DebugLog ("SDK initialized");
		};

	}

	/* Common method for ensuring logging messages have the same format */
	void DebugLog(string message) {
		Debug.Log("[VungleAdsHelper] " + System.DateTime.Today +": " + message);
	}
}
#endif
