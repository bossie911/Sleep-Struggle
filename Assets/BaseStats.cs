using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStats : MonoBehaviour
{
    public float Health = 100f;

    public string targetTag = "enemy";

    private void Start()
    {
        
    }

    private void Update()
    {

        Debug.Log(Health);

        if (Health <= 1)
        {
            
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("enemy"))
            {
                Health -= 1;
            }
        }
}
