using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public float portalHealth = 500;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (portalHealth <= 0)
        {
            Destroy(this.gameObject);
        }


        Debug.Log(portalHealth);
    }
}
