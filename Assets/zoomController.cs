using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomController : MonoBehaviour
{
    private Camera cam;
    private float targetZoom; 
    private float zoomFactor = 3f;
    [SerializeField] private float zoomLerpSpeed; 

    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize; 
    }

    void Update()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 4f, 9f); 
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed); 

    }
}
