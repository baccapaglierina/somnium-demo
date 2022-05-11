using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PanoramCapture : MonoBehaviour
{
    public Transform handHoverPoint;
    private int snapCounter = 0;
    public bool stereoscopic = false;
    public Camera targetCamera;
    public RenderTexture cubeMapLeft;
    public RenderTexture cubeMapRight;
    public RenderTexture equirectRT;  
    public Material mat;
    public SteamVR_Action_Boolean snapPhoto;
    public SteamVR_Input_Sources handType;

    private void Start()
    {
        snapPhoto.AddOnStateDownListener(TriggerDown, handType);
        snapPhoto.AddOnStateDownListener(TriggerUp, handType);
    }
    public void TriggerDown(SteamVR_Action_Boolean action, SteamVR_Input_Sources sources)
    {
        Capture();
    }
    public void TriggerUp(SteamVR_Action_Boolean action, SteamVR_Input_Sources sources)
    {
        PanoramicSphere();
    }
    //capture panorama
    public void Capture()
    {
        if(!stereoscopic)
        {
            targetCamera.RenderToCubemap(cubeMapLeft);
            cubeMapLeft.ConvertToEquirect(equirectRT);
        }
        else
        {
            targetCamera.stereoSeparation = 0.065f;
            targetCamera.RenderToCubemap(cubeMapLeft, 63, Camera.MonoOrStereoscopicEye.Left);
            targetCamera.RenderToCubemap(cubeMapRight, 63, Camera.MonoOrStereoscopicEye.Right);
            cubeMapLeft.ConvertToEquirect(equirectRT, Camera.MonoOrStereoscopicEye.Left);
            cubeMapRight.ConvertToEquirect(equirectRT, Camera.MonoOrStereoscopicEye.Right);
        }
        Save(equirectRT);
        Debug.Log("CLICK! Foto scattata!");
    }
    //convert photo to texture
    public void Save(RenderTexture rt)
    {
        Texture2D tex = new Texture2D(rt.width, rt.height);
        RenderTexture.active = rt;
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        RenderTexture.active = null;
        byte[] bytes = tex.EncodeToJPG();
        string temp_name = "/Panorama" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
        string path = Application.dataPath + temp_name;
        System.IO.File.WriteAllBytes(path, bytes);
        tex.LoadImage(bytes);
        Debug.Log("File saved in: " + path);
        mat.mainTexture = tex;  //qui fa riferimento ai mat nei miei asset
        snapCounter++;
    }   
    //create sphere with photo as texture material
    public void PanoramicSphere()
    {
        GameObject spherePic = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        spherePic.name = "Pic_" + snapCounter;
        spherePic.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        spherePic.AddComponent<Interactable>();
        spherePic.AddComponent<Throwable>();
        spherePic.GetComponent<Rigidbody>().useGravity = false;
        spherePic.GetComponent<MeshRenderer>().material = mat;  //qui assegno il materiale
        InvertSphere(spherePic);
        spherePic.transform.position = handHoverPoint.transform.position;
        Debug.Log("Pallina nr: " + snapCounter + " apparsa in coord " + transform.position);
    }
    //reflect inner outside so it looks better if you look it by inside
    public void InvertSphere(GameObject ob)
    {
        Vector3[] normals = ob.GetComponent<MeshFilter>().mesh.normals;
        for (int i = 0; i < normals.Length; i++)
        {
            normals[i] = -normals[i];
        }
        ob.GetComponent<MeshFilter>().sharedMesh.normals = normals;

        int[] triangles = ob.GetComponent<MeshFilter>().sharedMesh.triangles;
        for (int i = 0; i < triangles.Length; i += 3)
        {
            int t = triangles[i];
            triangles[i] = triangles[i + 2];
            triangles[i + 2] = t;
        }
        ob.GetComponent<MeshFilter>().sharedMesh.triangles = triangles;
        Debug.Log("Success revert of sphere nr " + snapCounter);
    }
}
