using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour {

    public EnemyProjectile projectile;
    public float projectileLifetime = 2f;

    public Transform shootFrom;

    public float fireRate = 1f;
    private float nextTimeToFire = 0f;

    public NavMeshAgent agent;
    public float range = 10f;

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            Transform pTran = player.transform;

            if (Vector3.Distance(transform.position, pTran.position) < range)
            {
                RaycastHit hit;

                if (Physics.Raycast(transform.position, transform.forward, out hit, range))
                {

                    if (hit.transform.gameObject.tag == "Player" && Time.time >= nextTimeToFire)
                    {
                        nextTimeToFire = Time.time + 1f / fireRate;
                        fireProjectile(pTran.position);
                    }
                }
            } 

            agent.SetDestination(player.transform.position);
        }
    }

    void fireProjectile(Vector3 target)
    {
        EnemyProjectile p = Instantiate(projectile, shootFrom.position, transform.rotation);
        p.lifeTime = projectileLifetime;
    }
}
