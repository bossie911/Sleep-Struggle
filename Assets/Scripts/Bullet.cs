using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float bulletSpeed = 10f;

    public float bulletDamage = 10f;

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = bulletSpeed * Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame)
        {
            TargetHit();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);       
    }

    void TargetHit()
    {
        //Debug.Log("HIT");

        Destroy(gameObject);

        target.GetComponent<Enemy>().currentHealth -= bulletDamage;

        //Debug.Log(target.GetComponent<Enemy>().currentHealth);

    }

    //functie wordt aangeroepen in de turret script om de target van de turret naar de script te krijgen
    public void Find(Transform _target)
    {
        target = _target;
    }
}
