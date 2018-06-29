using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashDamage : MonoBehaviour {

    public int damage = 3;
    public float knockBackPower = 2f;
    public float knockBackTime = 0.1f;

	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerHealth ph = other.GetComponent<PlayerHealth>();
            ph.takeDamage(damage);

            Vector3 direction = Vector3.Lerp(transform.position, other.transform.position, 0.5f).normalized;

            PlayerController pc = other.GetComponent<PlayerController>();
            pc.addKnockBack(knockBackPower, direction, knockBackTime);
        }
    }
}
