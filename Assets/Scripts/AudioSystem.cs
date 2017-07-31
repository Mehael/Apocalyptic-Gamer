using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour {
    public static AudioSystem instance;
    public AudioSource WallStuck;
    public AudioSource FallToHole;
    public AudioSource TrapStep;
    public AudioSource FloorStep;

    AudioSource loop;
    private void Awake()
    {
        loop = GetComponent<AudioSource>();
        instance = this;
    }

    bool isActivated = false;
    public void Gray()
    {
        if (isActivated == false)
        {
            loop.Play();
            isActivated = true;
        }
        loop.volume = 0.08f;
    }

    public void Color()
    {
        loop.volume = 0.16f;
    }

    public void None()
    {
        loop.volume = 0f;
    }

    public void PlayWallStuck()
    {
        WallStuck.Play();
    }

    public void PlayFallToHole()
    {
        FallToHole.Play();
    }

    public void PlayTrapStep()
    {
        TrapStep.Play();
    }

    public void PlayFloorStep()
    {
        FloorStep.Play();
    }

    public AudioSource DoorLocked;
    public void PlayDoorLocked()
    {
        DoorLocked.Play();
    }

    public AudioSource DoorOpen;
    public void PlayDoorOpen()
    {
        DoorOpen.Play();
    }

    public AudioSource Key;
    public void PlayKey()
    {
        Key.Play();
    }

    public AudioSource Slime;
    public void PlaySlime()
    {
        Slime.Play();
    }

    public AudioSource SlimeDie;
    public void PlaySlimeDie()
    {
        SlimeDie.Play();
    }

    public AudioSource TrapDown;
    public void PlayTrapDown()
    {
        TrapDown.Play();
    }

    public AudioSource TrapUp;
    public void PlayTrapUp()
    {
        TrapUp.Play();
    }

    public AudioSource BLIND;
    public void PlayBLIND()
    {
        BLIND.Play();
        None();
    }

    public AudioSource COLOR;
    public void PlayCOLOR()
    {
        COLOR.Play();
        Color();
    }

    public AudioSource GRAY;
    public void PlayGRAY()
    {
        GRAY.Play();
        Gray();
    }

    public AudioSource WeakFL;
    public void PlayWeakFL()
    {
        WeakFL.Play();
    }

    public AudioSource WeakFall;
    public void PlayWeakFall()
    {
        WeakFall.Play();
    }
}
