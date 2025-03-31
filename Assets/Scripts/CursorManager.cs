using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    Texture2D cursorTexture;
    [SerializeField]
    Texture2D targetTexture;
    [SerializeField]
    Texture2D noCoursor;
    public void SetNoCursorTexture(Vector2 hotspot)
    {
        Cursor.SetCursor(noCoursor, hotspot, CursorMode.Auto);
    }
    public void SetCursorTexture(Vector2 hotspot)
    {
        Cursor.SetCursor(cursorTexture, hotspot, CursorMode.Auto);
    }
    public void SetTargetTexture(Vector2 hotspot)
    {
        Cursor.SetCursor(targetTexture, hotspot, CursorMode.Auto);
    }
}
