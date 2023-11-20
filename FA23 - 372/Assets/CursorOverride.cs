using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorOverride : MonoBehaviour
{
    private SpriteRenderer cursorSpriteRenderer;
    [SerializeField] private Sprite newCursor;
    [SerializeField] private Sprite clickedCursor;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        cursorSpriteRenderer = GetComponent<SpriteRenderer>();
        cursorSpriteRenderer.sprite = newCursor;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorSpriteRenderer.transform.position = pos;
        if (Input.GetMouseButtonDown(0))
        {
            cursorSpriteRenderer.sprite = clickedCursor;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            cursorSpriteRenderer.sprite = newCursor;
        }
    }
}
