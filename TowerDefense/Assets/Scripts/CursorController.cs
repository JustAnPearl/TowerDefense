using UnityEngine;

public class CursorController : MonoBehaviour
{
    public Texture2D cursor;
    void Start()
    {
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
