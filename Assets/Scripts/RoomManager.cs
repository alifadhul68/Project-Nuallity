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
    private GameObject currentPathway; // Reference to the current pathway

    private void Start()
    {
        numOfRooms = UnityEngine.Random.Range(6,12);
        SelectTheme();//select the prefabs theme
        roomZLegnth = Vector3.zero;
        GenerateRoom(); // Generate the initial room

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateRoom();
            StartCoroutine(DeletePreviousRoom());    
            
            Debug.Log("generated");
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
        if(numOfRooms > 0)
        {
            // Randomly choose a prefab from the selected array
            int randomPrefabIndex = UnityEngine.Random.Range(0, selectedRooms.Length);
            GameObject selectedPrefab = selectedRooms[randomPrefabIndex];

            // Instantiate the selected prefab as the current room
            currentRoom = Instantiate(selectedPrefab, roomZLegnth, Quaternion.identity);
            Renderer[] roomRenderer = currentRoom.GetComponentsInChildren<Renderer>();
            roomZLegnth.z += roomRenderer[0].bounds.size.z; //+ roomRenderer[1].bounds.size.z;

            // Instantiate the pathway prefab between the player and the current room
            //currentPathway = Instantiate(pathwayPrefab, player.position, Quaternion.identity);
            numOfRooms--;
        }
        else
        {
            //change the room theme
            SelectTheme();
            Debug.Log("theme changed"+numOfRooms);
            numOfRooms = UnityEngine.Random.Range(6, 12);
            GenerateRoom();
        }

    }

    private IEnumerator DeletePreviousRoom()
    {        
        yield return new WaitForSeconds(5f);
        GameObject currentR = GameObject.FindWithTag("zone");
        if (currentR != null)
        {
            Destroy(currentR); // Destroy the current room GameObject
        }
        else
            Debug.Log("nothing");

        if (currentPathway != null)
        {
            //Destroy(currentPathway); // Destroy the current pathway GameObject
        }
    }

    
}
