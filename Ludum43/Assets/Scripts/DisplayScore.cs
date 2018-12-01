using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour {

    public GameObject player1;
    public GameObject player2;
    public GameObject cam;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject child1 = GameObject.FindGameObjectWithTag("Score");
        GameObject child2 = GameObject.FindGameObjectWithTag("Score1");
        GameObject child3 = GameObject.FindGameObjectWithTag("Time");
        Text t1 = child1.GetComponent<Text>();
        Text t2 = child2.GetComponent<Text>();
        Text t3 = child3.GetComponent<Text>();
        t1.text = "Your score: " + player1.GetComponent<PlayerStats>().score.ToString();
        t2.text = "Your score: " + player2.GetComponent<PlayerStats>().score.ToString();
        t3.text = cam.GetComponent<TimeChange>().timeOfGame.ToString();
    }
}
