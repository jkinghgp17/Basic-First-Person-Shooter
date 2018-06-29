using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpUp : MonoBehaviour {
    
    public float effectTime = 5f;
    public float amount = 3f;

    void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            pc.increaseJump(amount, effectTime);
            Destroy(gameObject);
        }
    }
}
