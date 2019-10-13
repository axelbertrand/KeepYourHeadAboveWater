using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField]
    private bool isOpen = false;

    private SpriteRenderer doorRenderer;
    private BoxCollider2D doorCollider;

    public Sprite[] sprites;

    private void Start()

    {
        doorRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>();
        SetOpened();
    }

    public void ChangeState()
    {
        isOpen = !isOpen;
        SetOpened();
    }

    public void SetOpened()
    {
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
}