using Prime31;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUpBehavior : MonoBehaviour
{

    private Item pickUpObject;

    private PlayerController2 playerController;
    private CharacterController2D characterController;

    private Player playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController2>();
        characterController = GetComponent<CharacterController2D>();

        playerInput = ReInput.players.GetPlayer(playerController.playerInputId);

        characterController.onTriggerEnterEvent += onTriggerEnterEvent;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerController.GetPlayerState() != PlayerController2.PlayerState.Default)
        {
            return;
        }

        if (playerInput.GetButtonDown("Item"))
        {
            UseItem();
        }

    }

    void onTriggerEnterEvent(Collider2D col)
    {
        if (playerController.GetPlayerState() != PlayerController2.PlayerState.Default)
        {
            return;
        }

        Item itemToBePicked = col.GetComponent<Item>();
        if (itemToBePicked != null && itemToBePicked.owner == null)
        {

            if (DestroyCurrentItem())
            {
                col.enabled = false;

                pickUpObject = itemToBePicked;
                col.transform.parent = transform;
                col.transform.localPosition = new Vector3(0, 0, -0.1f);
                col.transform.localScale = new Vector3(1, 1, 1);

                itemToBePicked.owner = gameObject;
            }
        }
    }


    public bool DestroyCurrentItem()
    {

        if (pickUpObject != null && pickUpObject.locked == false)
        {
            Destroy(pickUpObject.gameObject);
            pickUpObject = null;

            return true;
        }else if (pickUpObject == null)
        {
            return true;
        }

        return false;
    }

    public void UseItem()
    {
        Debug.Log("try to pick up");

        if (pickUpObject != null)
        {
            pickUpObject.Use(this);
        }
    }

}
