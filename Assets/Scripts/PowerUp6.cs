using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp6 : MonoBehaviour {

    private Shield s;
    
    //void Start()
    //{
    //        s = GameObject.FindGameObjectWithTag("Shield").GetComponent<Shield>();
    //}

    void OnTriggerEnter2D(Collider2D c)
    {

        if (c.CompareTag("Player"))
        {
            Destroy(gameObject);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.BroadcastMessage("TemporaryShield");
            //s.GetComponent<AudioSource>().Play();
        }

        if (c.CompareTag("Player2"))
        {
            Destroy(gameObject);
            GameObject player2 = GameObject.FindGameObjectWithTag("Player2");
            player2.BroadcastMessage("TemporaryShield");
            //s.GetComponent<AudioSource>().Play();
        }
    }
}
