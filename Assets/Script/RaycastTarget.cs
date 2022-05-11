using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RaycastTarget : MonoBehaviour
{
    private GameObject objHit;
    public GameObject arrow;
    private GameObject childObjHit;

    void FixedUpdate()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if(hit.collider.CompareTag("target"))
            { 
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green, 1000);
                objHit = hit.transform.gameObject;
                childObjHit = objHit.transform.GetChild(2).gameObject; //taking sphere as reference
                Debug.Log("Target Position: " + objHit.transform.position);
                Debug.Log("Target child name: " + childObjHit);  
                arrow.SetActive(true);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 8, Color.blue, 1000);
        }
        if (arrow.activeInHierarchy)
        {
            Debug.Log("Child position: " + childObjHit.transform.position);
            arrow.transform.LookAt(childObjHit.transform.position);
        }   
    }
}