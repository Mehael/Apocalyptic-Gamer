using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Board board;
    public Vector2 coords;

    void Start () {
        board = Board.current;
        MoveTo(board.enterPoint);
	}

    private void MoveTo(Vector2 enterPoint)
    {
        if (!board.cells.ContainsKey(enterPoint))
        {
            AudioSystem.instance.PlayFallToHole();
            Application.LoadLevel(Application.loadedLevel);
            return;
        }

        var nextTile = board.cells[enterPoint];
        Energy.instance.Move();

        if (nextTile.tag == "Unpassable")
        {
            AudioSystem.instance.PlayWallStuck();
            return;
        }


        transform.position = new Vector3(enterPoint.x + 0.5f, enterPoint.y + 0.5f, -1);
        coords = enterPoint;

        if (nextTile.tag == "Trap")
            AudioSystem.instance.PlayTrapStep();
        else
            AudioSystem.instance.PlayFloorStep();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            MoveTo(coords + Vector2.down);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            MoveTo(coords + Vector2.up);
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            MoveTo(coords + Vector2.left);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            MoveTo(coords + Vector2.right);
	}
}
