using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TakesDamage : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float maxhp;
    private float hp;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        //slider.transform.rotation = cam.transform.rotation;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);
            hp -= other.gameObject.GetComponentInChildren<Projectile>().damage;
            slider.value = hp/maxhp;
            //Debug.Log("took this much damage: " + other.gameObject.GetComponentInChildren<Projectile>().damage);
            //if (hp <= 0) {
            //    Destroy(gameObject);
            //}
        }
    }
}
