using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour {

    public float jumpAmount = 5f;

	void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();

            pc.jumpPad(jumpAmount);
        }
    }
}
