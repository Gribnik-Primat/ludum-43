using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    public int score;
    public int iteratePlusScore;
    public float iterateTimeOnFloat;
    void Start()
    {
        InvokeRepeating("PlusScore", iterateTimeOnFloat, iterateTimeOnFloat);
    }
    private void PlusScore()
    {
        score += iteratePlusScore;
    }
    private void Update()
    {
        if (score < 0)
            score = 0;
    }
}
