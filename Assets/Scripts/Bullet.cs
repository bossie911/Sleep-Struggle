using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float bulletSpeed = 10f;

    public float bulletDamage = 10f;

    public Color buffColor;

    // Update is called once per frame
    void Update()
    {
        NoTargetDestroy();


        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        //De TargetHit funcite wordt uitgevoerd als de lengte van de dir vector gelijk is als de afstand die hij heeft afgelegt deze frame
        if(direction.magnitude <= distanceThisFrame)
        {
            TargetHit();
            //return;
        }

        //movement
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    //Destroyed de bullet als hij geen target meer heeft
    void NoTargetDestroy()
    {
        if (target == null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
    }

    //Code die wordt uitgevoerd als de bullet collide met een enemy
    void TargetHit()
    {
        if (target.gameObject.name == "Portal")
        {
            target.GetComponent<Portal>().portalHealth -= bulletDamage;
        }
        else /*if (target.gameObject.name == "Enemy 1(Clone)")*/
        {         
            target.GetComponent<Enemy>().currentHealth -= bulletDamage;
        }

        Destroy(this.gameObject);
    }

    //functie wordt aangeroepen in de turret script om de target van de turret naar deze script te krijgen
    public void Find(Transform _target)
    {
        target = _target;
    }

    public void BulletDamage(float _bulletDamage, bool isBuffed)
    {
        bulletDamage = _bulletDamage;

        if(isBuffed)
        {
            BuffParticles();
        }
    }

    public void BuffParticles()
    {
        GetComponent<ParticleSystem>().startColor = buffColor;
    }
}
