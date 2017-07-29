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
}
