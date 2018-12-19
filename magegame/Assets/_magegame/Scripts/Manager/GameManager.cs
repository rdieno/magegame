using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DrawArrow.ForDebug(Vector3.zero, Vector3.right, Color.red);
        DrawArrow.ForDebug(Vector3.zero, Vector3.up, Color.green);
        DrawArrow.ForDebug(Vector3.zero, Vector3.forward, Color.blue);
    }

    private void OnDrawGizmos()
    {
        DrawArrow.ForGizmo(Vector3.zero, Vector3.right, Color.red);
        DrawArrow.ForGizmo(Vector3.zero, Vector3.up, Color.green);
        DrawArrow.ForGizmo(Vector3.zero, Vector3.forward, Color.blue);
    }
}
