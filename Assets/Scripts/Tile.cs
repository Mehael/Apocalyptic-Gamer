using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    private Color startColor;
    private SpriteRenderer sprite;

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

    public bool PressedSignUp()
    {
        if (tag == "Weak")
            return true;

        return false;
    }

    public bool StayOneMoreTurn()
    {
        if (tag == "Weak")
        {
            tag = "Death";
            sprite.enabled = false;          
        }

        return false;
    }
}
