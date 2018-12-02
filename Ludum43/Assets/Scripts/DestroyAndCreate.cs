using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAndCreate : MonoBehaviour {
    public Vector3 teleportPoint1 = new Vector3(-4, 10, 0);
    public Vector3 teleportPoint2 = new Vector3(4, 10, 0);
    private GameObject enemy;
    public int plusScoreOnTeleport;
    public int minusScoreOnTeleport;
    public Rigidbody2D RbPlayer1;
    public Rigidbody2D RbPlayer2;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.position = teleportPoint1;
            enemy = GameObject.FindWithTag("Player1");
            enemy.GetComponent<PlayerStats>().score += plusScoreOnTeleport;
            //Debug.Log(enemy.GetComponent<PlayerStats>().score);
            enemy = GameObject.FindWithTag("Player");
            enemy.GetComponent<PlayerStats>().score -= minusScoreOnTeleport;
            enemy.transform.position = teleportPoint1;
        }
        if (collision.tag == "Player1")
        {
            collision.transform.position = teleportPoint2;
            enemy = GameObject.FindWithTag("Player");
            enemy.GetComponent<PlayerStats>().score += plusScoreOnTeleport;
            //Debug.Log(enemy.GetComponent<PlayerStats>().score);
            enemy = GameObject.FindWithTag("Player1");
            enemy.GetComponent<PlayerStats>().score -= minusScoreOnTeleport;
            enemy.transform.position = teleportPoint2;
        }
    }
}
