using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour {

    public float effectTime = 5f;
    public float amount = 10f;

    void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.increaseSpeed(amount, effectTime);
            Destroy(gameObject);
        }
    }
}
