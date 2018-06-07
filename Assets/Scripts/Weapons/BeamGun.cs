using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeamGun : MonoBehaviour, IWeapon
{

    public float damage;
    public float range;
    public float fireRate;
    public float impactForce;

    public Transform fpsCamTransform;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Text ammoGUI;

    public int ammoMax = 30;
    public int ammoCur;

    public float reloadTimeMax = 2f;
    public float reloadTime = -1f;

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
        if (Physics.Raycast(fpsCamTransform.position, fpsCamTransform.forward, out hit, range))
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
            Destroy(impact, 0.1f);
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

        if (Input.GetButton("Fire1") && reloadTime < 0)
        {
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
        return;
    }

    public void setFpsCam(Transform fpsCam)
    {
        fpsCamTransform = fpsCam;
    }
}
