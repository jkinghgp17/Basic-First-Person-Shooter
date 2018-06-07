using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour {

    public NavMeshAgent agent;

	// Update is called once per frame
	void Update ()
    {
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null) {
            agent.SetDestination(player.transform.position);
        }
	}
}
