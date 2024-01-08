using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public Quaternion rotation;

    // Update is called once per frame
    void Update()
    {
        if (rotation != null)
        {
            gameObject.transform.rotation = rotation;
        }
        float speed = Time.deltaTime * 25;
        gameObject.transform.Rotate(0, speed, 0);
        rotation = gameObject.transform.rotation;
    }
}
