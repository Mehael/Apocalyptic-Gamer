using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour {
    public static AudioSystem instance;
    public AudioSource WallStuck;
    public AudioSource FallToHole;
    public AudioSource TrapStep;
    public AudioSource FloorStep;

    private void Awake()
    {
        instance = this;
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
}
