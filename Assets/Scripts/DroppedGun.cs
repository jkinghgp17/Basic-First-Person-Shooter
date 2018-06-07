using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroppedGun : MonoBehaviour {
    
    public GameObject weaponPrefab;
    public Text pickUpText;
    
	void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            PlayerController pc = other.GetComponent<PlayerController>();

            if (pc != null)
            {
                pc.addWeapon(weaponPrefab);
                pickUpText.text = "";
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickUpText.text = "Press E to pickup " + weaponPrefab.name;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pickUpText.text = "";
        }
    }
}
