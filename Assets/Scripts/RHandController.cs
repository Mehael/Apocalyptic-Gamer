using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RHandController : MonoBehaviour {
    public static RHandController instance;

    private void Awake()
    {
        instance = this;
    }

    public void ButtaryReset()
    {
    }

    internal void Like()
    {
    }

    public void ResTime()
    {
        Application.LoadLevel(nextLevel);
    }

    private int nextLevel;
    public void LoadLevel(int levelIndex)
    {
        nextLevel = levelIndex;
        Application.LoadLevel(levelIndex);
    }
}
