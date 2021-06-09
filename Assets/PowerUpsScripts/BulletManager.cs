using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BulletManager : MonoBehaviour
{

    public static void Shoot(Transform firePoint, GameObject powerUp, Quaternion shootingAngle, string playerName, bool powerUpBullet)
    {
        GameObject bullet = Instantiate(powerUp, firePoint.transform.position, shootingAngle);
        bullet.tag = playerName + " bullet";
        var bulletBase = bullet.GetComponent<BulletBase>();
        bulletBase.Shoot(firePoint, powerUp, shootingAngle, playerName, powerUpBullet);
    }




}