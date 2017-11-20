using Assets.Scripts;
using System.IO;
using UnityEngine;

public abstract class Utils
{
    public static float GetAngle2Point(Vector3 dest,Vector3 pos)
    {
        Vector3 dir = dest - pos;
        dir.Normalize();
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
    }

    public static Vector3 GetMousePos2D()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mouse);
    }

    public static float GetScreenWidth()
    {
        float screen_ratio = (float)Screen.width / (float)Screen.height;
        return Camera.main.orthographicSize * screen_ratio;
    }
}