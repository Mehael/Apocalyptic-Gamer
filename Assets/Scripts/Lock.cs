using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Tile {
    public Transform lockGo;

    void Start () {
        if (Board.current.KeysHere > WillOpenWhenXKeysStillOnLevel)
            lockGo.gameObject.SetActive(true);
        else
            lockGo.gameObject.SetActive(false);
    }

    public int WillOpenWhenXKeysStillOnLevel = 0;

    public override bool PressedSignUp(bool isPlayer = true)
    {
        if (!lockGo.gameObject.activeInHierarchy)
        {
            if (WillOpenWhenXKeysStillOnLevel == 0)
                HardDoorsCounter.HardDoors++;
            else
                HardDoorsCounter.EasyDoors++;
            Application.LoadLevel(Application.loadedLevel + 1);
        }
        return false;
    }

    public void KeysCollected(int KeysStillOnLevel)
    {
        if (KeysStillOnLevel <= WillOpenWhenXKeysStillOnLevel)
            lockGo.gameObject.SetActive(false);
    }
}
