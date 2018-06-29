using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haste : MonoBehaviour {

    public float effectTime = 5f;
    public float amount = 10f;

    void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.multiplySpeed(amount, effectTime);
            pc.multiplyFireRate(amount, effectTime);
            Destroy(gameObject);
        }
    }
}
