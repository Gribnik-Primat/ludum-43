using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartGame : MonoBehaviour {

    public GameObject Tutorial;
    public GameObject Score;
    public GameObject Score1;
    public GameObject Time;
    public GameObject Player;
    public GameObject Player1;
    public GameObject start;
    public DisplayScore Ds;
    public AudioSource audiostartplayer;
    public AudioClip audiostart;

    // Use this for initialization
    void Start () {
        StartCoroutine(Wait());
       
    }

    // Update is called once per frame
    void Update() {
        
    }
    IEnumerator Wait()
    {
        Ds.enabled = false;
        Score.SetActive(false);
        Score1.SetActive(false);
        Time.SetActive(false);
        Player.SetActive(false);
        Player1.SetActive(false);

        Tutorial.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        Tutorial.SetActive(false);

        start.SetActive(true);
        Text t = start.GetComponent<Text>();
        t.text = "3!";
        yield return new WaitForSecondsRealtime(1);
        t.text = "2!!";
        yield return new WaitForSecondsRealtime(1);
        t.text = "1!!!";
        yield return new WaitForSecondsRealtime(1);
        t.text = "GO!!!";
        yield return new WaitForSecondsRealtime(1);
        audiostartplayer.PlayOneShot(audiostart);
        Score.SetActive(true);
        Score1.SetActive(true);
        Time.SetActive(true);
        Player.SetActive(true);
        Player1.SetActive(true);
        Ds.enabled = true;
        start.SetActive(false);
    }
}
