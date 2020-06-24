using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield2 : MonoBehaviour {

    private GameObject p2;

	void Start () {

        p2 = GameObject.FindGameObjectWithTag("Player2");
	}
	void Update () 
    {
        transform.position = p2.transform.position;
	}
}
