using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Tile
{
    public GameObject activeTrapState;
    public GameObject passiveTrapState;
    public int TurnsToBecomePassive = 0;

   override public bool StayOneMoreTurn()
   {
        if (tag == "Trap")
        {
            AudioSystem.instance.PlayTrapUp();
            tag = "Death";
            TurnsToBecomePassive = 2;
            activeTrapState.SetActive(true);
            passiveTrapState.SetActive(false);
            return true;
        }

        if (tag == "Death")
        {
            TurnsToBecomePassive--;
            if (TurnsToBecomePassive == 0)
            {
                AudioSystem.instance.PlayTrapDown();
                tag = "Trap";
                activeTrapState.SetActive(false);
                passiveTrapState.SetActive(true);
            }
            else return true;
        }

        return false;
    }

    public override bool PressedSignUp(bool isPlayer = true)
    {
        if (tag == "Trap")
            return true;

        return false;
    }
}
