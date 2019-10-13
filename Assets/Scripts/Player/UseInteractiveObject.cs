using Prime31;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseInteractiveObject : MonoBehaviour
{

    private PlayerController2 playerController2;
    private InteractiveObject interactiveObject;

    private CharacterController2D characterController2D;

    private Player playerInput; 

    // Start is called before the first frame update
    private void Start()
    {

        playerController2 = GetComponent<PlayerController2>();
        characterController2D = GetComponent<CharacterController2D>();

        characterController2D.onTriggerEnterEvent += onTriggerEnterEvent;
        characterController2D.onTriggerExitEvent += onTriggerExitEvent;

        playerInput = ReInput.players.GetPlayer(playerController2.playerInputId);
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerInput.GetButtonDown("InteractiveObject") && interactiveObject != null)
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