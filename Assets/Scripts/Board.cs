using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {
    public Dictionary<Vector2,Tile> cells;
    public Dictionary<Vector2, Tile> items;

    public Transform overBoardNode;

    public int StartEnergyForLevel = 100;
    public Vector2 enterPoint;
    public List<Lock> exitPoints;

    public static Board current;
    private List<Tile> waitingTurnTiles = new List<Tile>();

    public void TurnDone(Vector2 PlayerCoords, bool isPlayerMove = true)
    {
        var newWaitingList = new List<Tile>();
        foreach (var cell in waitingTurnTiles)
            if (cell.StayOneMoreTurn())
                newWaitingList.Add(cell);

        if (cells.ContainsKey(PlayerCoords) && cells[PlayerCoords].PressedSignUp(isPlayerMove))
            newWaitingList.Add(cells[PlayerCoords]);

        if (items.ContainsKey(PlayerCoords) && items[PlayerCoords].PressedSignUp(isPlayerMove))
            newWaitingList.Add(items[PlayerCoords]);

        waitingTurnTiles = newWaitingList;
    }

	void Awake () {
        current = this;
        cells = ParseNodeToTiles(transform);
        items = ParseNodeToTiles(overBoardNode);

        Energy.instance.Start();
        Screen.instance.Start();
        PlayerMessage.instance.Hide();
    }

    private void Start()
    {
        ConsoleMessage.instance.Show("Level " + Application.loadedLevel + " Loaded");
        if (Application.loadedLevel == 1)
            PlayerMessage.instance.Show("YES, It works with new batteries!");
    }

    public int KeysHere = 0;

    public void GetKey()
    {
        KeysHere--;
        foreach(var door in exitPoints)
            door.KeysCollected(KeysHere);
    }

    Dictionary<Vector2,Tile> ParseNodeToTiles (Transform Node)
    {
        var TilesList = new Dictionary<Vector2, Tile>();

        for (int i = 0; i < Node.childCount; i++)
        {
            var child = Node.GetChild(i);
            var coords = new Vector2(Mathf.Round(child.transform.position.x - 0.5f),
                Mathf.Round(child.transform.position.y - 0.5f));

            child.transform.localPosition = new Vector2(coords.x + 0.5f, coords.y + 0.5f);
            TilesList.Add(coords, child.GetComponent<Tile>());

            if (child.tag == "Enter") enterPoint = coords;
            else if (child.tag == "Exit") exitPoints.Add(child.GetComponent<Lock>());
            else if (child.tag == "Key") KeysHere++;
        }

        return TilesList;
    }
}
