using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadDamge : MonoBehaviour {

    public float effectTime = 5f;
    public float amount = 3f;

    void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.multiplyDamage(amount, effectTime);
            Destroy(gameObject);
        }
    }
}
