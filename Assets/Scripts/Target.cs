using UnityEngine;

public class Target : MonoBehaviour {

    public float maxHealth;
    public float health;

    void Start()
    {
        health = maxHealth;
    }

    void Die()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
        if (health <= 0)
        {
            Die();
        }
    }
}
