using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoomManager : MonoBehaviour
{
    public GameObject[] level1Rooms;
    public GameObject[] level2Rooms;
    public GameObject[] level3Rooms;
    public GameObject[] level4Rooms;
    public GameObject[] selectedRooms;
    private float startingSize = 35.61682f + 29.54305f;
    private int numOfRooms;
    private Vector3 roomZLegnth;

    private GameObject currentRoom; // Reference to the current room
    private GameObject prevRoom; // Reference to the previuos room   
    private GameObject intra;
    private bool intraCheck;
    private bool firstTime = false;
    private void Start()
    {

        intraCheck = false;
        numOfRooms = UnityEngine.Random.Range(5, 7);
        SelectTheme();//select the prefabs theme
        GenerateRoom(); // Generate the initial room
        
    }

    private void Update()
    {
        if (intraCheck == true)
        {
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                GenerateRoom();
                intraCheck = false;

                Debug.Log("generated");
            }
        }

        if (intra != null)
        {
            if (intraCheck == false)
            {
                if (intra.activeInHierarchy == true)
                {
                    Debug.Log("intra active");
                    intraCheck = true;
                    StartCoroutine(DeletePreviousRoom());
                }
            }
           
        }

        
    }

    private void SelectTheme() { 
    
        int ind;
        ind = UnityEngine.Random.Range(1,4);
        //randomly select the theme of rooms
        switch (ind)
        {
            case 1:
                selectedRooms = level2Rooms;
                break;
            case 2:
                selectedRooms = level2Rooms;
                break;
            case 3:
                selectedRooms = level3Rooms;
                break;
            case 4:
                selectedRooms = level3Rooms;
                break;
            default:
                print("kldem");
                break;
        }
        
    }

    private void GenerateRoom()
    {
        if (numOfRooms <= 0)
        {
            //change the room theme
            SelectTheme();            
            Debug.Log("theme changed");
            numOfRooms = UnityEngine.Random.Range(1, 1);

        }
        prevRoom = currentRoom;
        // Randomly choose a prefab from the selected array
        int randomPrefabIndex = UnityEngine.Random.Range(0, selectedRooms.Length);
        GameObject selectedPrefab = selectedRooms[randomPrefabIndex];
        if (firstTime == false)
            GetFirstVector(selectedPrefab);
        else
        {
            Renderer roomRenderer1 = selectedPrefab.GetComponentInChildren<Renderer>();
            roomZLegnth.z += roomRenderer1.bounds.size.z / 2;
        }

        // Instantiate the selected prefab as the current room
        currentRoom = Instantiate(selectedPrefab, roomZLegnth, Quaternion.identity);
        intra = currentRoom.transform.Find("zone").Find("inter_trig").Find("interance").gameObject;
        //intra = currentRoom.transform.Find("exit").gameObject;
        Renderer roomRenderer2 = currentRoom.GetComponentInChildren<Renderer>();
        roomZLegnth.z += roomRenderer2.bounds.size.z / 2;

        numOfRooms--;

    }

    private void GetFirstVector(GameObject sP)
    {
        roomZLegnth = Vector3.zero;
        roomZLegnth.z += startingSize + (sP.transform.Find("zone").GetComponent<Renderer>().bounds.size.z / 2);
        firstTime = true;
    }

    private IEnumerator DeletePreviousRoom()
    {
        yield return new WaitForSeconds(1.5f);
        
        if (prevRoom != null)
        {
            Destroy(prevRoom); // Destroy the current room GameObject
        }
        else
            Debug.Log("nothing");

    }



}
