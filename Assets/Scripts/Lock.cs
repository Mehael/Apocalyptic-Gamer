using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Tile {
    public Transform lockGo;

    void Start () {
        if (Board.current.KeysHere > 0)
            lockGo.gameObject.SetActive(true);
        else
            lockGo.gameObject.SetActive(false);
    }

    public void KeysCollected()
    {
        lockGo.gameObject.SetActive(false);
    }
}
