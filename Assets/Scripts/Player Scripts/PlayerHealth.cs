using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    public Text healthText;

    public int maxArmor = 100;
    public int armor;

    public int maxHealth = 100;
    public int health;

	// Use this for initialization
	void Start () {
        health = maxHealth;
        armor = maxArmor;
        healthText.text = "Health" + health + "/" + maxHealth + "\n" + "Armor" + armor + "/" + maxArmor;
	}
	
	public void takeDamage(int amount)
    {
        if (armor > 0)
        {
            armor -= amount;
            if (armor < 0)
            {
                amount = -armor;
            }
            else
            {
                amount = 0;
            }
        }

        health -= amount;
        healthText.text = "Health" + health + "/" + maxHealth + "\nArmor" + armor + "/" + maxArmor;
    }
}
