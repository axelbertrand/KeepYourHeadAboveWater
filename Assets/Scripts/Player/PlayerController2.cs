using Prime31;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{ 


    public int playerInputId = 0;

    // movement config
    public float gravity = -25f;
    public float runSpeed = 8f;
    public float groundDamping = 20f; // how fast do we change direction? higher means faster
    public float inAirDamping = 5f;
    public float jumpHeight = 3f;

    public int playerId;
    public Item item;

    [HideInInspector]
    private float normalizedHorizontalSpeed = 0;

    private CharacterController2D _controller;
    private Animator _animator;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 _velocity;

    private float defaultGravity;

    private PlayerState playerState;

    private bool isGravity = true;

    public enum PlayerState
    {
        Default,
        DontMove,
        Ladder,
        InWater,
        Hooked
    }


    public void SetPlayerState(PlayerState value)
    {

        switch (value)
        {
            case PlayerState.Default:
                isGravity = true;
                break;

            case PlayerState.DontMove:
                _velocity = Vector3.zero;
                break;

            case PlayerState.Hooked:
                isGravity = false;
                _velocity = Vector3.zero;
                break;
            case PlayerState.InWater:
                break;
            case PlayerState.Ladder:
                isGravity = false;
                _velocity = Vector3.zero;
                break;

        }


        playerState = value;
    }

    private Player playerInput;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();

        // listen to some events for illustration purposes
        _controller.onControllerCollidedEvent += onControllerCollider;
        _controller.onTriggerEnterEvent += onTriggerEnterEvent;
        _controller.onTriggerExitEvent += onTriggerExitEvent;

        playerInput = ReInput.players.GetPlayer(playerInputId);

        defaultGravity = gravity;
    }


    #region Event Listeners

    void onControllerCollider(RaycastHit2D hit)
    {
        
    }


    void onTriggerEnterEvent(Collider2D col)
    {
        Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
        Item i = col.GetComponent<Item>();
        if (i && (!item || !item.locked)) {
            if (item) {
                Destroy(item.gameObject);
            }
            item = i;
            col.transform.parent = transform;
            col.transform.localPosition = new Vector3(0, 0, -0.1f);
            col.transform.localScale = new Vector3(1, 1, 1);
            col.enabled = false;
        }
    }


    void onTriggerExitEvent(Collider2D col)
    {
        Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
    }

    #endregion

    // the Update loop contains a very simple example of moving the character around and controlling the animation
    void FixedUpdate()
    {
        if (_controller.isGrounded)
            _velocity.y = 0;

        switch (playerState)
        {
            case PlayerState.Default:
                ManageDefaultControl();
                break;

            case PlayerState.DontMove:
                break;

            case PlayerState.Hooked:
                break;
            case PlayerState.InWater:
                break;
            case PlayerState.Ladder:
                break;
                
        }

        if (isGravity)
        {
            // apply gravity before moving
            _velocity.y += gravity * Time.deltaTime;
        }

        _controller.move(_velocity * Time.deltaTime);

        // grab our current _velocity to use as a base for all calculations
        _velocity = _controller.velocity;


        if (playerInput.GetButtonDown("Item") && item)
            item.Use(this);
    }


    private void ManageDefaultControl()
    {
        float xAxis = playerInput.GetAxisRaw("Move");
        if (Mathf.Abs(xAxis) > 0.25)
        {
            if (xAxis > 0.25)
            {
                normalizedHorizontalSpeed = 1;
                if (transform.localScale.x < 0f)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                if (_controller.isGrounded)
                    _animator.Play(Animator.StringToHash("Run"));
            }
            else if (xAxis < 0.25)
            {
                normalizedHorizontalSpeed = -1;
                if (transform.localScale.x > 0f)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

                if (_controller.isGrounded)
                    _animator.Play(Animator.StringToHash("Run"));
            }
        }
        else
        {
            normalizedHorizontalSpeed = 0;

            if (_controller.isGrounded)
                _animator.Play(Animator.StringToHash("Idle"));
        }


        // we can only jump whilst grounded
        if (_controller.isGrounded && playerInput.GetButtonDown("Jump"))
        {
            _velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
            _animator.Play(Animator.StringToHash("Jump"));
        }


        // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
        var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
        _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);


        // if holding down bump up our movement amount and turn off one way platform detection for a frame.
        // this lets us jump down through one way platforms

        if (_controller.isGrounded && playerInput.GetAxisRaw("MoveY") < -0.5 && !playerInput.GetButtonDown("Jump"))
        {
            _velocity.y *= 3f;
            _controller.ignoreOneWayPlatformsThisFrame = true;
        }
    }
}
