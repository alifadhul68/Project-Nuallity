using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
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

    public float fireRate;
    public float reloadTime;
    public float spread;
    public int maxAmmo;
    public int bullets;
    public float travelTime;
    public bool speedSpread = false;
    public float damage;

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
        //weaponPresets.Add("pistol", 
        //    new GunPreset(0.2f, 0.9f, 4.5f, 9, 1, "pistol", null, false, 0.5f
        //    ));
        //weaponPresets.Add("submachine", 
        //    new GunPreset(0.05f, 1.8f, 10f, 22, 1, "submachine", null, false, 0.3f
        //    ));
        //weaponPresets.Add("sawedoff", 
        //    new GunPreset(0.3f, 1.5f, 13f, 2, 6, "sawedoff", 0.5f, true, 0.4f
        //    ));

        string Prefpreset = PlayerPrefs.GetString("Loadout", "pistol");

        try
        {
            weaponPresets.Clear();
            LoadPresets();
            SetPreset(Prefpreset);
        }
        catch
        {
            SavePresets();
            LoadPresets();
            SetPreset(Prefpreset);
        }
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

    private void SetPreset(string preset)
    {
        Instance.fireRate = weaponPresets[preset].FireRate;
        Instance.maxAmmo = weaponPresets[preset].MaxAmmo;
        Instance.reloadTime = weaponPresets[preset].ReloadTime;
        Instance.spread = weaponPresets[preset].Spread;
        Instance.bullets = weaponPresets[preset].Bullets;
        Instance.travelTime = (float)weaponPresets[preset].TravelTime;
        Instance.speedSpread = weaponPresets[preset].SpeedSpread;
        Instance.damage = (float)weaponPresets[preset].Damage;

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
                if (travelTime != 0)
                {
                    bullet.GetComponentInChildren<Projectile>().travelTime = (float)travelTime;
                }  

                if (speedSpread)
                {
                    bullet.GetComponentInChildren<Projectile>().speed += Random.Range(0,spread/2);
                }

                if (damage != 0)
                {
                    bullet.GetComponentInChildren<Projectile>().damage = (float)damage;
                    //Debug.Log(damage);
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

    public void ApplyDamageBoost(float boostAmount, float duration)
    {
        // Apply the speed boost to the player
        StartCoroutine(DamageBoostRoutine(boostAmount, duration));
    }
    private IEnumerator DamageBoostRoutine(float boostAmount, float duration)
    {
        float originalDamage = Instance.damage;
        Instance.damage += boostAmount;

        yield return new WaitForSeconds(duration);

        Instance.damage = originalDamage;
    }

    

    private void UpdateAmmo()
    {
        ammoUI.text = ammoInMag.ToString();
    }

    private void UpdateMaxAmmo()
    {
        maxAmmoUI.text = maxAmmo.ToString();
    }

    private void SavePresets()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/presets"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/presets");
        }

        // order: fire rate, reload time, spread, max ammo, bullets shot per click, [optionals]> travel time, speedSpread, damage
        GunPreset[] presets = new GunPreset[3] {
            new GunPreset(0.2f, 0.9f, 4.5f, 9, 1, "pistol", 0, false, 0.5f),
            new GunPreset(0.05f, 1.8f, 10f, 22, 1, "submachine", 0, false, 0.3f),
            new GunPreset(0.3f, 1.5f, 13f, 2, 6, "sawedoff", 0.5f, true, 0.4f)
        };

        foreach (GunPreset preset in presets)
        {
            string filePath = Application.persistentDataPath + "/presets/" + preset.name + ".json";
            string fileText = JsonUtility.ToJson(preset);
            //Debug.Log(fileText);
            File.WriteAllText(filePath, fileText);
        }
    }

    private void LoadPresets()
    {
        string filePath = Application.persistentDataPath + "/presets/";
        foreach (var file in new DirectoryInfo(filePath).GetFiles())
        {
            string fileText = File.ReadAllText(filePath + file.Name);
            //Debug.Log(fileText);
            GunPreset preset = JsonUtility.FromJson<GunPreset>(fileText);
            weaponPresets.Add(preset.name, preset);
        }
    }
}
