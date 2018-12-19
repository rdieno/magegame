using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraZoomLevel : MonoBehaviour
{
    [MenuItem("magegame/Zoom/100")]
    static void ZoomToPercent100()
    {
        Debug.Log("Changing camera zoom to 100%");
        Camera.main.orthographicSize = 5;
    }

    [MenuItem("magegame/Zoom/75")]
    static void ZoomToPercent75()
    {
        Debug.Log("Changing camera zoom to 75%");
        Camera.main.orthographicSize = 8;
    }

    [MenuItem("magegame/Zoom/50")]
    static void ZoomToPercent50()
    {
        Debug.Log("Changing camera zoom to 50%");
        Camera.main.orthographicSize = 10;
    }

    [MenuItem("magegame/Zoom/25")]
    static void ZoomToPercent25()
    {
        Debug.Log("Changing camera zoom to 25%");
        Camera.main.orthographicSize = 12;
    }

}
