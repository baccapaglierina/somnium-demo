using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapMov : MonoBehaviour
{
    private Vector3 pos;

    //change position camera
    public void Right()
    {
        pos.x += 1;
        transform.position = pos;
        Debug.Log("pos:" + pos);
    }
    public void Left()
    {
        pos.x -= 1;
        transform.position = pos;
        Debug.Log("pos:" + pos);
    }
    public void Up()
    {
        pos.z -= 1;
        transform.position = pos;
        Debug.Log("pos:" + pos);
    }
    public void Down()
    {
        pos.z += 1;
        transform.position = pos;
        Debug.Log("pos:" + pos);
    }

    public void PosReset()
    {
        pos.x = 0;
        pos.z = 0;
        transform.position = pos;
        Debug.Log("pos:" + pos);
    }
}
