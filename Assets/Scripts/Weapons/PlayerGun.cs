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

    float fireRate;
    float reloadTime;
    float spread;
    int maxAmmo;
    int bullets;
    float? travelTime;
    bool speedSpread = false;
    float? damage;

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

        LoadPresets();

        string Prefpreset = PlayerPrefs.GetString("Loadout", "pistol");

        // default preset
        try
        {
            setPreset(Prefpreset);
        }
        catch
        {
            SavePresets();
            LoadPresets();
            setPreset(Prefpreset);
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
            new GunPreset(0.2f, 0.9f, 4.5f, 9, 1, "pistol", null, false, null),
            new GunPreset(0.05f, 1.8f, 10f, 22, 1, "submachine", null, false, 0.3f),
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
