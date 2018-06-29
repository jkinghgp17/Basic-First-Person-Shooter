using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public IWeapon weapon;
    public PlayerMove pMove;
    public PlayerHealth pHealth;

    public GameObject weaponHolder;
    public Transform cameraTransform;

    private bool isOnFire = false;
    private float lastTimeOnFire = 0f;
    private int fireDamage = 0;

    private bool isOnJumpPad = false;
    private float lastTimeOnJumpPad = 0f;
    private float jumpPadIncrease = 0f;

    float plusDamageTime = 0f;
    float plusDamage;

    float plusFireRateTime = 0f;
    float plusFireRate;

    float plusSpeed = 0f;
    float plusSpeedTime;

    float plusJump = 0f;
    float plusJumpTime;

    float knockBackTime;

    void Update()
    {
        if (isOnFire)
        {
            pHealth.takeDamage(fireDamage);

            if (lastTimeOnFire < (Time.time - Time.deltaTime))
            {
                isOnFire = false;
                fireDamage = 0;
            }
        }

        if (isOnJumpPad)
        {
            if (lastTimeOnJumpPad < (Time.time - Time.deltaTime))
            {
                isOnJumpPad = false;
                pMove.addJump(-jumpPadIncrease);
                jumpPadIncrease = 0;
            }
        }

        if (plusDamageTime < 0)
        {
            weapon.addDamage(-plusDamage);
            plusDamage = 0;
        }
        else
        {
            plusDamageTime -= Time.deltaTime;
        }

        if (plusFireRateTime < 0)
        {
            weapon.addFireRate(-plusFireRate);
            plusJump = 0;
        }
        else
        {
            plusFireRateTime -= Time.deltaTime;
        }

        if (plusSpeedTime < 0)
        {
            pMove.addSpeed(-plusSpeed);
            plusSpeed = 0;
        }
        else
        {
            plusSpeedTime -= Time.deltaTime;
        }

        if (plusJumpTime < 0)
        {
            pMove.addJump(-plusJump);
            plusJump = 0;
        }
        else
        {
            plusJumpTime -= Time.deltaTime;
        }

        if (knockBackTime < 0)
        {
            pMove.addKnockBack(0f, Vector3.zero);
        }
        else
        {
            knockBackTime -= Time.deltaTime;
        }
    }

    public void increaseDamage(float amount, float time)
    {
        plusDamage = amount;
        weapon.addDamage(plusDamage);
        plusDamageTime = time;
    }

    public void multiplyDamage(float amount, float time)
    {
        plusDamage = weapon.getDamage() * amount;
        weapon.addDamage(plusDamage);
        plusDamageTime = time;
    }

    public void increaseFireRate(float amount, float time)
    {
        plusFireRate = amount;
        weapon.addFireRate(plusFireRate);
        plusFireRateTime = time;
    }

    public void multiplyFireRate(float amount, float time)
    {
        plusFireRate = amount;
        weapon.addFireRate(plusFireRate);
        plusFireRateTime = time;
    }

    public void increaseSpeed(float amount, float time)
    {
        plusSpeed = amount;
        pMove.addSpeed(plusSpeed);
        plusSpeedTime = time;
    }

    public void multiplySpeed(float amount, float time)
    {
        plusSpeed = pMove.walkSpeed * amount;
        pMove.addSpeed(plusSpeed);
        plusSpeedTime = time;
    }

    public void increaseJump(float amount, float time)
    {
        plusJump = amount;
        pMove.addJump(plusJump);
        plusJumpTime = time;
    }

    public void addWeapon(GameObject newWeapon)
    {
        GameObject nw = Instantiate(newWeapon, weaponHolder.transform);
        nw.SetActive(false);
        //nw.transform.position = weaponHolder.transform.position + new Vector3(0.5f, -0.525f, 1);
        nw.transform.rotation = weaponHolder.transform.rotation;
        IWeapon nwg = nw.GetComponent<IWeapon>();
        if (nwg != null)
        {
            nwg.setFpsCam(cameraTransform);
        }
    }

    public void setFire(bool state, int amount)
    {
        isOnFire = state;
        fireDamage = amount;
            
        if (state)
        {
            lastTimeOnFire = Time.time;
        }
    }

    public void addKnockBack(float amount, Vector3 direction, float time)
    {
        pMove.addKnockBack(amount, direction);
        knockBackTime = time;
    }

    public void jumpPad(float amount)
    {
        isOnJumpPad = true;
        jumpPadIncrease = amount;
        lastTimeOnJumpPad = Time.time;
        pMove.addJump(jumpPadIncrease);
    }


}
