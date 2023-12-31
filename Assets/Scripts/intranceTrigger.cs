using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class intranceTrigger : MonoBehaviour
{
    public GameObject intrance;

       
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        { 
            intrance.SetActive(true);
            
        }
    }
}
