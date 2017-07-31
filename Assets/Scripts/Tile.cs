using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public GameObject LightPart;
    protected SpriteRenderer sprite;

    virtual public void GrayTint()
    {
        LightPart.SetActive(false);
    }

    virtual public void ColorTint()
    {
        LightPart.SetActive(true);
    }

    public virtual bool PressedSignUp(bool isPlayer = true)
    {
        if (tag == "Slime" && isPlayer)
            PlayerController.instance.BecomeSlimed(this);

        return false;
    }

    public bool SlimeStillLiveAfterThrow(Vector2 position)
    {
        if (!Board.current.cells.ContainsKey(position) || Board.current.cells[position].tag == "Death")
        {
            Die();
            return false;
        }

        return true;
    }

    public void Die()
    {
        tag = "Floor";
        gameObject.SetActive(false);
    }

    virtual public bool StayOneMoreTurn()
    {
        return false;
    }
}
