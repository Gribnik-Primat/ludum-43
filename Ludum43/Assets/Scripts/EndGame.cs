﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour {
    public GameObject Score;
    public GameObject Score1;
    public GameObject Time;
    public GameObject Player;
    public GameObject Player1;
    public GameObject start;
    public GameObject Text;
    public GameObject Text1;
    public GameObject Text2;
    public DisplayScore Ds;
    public AudioSource audioplayer;
    public AudioClip audio;

    private bool isEnd;
    private bool timeCheck = true;
    private AsyncOperation asyncOperation;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (!isEnd)
        {
            if (GameObject.FindObjectOfType<TimeChange>().timeOfGame <= 0 && timeCheck == true)
            {
                Ds.enabled = false;
                Score.SetActive(false);
                Score1.SetActive(false);
                Time.SetActive(false);
                Player.SetActive(false);
                Player1.SetActive(false);
                timeCheck = false;
                asyncOperation = SceneManager.LoadSceneAsync("Menu");
                asyncOperation.allowSceneActivation = false;
                Text.SetActive(true);
                isEnd = true;
            }
            if (Player.GetComponent<PlayerStats>().score > 499 && timeCheck == true)
            {
                Ds.enabled = false;
                Score.SetActive(false);
                Score1.SetActive(false);
                Time.SetActive(false);
                Player.SetActive(false);
                Player1.SetActive(false);
                timeCheck = false;
                asyncOperation = SceneManager.LoadSceneAsync("Menu");
                asyncOperation.allowSceneActivation = false;
                Text1.SetActive(true);
                audioplayer.PlayOneShot(audio);
                isEnd = true;
            }
            if (Player1.GetComponent<PlayerStats>().score > 499 && timeCheck == true)
            {
                Ds.enabled = false;
                Score.SetActive(false);
                Score1.SetActive(false);
                Time.SetActive(false);
                Player.SetActive(false);
                Player1.SetActive(false);
                timeCheck = false;
                asyncOperation = SceneManager.LoadSceneAsync("Menu");
                asyncOperation.allowSceneActivation = false;
                Text2.SetActive(true);
                audioplayer.PlayOneShot(audio);
                isEnd = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LaunchGame();
            }
        }
        

    }

    void LaunchGame()
    {
        asyncOperation.allowSceneActivation = true;
    }
}
