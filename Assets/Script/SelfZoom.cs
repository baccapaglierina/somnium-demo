using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelfZoom : MonoBehaviour
{
    Camera maincamera;
    [SerializeField]
    private float zoomAMT;
    void Start()
    {
        maincamera = GetComponent<Camera>();
        zoomAMT = maincamera.orthographicSize;
    }

    void Update()
    {
        maincamera.orthographicSize = zoomAMT;
    }
    //change zoom camera
    public void ZoomIN()
    {
        zoomAMT -= 1;
    }
    public void ZoomOUT()
    {
        zoomAMT  += 1;
    }
}
