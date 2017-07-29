using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public Dictionary<Vector2,Tile> cells = new Dictionary<Vector2, Tile>();

    public int StartEnergyForLevel = 100;
    public Vector2 enterPoint;
    public Vector2 exitPoint;

    public static Board current;

	void Awake () {
        current = this;

        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var coords = new Vector2(Mathf.Round(child.transform.position.x - 0.5f),
                Mathf.Round(child.transform.position.y - 0.5f));

            child.transform.position = new Vector2(coords.x + 0.5f, coords.y + 0.5f);
            cells.Add(coords, child.GetComponent<Tile>());

            if (child.tag == "Enter") enterPoint = coords;
            if (child.tag == "Exit") exitPoint = coords;
        }
    }
}
