using Prime31;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController2))]
public class FishingRod : MonoBehaviour
{

    [SerializeField]
    public GameObject hookPosition;

    [SerializeField]
    private LayerMask whatIsGround;

    [SerializeField]
    private float cableLength = 15f;

    [SerializeField]
    private float cableSpeed = 4f;

    [SerializeField]
    private GameObject hookPrefab;


    private HookBehavior hook;

    private PlayerController2 playerController;
    private CharacterController2D characterController;

    private Player playerInput;

    // Start is called before the first frame update
    void Start()
    {
       playerController = GetComponent<PlayerController2>();
       characterController = GetComponent<CharacterController2D>();

       playerInput = ReInput.players.GetPlayer(playerController.playerInputId);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.GetButtonDown("FishingRod"))
        {
            LaunchFishingRod();
        }
    }

    private void LaunchFishingRod()
    {
        if(hook == null && characterController.isGrounded && Physics2D.OverlapCircle(hookPosition.transform.position, 0.25f, whatIsGround) == null)
        {
            playerController.SetPlayerState(PlayerController2.PlayerState.DontMove);

            GameObject newHook = Instantiate(hookPrefab, hookPosition.transform.position, new Quaternion());
            hook = newHook.GetComponent<HookBehavior>();

            hook.LaunchHook(this, cableLength, cableSpeed);
        }
    }

    public void CancelFishingRod(GameObject hookedItem = null)
    {

        playerController.SetPlayerState(PlayerController2.PlayerState.Default);

        if (hookedItem != null)
        {
            hookedItem.transform.SetParent(null, true);

            PlayerController2 hookedPlayer = hookedItem.GetComponent<PlayerController2>();
            if (hookedPlayer != null)
            {
                hookedPlayer.SetPlayerState(PlayerController2.PlayerState.Default);
            }
        }

        if (hook != null)
        {
            Destroy(hook.gameObject);
            hook = null;
        }
    }
}
