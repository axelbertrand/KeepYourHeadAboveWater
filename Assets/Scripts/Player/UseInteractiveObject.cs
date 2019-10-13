using Prime31;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseInteractiveObject : MonoBehaviour
{
    private InteractiveObject interactiveObject;

    private CharacterController2D characterController2D;

    // Start is called before the first frame update
    private void Start()
    {
        characterController2D = GetComponent<CharacterController2D>();

        characterController2D.onTriggerEnterEvent += onTriggerEnterEvent;
        characterController2D.onTriggerExitEvent += onTriggerExitEvent;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && interactiveObject != null)
        {
            interactiveObject.UseObject();
        }
    }

    private void onTriggerEnterEvent(Collider2D col)
    {
        if (col.GetComponent<InteractiveObject>() != null)
        {
            interactiveObject = col.GetComponent<InteractiveObject>();
        }
    }

    private void onTriggerExitEvent(Collider2D col)
    {
        interactiveObject = null;
    }
}