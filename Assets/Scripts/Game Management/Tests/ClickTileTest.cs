using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClickTileTest : MonoBehaviour
{
    TileManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GetComponent<TileManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log(manager.GetTileFromPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition)).CanPlaceTower());
        }
    }
}
