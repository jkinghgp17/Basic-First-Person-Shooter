using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    public float speed = 1000f;
    public int damage = 10;

    public float lifeTime;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.transform.name);

        

        PlayerHealth pc = other.gameObject.GetComponent<PlayerHealth>();
        if (pc != null)
        {
            pc.takeDamage(damage);
        }

        Destroy(gameObject);
    }

    void Update()
    {
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
        lifeTime -= Time.deltaTime;
    }
}
