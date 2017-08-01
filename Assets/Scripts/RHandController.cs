using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RHandController : MonoBehaviour {
    public static RHandController instance;
    Animator anim;

    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

	void Update () {
        if (Input.GetMouseButtonDown(1)|| Input.GetMouseButtonDown(0))
            anim.SetTrigger("Energy");
    }

    public void ButtaryReset()
    {
        anim.SetTrigger("Battary");
    }

    internal void Like()
    {
        anim.SetTrigger("Like");
    }

    
}
