using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{   
    public static float enemyDamage = 0.1f;

    public float currentHealth;

    public GameObject bloodSpat;
    public float startHealth;

    

    public float maxHealth
    { 
        get { return currentHealth; }
        set { currentHealth = value; }    
    }

    public float getHealed = 5f;

    public GameObject turret;

    public void Start()
    {
        maxHealth = startHealth;
        currentHealth = maxHealth;
    }

    public void Update()
    {
        //Enemy scant de tiles naast hem voor turrets. als er een turret naast hem staat, val deze aan. 
        //als er niet een naast hem staat, heal voor 5 hp en move dichterbij de player base

        //Als currentHealth 0 is, is de enemy dood en wordt hij verwijderd
        if(currentHealth <= 0)
        {
            EnemyDied();
        }

    }

    public void DealDamage()
    {
        //Scan tiles om enemy heen voor turrets. als er een turret 1 tile naast de enemy staat, doe damage hierop per 5 seconden.
    }

    public void HealEnemy()
    {
        currentHealth += getHealed;
    }

    public void EnemyDied()
    {
        Instantiate(bloodSpat, transform);
        Destroy(this.gameObject);
    }
}
