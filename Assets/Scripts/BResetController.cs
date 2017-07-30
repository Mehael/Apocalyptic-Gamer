using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BResetController : MonoBehaviour {
    public static BResetController instance;
    Animator anim;

    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }


    private int nextLevel;
    public void PlayReset(int nextLEvel)
    {
        this.nextLevel = nextLEvel;
        anim.SetTrigger("Battary");
    }

    public void AnimEnded()
    {
        Application.LoadLevel(nextLevel);
    }
}
