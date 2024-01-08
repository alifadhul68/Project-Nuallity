using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject[] level1Rooms;
    public GameObject[] level2Rooms;
    public GameObject[] level3Rooms;
    public GameObject[] level4Rooms;
    public GameObject[] selectedRooms;
    public GameObject pathwayPrefab; // Prefab for the pathway
    public Transform player; // Reference to the player transform

    private int numOfRooms;
    private float roomXLegnth;
    private Vector3 roomZLegnth;

    private GameObject currentRoom; // Reference to the current room
    private GameObject prevRoom; // Reference to the previuos room   
    private GameObject intra;
    private bool intraCheck;
    private void Start()
    {
        numOfRooms = UnityEngine.Random.Range(6, 12);
        SelectTheme();//select the prefabs theme
        roomZLegnth = Vector3.zero;
        GenerateRoom(); // Generate the initial room
        intraCheck = false;
    }

    private void Update()
    {
        if (intra != null)
        {
            if (!intraCheck)
            {
                if (intra.gameObject.activeInHierarchy == true)
                {
                    intraCheck = true;
                    StartCoroutine(DeletePreviousRoom());
                }
            }
           
        }

        if (intraCheck == true)
        {
            if (!GameObject.FindGameObjectWithTag("Enemy"))
            {
                GenerateRoom();


                Debug.Log("generated");
            }
        }
    }

    private void SelectTheme()
    {/*
        int ind;
        ind = Random.Range(1,4);
        //randomly select the theme of rooms
        switch (ind)
        {
            case 1:
                selectedRooms = level1Rooms;
                break;
            case 2:
                selectedRooms = level2Rooms;
                break;
            case 3:
                selectedRooms = level3Rooms;
                break;
            case 4:
                selectedRooms = level4Rooms;
                break;
            default:
                print("kldem");
                break;
        }*/
        selectedRooms = level2Rooms;
    }

    private void GenerateRoom()
    {
        if (numOfRooms <= 0)
        {
            //change the room theme
            SelectTheme();
            Debug.Log("theme changed" + numOfRooms);
            numOfRooms = UnityEngine.Random.Range(6, 12);

        }
        prevRoom = currentRoom;
        // Randomly choose a prefab from the selected array
        int randomPrefabIndex = UnityEngine.Random.Range(0, selectedRooms.Length);
        GameObject selectedPrefab = selectedRooms[randomPrefabIndex];
        intra = selectedPrefab.transform.Find("zone").transform.Find("interance").gameObject;
        // Instantiate the selected prefab as the current room
        currentRoom = Instantiate(selectedPrefab, roomZLegnth, Quaternion.identity);
        Renderer[] roomRenderer = currentRoom.GetComponentsInChildren<Renderer>();
        roomZLegnth.z += roomRenderer[0].bounds.size.z; //+ roomRenderer[1].bounds.size.z;

        // Instantiate the pathway prefab between the player and the current room
        //currentPathway = Instantiate(pathwayPrefab, player.position, Quaternion.identity);
        numOfRooms--;

    }

    private IEnumerator DeletePreviousRoom()
    {
        yield return new WaitForSeconds(5f);
        
        if (prevRoom != null)
        {
            Destroy(prevRoom); // Destroy the current room GameObject
        }
        else
            Debug.Log("nothing");

        //if (currentPathway != null)
        //Destroy(currentPathway); // Destroy the current pathway GameObject
    }



}
