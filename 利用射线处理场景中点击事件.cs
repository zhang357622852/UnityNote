// 场景相机
public Camera SceneCamera;

private void Update()
{
	if (Input.GetMouseButtonUp(0))
	{
       // 获取鼠标当前位置 屏幕坐标(左下角原点)
        Vector3 curPos = Input.mousePosition;

        // 如果当前ui相机发生碰撞了优先处理UI碰撞
        //UICamera是NGUI的UI相机脚本
        if (UICamera.Raycast(curPos))
        {
        	return;
        }


        // 执行碰撞
    	DoRayCast(curPos);
	}
}

private void DoRayCast(Vector3 position)
{
    // 检查屏幕边界
    if (Game.IsOutOfScreen(position))
        return;

    // 发出射线
    Ray ray = SceneCamera.ScreenPointToRay(position);
    RaycastHit hit;

    // 执行碰撞检测
    if (!Physics.Raycast(ray, out hit, SceneCamera.farClipPlane))
        return;

    // 获取碰撞到的GameObject
    GameObject gameObject = hit.transform.gameObject;

    //TO DO: 这样就可以获取gameobject上的脚本，并且调用相关处理函数 
}

//是否超出屏幕
public static bool IsOutOfScreen(Vector3 point)
{
    // 检查边界
    if (point.x < 0f || point.x > Screen.width ||
        point.y < 0f || point.y > Screen.height)
        return true;

    // 没有超出屏幕
    return false;
}