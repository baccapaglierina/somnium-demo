using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinimapFollow : MonoBehaviour
{
    public Transform player;
    public Transform container;

    void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }

    public void ResetRotation()
    {
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
    }
    public void ResetPosition()
    {
        container.transform.position = new Vector3(0,0,0);
    }
}
