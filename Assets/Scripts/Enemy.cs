using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyPosition; 

    public float enemyDamage = 10f;

    public float currentHealth;
    public float maxHealth
    { 
        get { return currentHealth; }
        set { currentHealth = value; }    
    }

    public float getHealed = 5f; 

    void Start()
    {
        maxHealth = 100f;
    }

    void Update()
    {
        //Enemy beweegt naar de tile die op de beste weg ligt naar de player base. 
        //Hiervoor stel ik de enemy position gelijk aan de position van de desbetreffende tile

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

    }

    public void HealEnemy()
    {
        currentHealth += getHealed;
    }

    public void EnemyDied()
    {
        Enemy.Destroy(this);
    }
}
