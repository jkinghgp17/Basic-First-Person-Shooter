using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour, IWeapon {

    public float damage;
    public float range;
    public float fireRate;
    public float impactForce;

    public Transform fpsCamTransform;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public Text ammoGUI;

    public bool autoFire = false;

    public int ammoMax = 30;
    public int ammoCur;

    public float reloadTimeMax = 2f;
    public float reloadTime = -1f;

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
        if (Physics.Raycast(fpsCamTransform.position, fpsCamTransform.forward, out hit, range))
        {
            //Debug.Log(hit.transform.name);
            //Debug.DrawLine(fpsCamTransform.position, hit.transform.position, new Color(0, 0, 255));

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

    void Start()
    {
        ammoCur = ammoMax;
        ammoGUI.text = "Ammo: " + ammoCur + "/" + ammoMax; 
    }

	// Update is called once per frame
	void Update () {
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
