using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Tile
{
    public override bool PressedSignUp(bool isPlayer = true)
    {
        Board.current.GetKey();
        Die();
        return false;
    }
}
