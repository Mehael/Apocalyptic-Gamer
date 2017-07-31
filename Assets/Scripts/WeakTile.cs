using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakTile : Tile {
    public GameObject visualPart;
    public GameObject veryWeak;

    public override bool PressedSignUp(bool isPlayer = true)
    {
        veryWeak.SetActive(true);
        return true;
    }

    public override bool StayOneMoreTurn()
    {
        AudioSystem.instance.PlayWeakFall();
        tag = "Death";
        visualPart.SetActive(false);

        return false;
    }
}
