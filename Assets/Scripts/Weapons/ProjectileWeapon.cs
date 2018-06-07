using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileWeapon : MonoBehaviour, IWeapon {

    public GameObject projectilePrefab;
    private Projectile projectile;

    public Transform gunTip;

    public Transform fpsCamTransform;

    public float range = 1000f;

    public int ammoMax = 30;
    public int ammoCur;
    public Text ammoGUI;

    public bool autoFire = false;

    public float reloadTimeMax = 2f;
    public float reloadTime = -1f;

    public float nextTimeToFire = 0f;
    public float fireRate = 1f;
    public float projectileLifetime = 2f;

    void Start()
    {
        projectile = projectilePrefab.GetComponent<Projectile>();

        ammoCur = ammoMax;
        ammoGUI.text = "Ammo: " + ammoCur + "/" + ammoMax;
    }

    void Shoot()
    {
        if (ammoCur <= 0)
        {
            Reload();
            return;
        }

        ammoCur--;

        ammoGUI.text = "Ammo: " + ammoCur + "/" + ammoMax;

        Projectile p;
        Quaternion originalRotation = gunTip.rotation;
        RaycastHit hit;

        if (Physics.Raycast(fpsCamTransform.position, fpsCamTransform.forward, out hit, range))
        {
            //Debug.DrawLine(fpsCamTransform.position, hit.transform.position, new Color(0,0,255));
            //Debug.Log("Halp");

            gunTip.LookAt(hit.point);
            p = Instantiate(projectile, gunTip.position, gunTip.rotation);
            p.lifeTime = projectileLifetime;

            gunTip.rotation = originalRotation;
            return;
        }

        
        p = Instantiate(projectile, gunTip.position, transform.rotation);
        p.lifeTime = projectileLifetime;
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

        if (autoFire)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && reloadTime < 0)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        } else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && reloadTime < 0)
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    private void Reload()
    {
        reloadTime = reloadTimeMax;
    }

    public void addDamage(float amount)
    {
        projectile.damage += amount;
    }

    public float getDamage()
    {
        return projectile.damage;
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
