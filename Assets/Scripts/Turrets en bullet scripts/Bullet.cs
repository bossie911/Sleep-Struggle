using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;
    public float bulletSpeed = 10f;
    public float bulletDamage = 10f;
    public Color buffColor;

    void Update()
    {
        NoTargetDestroy();

        Vector3 direction = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        //De TargetHit funcite wordt uitgevoerd als de lengte van de dir vector gelijk is als de afstand die hij heeft afgelegt deze frame
        if(direction.magnitude <= distanceThisFrame)
        {
            TargetHit();
            return;
        }

        //movement
        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    //Destroyed de bullet als hij geen target meer heeft
    void NoTargetDestroy()
    {
        if (target == null)
        {
            Destroy(this.gameObject);
        }
    }

    //Code voor damage op enemies en destroyed de bullet gameobject
    void TargetHit()
    {
        if (target.gameObject.name == "Portal")
        {
            target.GetComponent<Portal>().portalHealth -= bulletDamage;
        }
        else if (target.gameObject.name == "Enemy 1(Clone)")
        {         
            target.GetComponent<Enemy>().currentHealth -= bulletDamage;
        }
        else if(target.gameObject.name == "Enemy 2(Clone)")
        {
            target.GetComponent<Mosquito>().currentHealth -= bulletDamage;
        }
        else if (target.gameObject.name == "Enemy 3(Clone)")
        {
            target.GetComponent<Nightmare>().currentHealth -= bulletDamage;
        }

        Destroy(this.gameObject);
    }

    //functie wordt aangeroepen in de turret script om de target van de turret naar deze script te krijgen
    public void Find(Transform _target)
    {
        target = _target;
    }

    //Functie wordt aangeroepen in de turret script om te kijken of de turret gebuffed is
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
