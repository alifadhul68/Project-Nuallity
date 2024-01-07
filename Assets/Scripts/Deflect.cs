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
    public GameObject gOb;

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
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isDeflecting && !PauseMenu.isPaused)
        {
            StartCoroutine(Deflecti());
        }
    }

    IEnumerator Deflecti()
    {
        isDeflecting = true;

        //partEmit.enabled = true;
        gOb.SetActive(true);

        //enable the audio and play it
        audioDeflect.enabled = true;
        audioDeflect.Play();

        float startTime = Time.time;
        while (Time.time - startTime < deflectTime)
        {
            //part.Emit(new ParticleSystem.EmitParams() 
            //{ position = UnityEngine.Random.onUnitSphere}, 5);
            gOb.transform.Rotate(new Vector3(0.1f, 0.1f, 0.1f));
            yield return null;
        }
        //disable the audio
        audioDeflect.enabled = false;

        //partEmit.enabled = false;
        gOb.SetActive(false);
        isDeflecting = false;
    }

}
