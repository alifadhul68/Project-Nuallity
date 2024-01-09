using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HhaSound : MonoBehaviour
{
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audio.enabled = true;
            audio.Play();
        }
    }

}
