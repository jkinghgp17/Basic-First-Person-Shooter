using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon {
    float getDamage();
    void addDamage(float amount);
    void addFireRate(float amount);

    void setFpsCam(Transform fpsCam);
}
