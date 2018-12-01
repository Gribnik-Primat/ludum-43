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
        Slider t1 = child1.GetComponent<Slider>();
        Slider t2 = child2.GetComponent<Slider>();
        Text t3 = child3.GetComponent<Text>();
        t1.value = player1.GetComponent<PlayerStats>().score;
        t2.value = player2.GetComponent<PlayerStats>().score;
        t3.text = cam.GetComponent<TimeChange>().timeOfGame.ToString();
    }
}
