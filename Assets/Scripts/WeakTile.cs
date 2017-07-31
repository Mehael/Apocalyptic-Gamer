using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakTile : Tile {
    public GameObject visualPart;

    public override bool PressedSignUp(bool isPlayer = true)
    {
        return true;
    }

    public override bool StayOneMoreTurn()
    {
        tag = "Death";
        visualPart.SetActive(false);

        return false;
    }
}
