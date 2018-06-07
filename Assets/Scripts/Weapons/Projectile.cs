using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float speed = 1000f;
    public float damage = 10f;
    public float impactForce = 10f;

    public float lifeTime;

    public GameObject expolsion;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
	}
	
	void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.transform.name);

        // Don't hit the player
        if (other.gameObject.tag == "Player")
            return;

        if (expolsion != null)
        {
            GameObject expolsiontemp = Instantiate(expolsion, transform.position, transform.rotation);
            Destroy(expolsiontemp, 0.1f);
        }

        Target target = other.gameObject.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage(damage);
        }

        Rigidbody otherRB = other.gameObject.GetComponent<Rigidbody>();
        if (otherRB != null)
        {
            otherRB.AddForce(-transform.forward * impactForce);
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
