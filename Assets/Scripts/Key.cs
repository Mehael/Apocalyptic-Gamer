using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Tile
{
    public static bool isFirst = true;
    public override bool PressedSignUp(bool isPlayer = true)
    {
        if (isFirst == true)
        {
            PlayerMessage.instance.Show("This key must be from the door");
            isFirst = false;
        }
        Board.current.GetKey();
        Die();
        return false;
    }
}
