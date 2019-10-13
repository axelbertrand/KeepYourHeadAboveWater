using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField]
    private bool isOpen;

    private SpriteRenderer doorRenderer;
    private BoxCollider2D doorCollider;

    public Sprite[] sprites;

    private void Start()

    {
        doorRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>();
        if (isOpen)
        {
            doorCollider.enabled = false;
            doorRenderer.sprite = sprites[0];
        }
        else
        {
            doorCollider.enabled = true;
            doorRenderer.sprite = sprites[1];
        }
    }

    public void ChangeState()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            doorCollider.enabled = false;
            doorRenderer.color = Color.grey;
        }
        else
        {
            doorCollider.enabled = true;
            doorRenderer.color = Color.black;
        }
    }
}