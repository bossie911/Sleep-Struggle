using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceableSelector : MonoBehaviour
{
    public GameObject Factory = new GameObject();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Input.mousePosition;
            Instantiate(Factory, mousePos, Quaternion.identity);
        }
    }
}
