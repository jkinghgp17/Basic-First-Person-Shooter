using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpreadGun : MonoBehaviour, IWeapon
{

    public float damage = 10f;
    public float range = 10000f;
    public float fireRate = 1f;
    public float impactForce = 0f;

    public Transform fpsCamTransform;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Text ammoGUI;

    public int shotFragments = 5;
    public float spreadAngle = 10f;

    public int ammoMax = 30;
    public int ammoCur;

    public float reloadTimeMax = 2f;
    private float reloadTime = -1f;

    private float nextTimeToFire = 0f;

    void Shoot()
    {
        if (ammoCur <= 0)
        {
            Reload();
            return;
        }

        ammoCur--;

        ammoGUI.text = "Ammo: " + ammoCur + "/" + ammoMax;

        muzzleFlash.Play();

        RaycastHit hit;

        for (int i = 0; i < shotFragments; i++)
        {
            Quaternion fireRotation = Quaternion.LookRotation(fpsCamTransform.forward);

            Quaternion randomRotation = Random.rotation;

            fireRotation = Quaternion.RotateTowards(fireRotation, randomRotation, Random.Range(0.0f, spreadAngle));


            if (Physics.Raycast(fpsCamTransform.position, fireRotation*Vector3.forward, out hit, range))
            {
                //Debug.Log(hit.transform.name);

                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    target.TakeDamage(damage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * impactForce);
                }

                GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(impact, 0.5f);
            }
        }
    }

    void Start()
    {
        ammoCur = ammoMax;
        ammoGUI.text = "Ammo: " + ammoCur + "/" + ammoMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (reloadTime > 0)
        {
            reloadTime -= Time.deltaTime;
            if (reloadTime < 0)
            {
                ammoCur = ammoMax;
                ammoGUI.text = "Ammo: " + ammoCur + "/" + ammoMax;
            }
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && reloadTime < 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
    }

    public void addDamage(float amount)
    {
        damage += amount;
    }

    void Reload()
    {
        reloadTime = reloadTimeMax;
    }

    public float getDamage()
    {
        return damage;
    }

    public void addFireRate(float amount)
    {
        fireRate -= amount;
        if (fireRate <= 0)
            fireRate = 0.001f;
    }

    public void setFpsCam(Transform fpsCam)
    {
        fpsCamTransform = fpsCam;
    }
}
