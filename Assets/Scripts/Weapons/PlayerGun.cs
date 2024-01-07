using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGun : MonoBehaviour
{

    public static PlayerGun Instance;

    private float nextTimeToShoot = 0;

    [SerializeField] 
    private Image reloadIndicator;
    [SerializeField] 
    private TextMeshProUGUI ammoUI;
    [SerializeField]
    private TextMeshProUGUI maxAmmoUI;
    [SerializeField]
    public Transform firingPoint;
    [SerializeField]
    GameObject projectilePrefab;

    float fireRate;
    float reloadTime;
    float spread;
    int maxAmmo;
    int bullets;
    float? travelTime;
    bool speedSpread = false;
    float? damage;

    public enum Presets
    {
        pistol,
        submachine,
        sawedoff
    }
    public Presets preset;

    private int ammoInMag;
    private bool reloading;

    // Gun presets
    private static Dictionary<string, GunPreset> weaponPresets = new Dictionary<string, GunPreset>();

    void Awake()
    {
    }

    private void Start()
    {
        Instance = GetComponent<PlayerGun>();

        // order: fire rate, reload time, spread, max ammo, bullets shot per click, [optionals]> travel time, speedSpread, damage
        weaponPresets.Add("pistol", 
            new GunPreset(0.2f, 0.9f, 4.5f, 9, 1, null, false, null
            ));
        weaponPresets.Add("submachine", 
            new GunPreset(0.05f, 1.8f, 10f, 22, 1, null, false, 0.3f
            ));
        weaponPresets.Add("sawedoff", 
            new GunPreset(0.3f, 1.5f, 13f, 2, 6, 0.5f, true, 0.4f
            ));
        
        // default preset
        setPreset(preset.ToString());
    }

    private void Update()
    {
        if (reloading)
        {
            handleReloadIndicator();
        }

        reloadIndicator.transform.position = Input.mousePosition;
    }

    private void handleReloadIndicator()
    {
        if (Time.time > nextTimeToShoot)
        {
            reloadIndicator.fillAmount = 0;
            reloading = false;

            UpdateAmmo();

        } else
        {
            reloadIndicator.fillAmount = ((nextTimeToShoot - Time.time) / reloadTime) * 1;
        }
    }

    private void setPreset(string preset)
    {
        Instance.fireRate = weaponPresets[preset].FireRate;
        Instance.maxAmmo = weaponPresets[preset].MaxAmmo;
        Instance.reloadTime = weaponPresets[preset].ReloadTime;
        Instance.spread = weaponPresets[preset].Spread;
        Instance.bullets = weaponPresets[preset].Bullets;

        if (weaponPresets[preset].TravelTime != null)
        {
            Instance.travelTime = (float)weaponPresets[preset].TravelTime;
        }
        else
        {
            Instance.travelTime = null; // makes sure travel time doesn't affect a preset laoded midgame, same with others
        }

        if (weaponPresets[preset].SpeedSpread)
        {
            Instance.speedSpread = true;
        }
        else
        {
            Instance.speedSpread = false;
        }

        if (weaponPresets[preset].Damage != null)
        {
            Instance.damage = (float)weaponPresets[preset].Damage;
        }
        else
        {
            Instance.damage = null;
        }

        //set ammo values
        UpdateAmmo();
        UpdateMaxAmmo();
    }

    // instantiates projectile on player input 
    public void Shoot()
    {
        if (PauseMenu.isPaused)
        {
            return;
        }
        if (ammoInMag <= 0)
        {
            Reload();
            return;
        }
        if (Time.time >= nextTimeToShoot + Time.deltaTime) // don't remove time.deltatime or the reload indicator glitches
        {
            nextTimeToShoot = Time.time + fireRate;
            for (int i = 0; i < bullets; i++)
            {
                var bullet = Instantiate(projectilePrefab, firingPoint.position, firingPoint.rotation);
                Projectile bulletScript = bullet.GetComponent<Projectile>();
                bullet.transform.Rotate(0, Random.Range(-spread+i, spread+i), 0); // +i makes it so bullet spread is higher per bullet
                if (bulletScript != null)
                {
                    bulletScript.SetShooter(transform.parent.gameObject); // Set this GameObject as the shooter
                }
                if (travelTime != null)
                {
                    bullet.GetComponentInChildren<Projectile>().travelTime = (float)travelTime;
                }  

                if (speedSpread)
                {
                    bullet.GetComponentInChildren<Projectile>().speed += Random.Range(0,spread/2);
                }

                if (damage != null)
                {
                    bullet.GetComponentInChildren<Projectile>().damage = (float)damage;
                }
            }
            ammoInMag -= 1;
            UpdateAmmo();
        }
    }

    public void Reload()
    {
        if (ammoInMag < maxAmmo)
        {
            reloading = true;
            nextTimeToShoot = Time.time + reloadTime;
            ammoInMag = maxAmmo;
        }
    }

    private class GunPreset
    {
        float fireRate; 
        float reloadTime;
        float spread;
        int maxAmmo;
        int bullets;
        float? travelTime;
        bool speedSpread = false;
        float? damage;

        public float FireRate { get => fireRate; set => fireRate = value; }
        public float ReloadTime { get => reloadTime; set => reloadTime = value; }
        public float Spread { get => spread; set => spread = value; }
        public int MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
        public int Bullets { get => bullets; set => bullets = value; }
        public float? TravelTime { get => travelTime; set => travelTime = value; }
        public bool SpeedSpread { get => speedSpread; set => speedSpread = value; }
        public float? Damage { get => damage; set => damage = value; }

        public GunPreset(float fireRate, float reloadTime, float spread, int maxAmmo, int bullets)
        {
            this.FireRate = fireRate;
            this.ReloadTime = reloadTime;
            this.Spread = spread;
            this.MaxAmmo = maxAmmo;
            this.Bullets = bullets;
        }

        public GunPreset(float fireRate, float reloadTime, float spread, int maxAmmo, int bullets, float? travelTime, bool speedSpread = false, float? damage = null) : this(fireRate, reloadTime, spread, maxAmmo, bullets)
        {
            this.travelTime = travelTime;
            this.SpeedSpread = speedSpread;
            this.damage = damage;
        }
    }

    private void UpdateAmmo()
    {
        ammoUI.text = ammoInMag.ToString();
    }

    private void UpdateMaxAmmo()
    {
        maxAmmoUI.text = maxAmmo.ToString();
    }

}
