using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoomController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float zoomLerpSpeed;

    private Vector3 dragOrigin;


    private float targetZoom;
    private float zoomFactor = 3f;

    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    private void Update()
    {
        float scrollData;
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        targetZoom -= scrollData * zoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 4f, 9f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);

        //PanCamera();
    }

    private void PanCamera()
    {

        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            print(" origin " + dragOrigin + " newPosition " + cam.ScreenToWorldPoint(Input.mousePosition) + " = difference " + difference);

            cam.transform.position += difference;

        }
    }
}