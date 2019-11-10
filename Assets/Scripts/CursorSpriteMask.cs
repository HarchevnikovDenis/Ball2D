using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSpriteMask : MonoBehaviour
{
    [SerializeField] private GameObject eyeSprite;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
        eyeSprite.transform.position = mousePosition;
    }
}
