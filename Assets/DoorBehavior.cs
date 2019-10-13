using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour
{
    [SerializeField]
    private bool isOpen;

    private SpriteRenderer doorRenderer;
    private BoxCollider2D doorCollider;

    private void Start()

    {
        doorRenderer = GetComponent<SpriteRenderer>();
        doorCollider = GetComponent<BoxCollider2D>();
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