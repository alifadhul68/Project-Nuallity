using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflect : MonoBehaviour
{

    [SerializeField]
    private bool isDeflecting = false;
    [SerializeField]
    public float deflectTime = 1f;
    public GameObject gOb;
    public ParticleSystem part;
    private ParticleSystem.EmissionModule partEmit;


    // Start is called before the first frame update
    void Start()
    {
        partEmit = part.emission;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isDeflecting)
        {
            StartCoroutine(Deflecti());
        }
    }

    IEnumerator Deflecti()
    {
        isDeflecting = true;

        partEmit.enabled = true;
        gOb.SetActive(true);

        float startTime = Time.time;
        while (Time.time - startTime < deflectTime)
        {
            part.Emit(new ParticleSystem.EmitParams() 
            { position = UnityEngine.Random.onUnitSphere}, 5);
            yield return null;
        }

        partEmit.enabled = false;
        gOb.SetActive(false);
        isDeflecting = false;
    }
}
