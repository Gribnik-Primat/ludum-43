using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MovePlatform : MonoBehaviour {
    public Vector3 destinationDistance = new Vector3(0f, 1f, 0f);
    public float smoothing;
    public GameObject platform;

    private bool Check = false;
    private Vector3 startPos;
    private Vector3 destinationPoint;

    private void Start()
    {
        startPos = new Vector3(platform.transform.position.x, platform.transform.position.y, 0);
        destinationPoint = startPos - destinationDistance;
    }

    void Update()
    {
        //Debug.Log(Check);
        if (Check == false)
        {
            transform.position = Vector3.Lerp(transform.position, destinationPoint, smoothing * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPos, smoothing * Time.deltaTime);
        }
        if (platform.transform.position.y < (destinationPoint - new Vector3(0f, -0.05f, 0f)).y)
        {
            Check = true;
        }
        if (platform.transform.position.y > (startPos - new Vector3(0f, 0.05f, 0f)).y)
        {
            Check = false;
        }
    }
}
