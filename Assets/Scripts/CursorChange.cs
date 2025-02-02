using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorChange : MonoBehaviour
{
    [SerializeField] private Texture2D hoverCursor;
    [SerializeField] private Texture2D defaultCursor;

    private void Start()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseEnter()
    {
        if (hoverCursor != null)
        {
            Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
        }
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseUp()
    {
        Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseDown()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
