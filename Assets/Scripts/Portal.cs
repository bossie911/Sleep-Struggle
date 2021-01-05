using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public float portalHealth = 500;
    public AudioManager audioManager;

    public GameObject deathParticles;
    float lastHealth;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (portalHealth <= 0)
        {
            Instantiate(deathParticles, transform.position, Quaternion.identity);//starts the blood splatter animation
            audioManager.Play("portalBreak");
            Destroy(this.gameObject);
        }


        //Debug.Log(portalHealth);
        if (lastHealth > portalHealth)
        {
            audioManager.Play("portalDamage");
        }
        lastHealth = portalHealth;
    }
}
