using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangCuser : MonoBehaviour
{
    [SerializeField] private Texture2D texture2D;

    private void Start()
    {
        Cursor.SetCursor(texture2D, Vector2.zero, CursorMode.ForceSoftware);
    }
}
