using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TileSetter : MonoBehaviour {
    void Update()
    {
        var coords = new Vector2(Mathf.Round(transform.position.x - 0.5f), Mathf.Round(transform.position.y - 0.5f));

        transform.localPosition = new Vector3(coords.x + 0.5f, coords.y + 0.5f, coords.y/10);
    }
}
