using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Board board;
    public Vector2 coords;
    public static PlayerController instance;
    public GameObject LightCharacter;

    private void Awake()
    {
        instance = this;
    }

    private int SlimedHPCounter = 0;
    private Tile Slime;
    public int SlimedHP = 2;

    List<String> slimedMEssgaes = new List<string>()
    {
        "Oh great! My character is covered in slime.",
        "Oh, I don't want to touch it!",
        "Taste my kick!",
    };

    List<String> trapMEssgaes = new List<string>()
    {
        "Ouch, thay are sharp",
        "Every game about the dungeon has spikes. VERY smart.",
        "I assume, It's a bad idea to step on it",
    };


    bool didShowSlimeMessage = false;
    public void BecomeSlimed(Tile slime)
    {
        if (Application.loadedLevel == 5 && !didShowSlimeMessage){
            PlayerMessage.instance.Show("Dirty Slime!\nGo the hole.");
            didShowSlimeMessage = true;        
        }
        else
            PlayerMessage.instance.Show(slimedMEssgaes);

        AudioSystem.instance.PlaySlime();
        SlimedHPCounter = SlimedHP;
        Slime = slime;
    }

    void Start () {
        InputSystem.Instance.ButtonPressed += HandleButtonPress;
        board = Board.current;
        MoveTo(board.enterPoint, true);
    }

    public void Color()
    {
        LightCharacter.SetActive(true);
    }

    public void Gray()
    {
        LightCharacter.SetActive(false);
    }

    private void MoveTo(Vector2 enterPoint, bool isInit = false)
    {
        if (isInit == false) Energy.instance.Move();

        if (SlimedHPCounter > 0)
        {
            SlimedHPCounter--;
            Instantiate<GameObject>(Slime.gameObject,
                new Vector3(enterPoint.x + 0.5f, enterPoint.y + 0.5f, Slime.transform.position.z),
                Quaternion.identity, Slime.transform.parent).AddComponent<DieAfterSecond>();

            Slime.HideForSecond();

            if (SlimedHPCounter < 1 || !Slime.SlimeStillLiveAfterThrow(enterPoint))
            {
                AudioSystem.instance.PlaySlimeDie();
                Slime.Die();
                SlimedHPCounter = 0;
            }
            else
            {
                AudioSystem.instance.PlaySlime();
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
            PlayerMessage.instance.Show(WallMEssages);
            if (board.cells[coords].tag == "Death")
                StartCoroutine(Fall());
            else
                AudioSystem.instance.PlayWallStuck();
            return;
        }

        coords = enterPoint;
        MoveHeroSprite(enterPoint);

        if (nextTile.tag == "Trap")
        {
            AudioSystem.instance.PlayTrapStep();
            PlayerMessage.instance.Show(trapMEssgaes);
        }
        else if (nextTile.tag == "Exit")
            return;
        else if (nextTile.tag == "Key")
            AudioSystem.instance.PlayKey();
        else if (nextTile.tag == "Weak")
            AudioSystem.instance.PlayWeakFL();
        else
            AudioSystem.instance.PlayFloorStep();


    }

    private void MoveHeroSprite(Vector2 enterPoint)
    {
        transform.position = new Vector3(enterPoint.x + 0.5f, enterPoint.y + 0.5f, (enterPoint.y / 10) - 0.08f);
    }

    List<String> WallMEssages = new List<string>()
    {
        "Stop hit this wall",
        "You won't break it",
        "Ouch",
    };

    List<String> DeatchMEssages = new List<string>()
    {
        "Who made this game? I'm sweating my ass off!",
        "Not Again!",
        "Are you kidding me?",
        "No Way.",
        "WHYYYY?",
        "Well, It's not even frustrating anymore.",
        "Well Done, I'm DEAD.",
        "I didn't see that coming.",
    };

    private IEnumerator Fall(bool isTrapDie = false)
    {
        if (isTrapDie)
            ConsoleMessage.instance.Show("Trap killed you");
        else
            ConsoleMessage.instance.Show("You fall to hole");

        AudioSystem.instance.PlayFallToHole();
        PlayerMessage.instance.Show(DeatchMEssages);

        yield return new WaitForSeconds(1.5f);
        BResetController.instance.PlayReset(Application.loadedLevel);
    }

    private void OnDestroy()
    {
        InputSystem.Instance.ButtonPressed -= HandleButtonPress;
    }

    private void HandleButtonPress(InputButton button)
    {
        if (Energy.instance.currentEnergy == 0) return;

        if (button == InputButton.Down)
            MoveTo(coords + Vector2.down);
        if (button == InputButton.Up)
            MoveTo(coords + Vector2.up);
        if (button == InputButton.Left)
            MoveTo(coords + Vector2.left);
        if (button == InputButton.Right)
            MoveTo(coords + Vector2.right);
    }

    void Update () {
        return;
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
