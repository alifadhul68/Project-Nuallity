using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflect : MonoBehaviour
{

    [SerializeField]
    public bool isDeflecting = false;
    [SerializeField]
    public float deflectTime = 1f;
    [SerializeField]
    private float deflectCooldown = 2f;
    public GameObject gOb;

    private float lastDeflectTime = -Mathf.Infinity; // Initialize to a time in the past

    //audio variable for deflect
    private AudioSource audioDeflect;

    // Start is called before the first frame update
    void Start()
    {
        //looks for the audioSource comp in the player
        audioDeflect = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && Time.time >= lastDeflectTime + deflectCooldown && !isDeflecting && !PauseMenu.isPaused)
        {
            StartCoroutine(Deflecti());
        }
    }

    IEnumerator Deflecti()
    {
        isDeflecting = true;

        gOb.SetActive(true);
        audioDeflect.enabled = true;
        audioDeflect.Play();

        float startTime = Time.time;
        while (Time.time - startTime < deflectTime)
        {
            gOb.transform.Rotate(new Vector3(0.1f, 0.1f, 0.1f));
            yield return null;
        }

        audioDeflect.enabled = false;
        gOb.SetActive(false);
        lastDeflectTime = Time.time; // Set the timestamp for the last deflect
        isDeflecting = false;
    }

}
