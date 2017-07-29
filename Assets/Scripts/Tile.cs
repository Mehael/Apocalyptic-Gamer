﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    private Color startColor;
    protected SpriteRenderer sprite;

    void Awake () {
        sprite = GetComponent<SpriteRenderer>();
        startColor = sprite.color;
	}
	
    public void GrayTint()
    {
        sprite.color = Color.white;
    }

    public void ColorTint()
    {
        sprite.color = startColor;
    }

    public virtual bool PressedSignUp(bool isPlayer = true)
    {
        if (tag == "Slime" && isPlayer)
            PlayerController.instance.BecomeSlimed(this);

        if (tag == "Weak" || tag == "Trap")
            return true;

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
        sprite.enabled = false;
    }

    virtual public bool StayOneMoreTurn()
    {
        if (tag == "Weak")
        {
            tag = "Death";
            sprite.enabled = false;
        }

        return false;
    }
}
