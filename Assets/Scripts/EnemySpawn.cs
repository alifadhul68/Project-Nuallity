using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject trig;
    public GameObject exit;
    public GameObject elite;
    private GameObject check;
    //public float[] ranPos;
    public int nomOfEnemy = 6;
    public GameObject[] enemies;
    private bool spawned;
    private bool areDead;

    // Start is called before the first frame update
    void Start()
    {
        spawned = true;
        areDead = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        if(spawned)
        {
            //checks if the intrance is active
            if (trig.activeInHierarchy == true)
            {
                //to make the if statement called once
                spawned = false;
                EnemySpawning();
                //Debug.Log("T");
            }
        }

        if (!areDead)
        {
            //gets enemy clones if any 
            if (!GameObject.FindGameObjectWithTag("Enemy"))
            {
                areDead = true;
                //disable the exit barrier
                exit.SetActive(false);
            }
        }
             
    }

    public void EnemySpawning()
    {
        //rest os the zones
        if(gameObject.transform.name != "eliteZone"){ 
            int enemyRand;
            Vector3 positionRand;
            for (int i = 0; i <= nomOfEnemy; i++)
            {
                enemyRand = UnityEngine.Random.Range(0, enemies.Length);
                positionRand = new Vector3(UnityEngine.Random.Range(-21f, 20f), 0.3f, UnityEngine.Random.Range(-15f, 13.5f));
                Instantiate(enemies[enemyRand], transform.position + positionRand, Quaternion.identity);
            }
        }
        else//elite zone
        {
            Instantiate(elite, transform.position, Quaternion.identity);
        }
    }
}