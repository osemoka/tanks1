using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

    private GameObject p;

	void Start () {

        p = GameObject.FindGameObjectWithTag("Player");
	}
	void Update () 
    {
        transform.position = p.transform.position;
	}
}
