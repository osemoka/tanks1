using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade1 : MonoBehaviour {

    private Manager m;
    private Animator animator;
    public int i =1;
    
	// Use this for initialization
	void Start () 
    {
        i = 1;
        m = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>(); 
        animator = GetComponent<Animator>();
	}

    void Update()
    {//moze w starcie by pyklo?
        if (m.score == 10)
        {
            i = 3;
        }
        else if (m.score == 39)
        {
            i = 4;
        }
        else if (m.score == 84)
        {
            i = 5;
        }
        else if (m.score == 188)
        {
            i = 6;
        }
        else if (m.score == 262)
        {
            i = 7;
        }
    }

    public void fadeOn()
    {
        animator.SetBool("fade", true);
        StartCoroutine(PlayAnim());
    }



    IEnumerator PlayAnim()
    {
        yield return StartCoroutine(WaitForSeconds(1F));
        if (Application.loadedLevel != 2 && Application.loadedLevel != 0)
            Application.LoadLevel(2);
        else
            Application.LoadLevel(i);
    }


    IEnumerator WaitForSeconds(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
}
