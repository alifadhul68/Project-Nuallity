using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAllEnemies : MonoBehaviour
{
    private string powerupTitle = "Enemy Clean Up";
    private string powerupDescription = "All enemies in the room are instantly killed";
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
            PopupManager.Instance.ShowPopup(powerupTitle, powerupDescription, 2.5f);
            Destroy(gameObject);
        }
    }
}
