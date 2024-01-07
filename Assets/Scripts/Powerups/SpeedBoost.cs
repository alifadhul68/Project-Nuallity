using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField] private float boostAmount = 3f; // Adjust as needed
    [SerializeField] private float duration = 15f;   // Adjust as needed

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                playerMovement.ApplySpeedBoost(boostAmount, duration);
                Destroy(gameObject);
            }
        }
    }
}
