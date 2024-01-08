using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class GunPreset
{
    public string name;
    public float fireRate;
    public float reloadTime;
    public float spread;
    public int maxAmmo;
    public int bullets;
    public float? travelTime;
    public bool speedSpread = false;
    public float? damage;

    public float FireRate { get => fireRate; set => fireRate = value; }
    public float ReloadTime { get => reloadTime; set => reloadTime = value; }
    public float Spread { get => spread; set => spread = value; }
    public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
    public int Bullets { get => bullets; set => bullets = value; }
    public float? TravelTime { get => travelTime; set => travelTime = value; }
    public bool SpeedSpread { get => speedSpread; set => speedSpread = value; }
    public float? Damage { get => damage; set => damage = value; }
    public string Name { get => name; set => name = value; }

    public GunPreset(float fireRate, float reloadTime, float spread, int maxAmmo, int bullets, string name)
    {
        this.fireRate = fireRate;
        this.reloadTime = reloadTime;
        this.spread = spread;
        this.maxAmmo = maxAmmo;
        this.bullets = bullets;
        this.name = name;
    }

    public GunPreset(float fireRate, float reloadTime, float spread, int maxAmmo, int bullets, string name, float? travelTime, bool speedSpread = false, float? damage = null) 
        : this(fireRate, reloadTime, spread, maxAmmo, bullets, name)
    {
        this.travelTime = travelTime;
        this.speedSpread = speedSpread;
        this.damage = damage;
    }
}
