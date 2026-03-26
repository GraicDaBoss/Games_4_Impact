using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class camerafollow : MonoBehaviour
{

    public Transform playerPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerPos.position + new Vector3(1, 2, -10); 
    }
}
