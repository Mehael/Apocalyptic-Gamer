using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Tile
{
    public Sprite activeTrapState;
    public Sprite passiveTrapState;
    public int TurnsToBecomePassive = 0;

   override public bool StayOneMoreTurn()
   {
        if (tag == "Trap" && sprite.sprite == passiveTrapState)
        {
            tag = "Death";
            TurnsToBecomePassive = 2;
            sprite.sprite = activeTrapState;
            return true;
        }

        if (tag == "Death")
        {
            TurnsToBecomePassive--;
            if (TurnsToBecomePassive == 0)
            {
                tag = "Trap";
                sprite.sprite = passiveTrapState;
            }
            else return true;
        }

        return false;
    }
}
