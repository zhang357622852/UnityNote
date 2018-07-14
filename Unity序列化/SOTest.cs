using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//添加了这个可以在Project中右键Create中创建这个资源
[CreateAssetMenu(menuName = "TestSo")]
public class SOTest : ScriptableObject
{
    public int hehe = 666;
    public MyClass mClass;
    public MyEnum mEnum = MyEnum.Enum1;
    public MyStruct mStruct;

    [System.Serializable]
    public class MyClass
    {
        public int xixi = 777;
        //[SerializeField]就可以序列化了,[HideInInspector]在属性面板隐藏
        [SerializeField][HideInInspector]private int xixiP = 7777;
    }

    //枚举无需添加[System.Serializable]就可以序列化
    public enum MyEnum
    {
        Enum1 = 1,
        Enum2,
        Enum3,
    }

    [System.Serializable]
    public struct MyStruct
    {
        public int pipi;
        //加上[SerializeField]就可以序列化了
        [SerializeField]private int pipiP;
    }

    private void OnDestroy()
    {
        Debug.Log("====OnDestroy=====");
    }

    private void OnEnable()
    {
        Debug.Log("====OnEnable=====");
    }

    private void OnDisable()
    {
        Debug.Log("====OnDisable=====");
    }

}
