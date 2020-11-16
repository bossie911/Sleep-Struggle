using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float enemyDamage = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionStay2D (Collision2D col)
    {
        if (col.gameObject.tag.Equals("enemy"))
        {
            HPBarBase.baseHP -= enemyDamage;
        }
    }
}
