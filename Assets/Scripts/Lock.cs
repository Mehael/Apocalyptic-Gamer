using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : Tile {
    public Transform lockGo;
    public GameObject lockLight;

    void Start () {
        if (Board.current.KeysHere > WillOpenWhenXKeysStillOnLevel)
            lockGo.gameObject.SetActive(true);
        else
            lockGo.gameObject.SetActive(false);
    }

    public override void ColorTint()
    {
        lockLight.SetActive(true);
        base.ColorTint();
    }

    public override void GrayTint()
    {
        lockLight.SetActive(false);
        base.GrayTint();
    }

    public int WillOpenWhenXKeysStillOnLevel = 0;

    public override bool PressedSignUp(bool isPlayer = true)
    {
        if (!lockGo.gameObject.activeInHierarchy)
        {
            StartCoroutine(WaitToChangeLevel());
            AudioSystem.instance.PlayDoorOpen();
        }
        AudioSystem.instance.PlayDoorLocked();

        return false;
    }

    public IEnumerator WaitToChangeLevel()
    {
        if (WillOpenWhenXKeysStillOnLevel == 0)
            HardDoorsCounter.HardDoors++;
        else
            HardDoorsCounter.EasyDoors++;
        ConsoleMessage.instance.Show("Level " + (Application.loadedLevel + 1) + " Saved");
        if (Application.loadedLevel == 1)
            PlayerMessage.instance.Show("EASY");

        yield return new WaitForSeconds(2f);
        
        RHandController.instance.LoadLevel(Application.loadedLevel + 1);
    }

    public void KeysCollected(int KeysStillOnLevel)
    {
        if (KeysStillOnLevel <= WillOpenWhenXKeysStillOnLevel)
            lockGo.gameObject.SetActive(false);
    }
}
