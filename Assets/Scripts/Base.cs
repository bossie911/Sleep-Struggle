using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float enemyDamage = 0.1f;

    //Deze functie checked of de base collide met een enemy en de enemy doet dan damage

    void OnCollisionStay2D (Collision2D col)
    {
        if (col.gameObject.tag.Equals("enemy"))
        {
            HPBarBase.baseHP -= enemyDamage;
        }
    }
}
