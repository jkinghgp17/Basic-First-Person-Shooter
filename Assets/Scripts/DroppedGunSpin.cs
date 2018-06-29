using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedGunSpin : MonoBehaviour {

    public float spinSpeed = 250f;

    void Update()
    {
        transform.Rotate(new Vector3(0f, spinSpeed * Time.deltaTime, 0f));
    }
}
