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

    public static bool isFirst = false;
    public override bool PressedSignUp(bool isPlayer = true)
    {

        if (!lockGo.gameObject.activeInHierarchy)
        {
            StartCoroutine(WaitToChangeLevel());
            AudioSystem.instance.PlayDoorOpen();
        }
        else
        {
            if (isFirst == true)
            {
                PlayerMessage.instance.Show("This key must be from the door");
                isFirst = false;
            }
            AudioSystem.instance.PlayDoorLocked();
        }
        return false;
    }

    public IEnumerator WaitToChangeLevel()
    {
        
        HardDoorsCounter.KeysAtAll += Board.current.KeysHere + KeysCOllected;
        HardDoorsCounter.KeysCollected += KeysCOllected;

        ConsoleMessage.instance.Show("Level " + (Application.loadedLevel + 1) + " Saved");
        if (Application.loadedLevel == 1)
            PlayerMessage.instance.Show("EASY");

        yield return new WaitForSeconds(2f);
        
        BResetController.instance.PlayReset(Application.loadedLevel + 1);
    }

    int KeysCOllected = 0;
    public void KeysCollected(int KeysStillOnLevel)
    {
        KeysCOllected++;
        if (KeysStillOnLevel <= WillOpenWhenXKeysStillOnLevel)
            lockGo.gameObject.SetActive(false);
    }
}
