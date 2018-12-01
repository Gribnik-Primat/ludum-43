using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeChange : MonoBehaviour {
    public float timeOfGame = 90f;
    public float startOn = 3f;
    // Use this for initialization
    void Awake()
    {
        timeOfGame += startOn;
        InvokeRepeating("TimeIterate", startOn, 1.0f);
    }
    private void TimeIterate()
    {
        timeOfGame -= 1f;
        if (timeOfGame < 0)
        {
            timeOfGame = 0;
        }
        //Debug.Log(timeOfGame);
    }
}
