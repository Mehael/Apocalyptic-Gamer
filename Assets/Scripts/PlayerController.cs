using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Board board;
    public Vector2 coords;
    public static PlayerController instance;

    private void Awake()
    {
        instance = this;
    }

    private int SlimedHPCounter = 0;
    private Tile Slime;
    public int SlimedHP = 2;
    public void BecomeSlimed(Tile slime)
    {
        SlimedHPCounter = SlimedHP;
        Slime = slime;
    }

    void Start () {
        board = Board.current;
        MoveTo(board.enterPoint);
	}

    private void MoveTo(Vector2 enterPoint)
    {
        Energy.instance.Move();

        if (SlimedHPCounter > 0)
        {
            SlimedHPCounter--;
            Instantiate<GameObject>(Slime.gameObject,
                new Vector3(enterPoint.x + 0.5f, enterPoint.y + 0.5f, Slime.transform.position.z),
                Quaternion.identity).AddComponent<DieAfterSecond>();

            if (SlimedHPCounter < 1 || !Slime.SlimeStillLiveAfterThrow(enterPoint))
            {
                Slime.Die();
                SlimedHPCounter = 0;
            }
            else
            {
                Board.current.TurnDone(enterPoint, false);
            }

            return;
        }

        Board.current.TurnDone(enterPoint);

        if (!board.cells.ContainsKey(enterPoint))
        {
            StartCoroutine(Fall());
            MoveHeroSprite(enterPoint);
            return;
        }

        var nextTile = board.cells[enterPoint];

        if (nextTile.tag == "Death")
            StartCoroutine(Fall(true));

        if (nextTile.tag == "Unpassable")
        {
            if (board.cells[coords].tag == "Death")
                StartCoroutine(Fall());
            else
                AudioSystem.instance.PlayWallStuck();
            return;
        }

        coords = enterPoint;
        MoveHeroSprite(enterPoint);
        
        if (nextTile.tag == "Trap")
            AudioSystem.instance.PlayTrapStep();
        else
            AudioSystem.instance.PlayFloorStep();
    }

    private void MoveHeroSprite(Vector2 enterPoint)
    {
        transform.position = new Vector3(enterPoint.x + 0.5f, enterPoint.y + 0.5f, (enterPoint.y / 10) - 0.09f);
    }

    private IEnumerator Fall(bool isTrapDie = false)
    {
        if (isTrapDie)
            ConsoleMessage.instance.Show("Trap kill you");
        else
            ConsoleMessage.instance.Show("You fall to hole");

        AudioSystem.instance.PlayFallToHole();

        yield return new WaitForSeconds(0.5f);
        RHandController.instance.LoadLevel(Application.loadedLevel);
    }

    void Update () {
        if (Energy.instance.currentEnergy == 0) return;

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
