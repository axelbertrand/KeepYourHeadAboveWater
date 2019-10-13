using Prime31;
using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{

    public int gamePlayerId = 0;
    // sounds
    private AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip hookedClip;
    public AudioClip waterClip;


    public int playerInputId = 0;


    // movement config
    public float gravity = -25f;
    public float runSpeed = 8f;
    public float groundDamping = 20f; // how fast do we change direction? higher means faster
    public float inAirDamping = 5f;
    public float jumpHeight = 3f;
    public float floatingDelta = 1.0f;

    public LayerMask waterMask;
    public GameObject HighPoint;
    public GameObject DownPoint;

    [HideInInspector]
    private float normalizedHorizontalSpeed = 0;

    private CharacterController2D _controller;
    private Animator _animator;
    private RaycastHit2D _lastControllerColliderHit;
    private Vector3 _velocity;
    private CharacterLife characterLife_;

    private float defaultGravity;

    private PlayerState playerState;

    private bool isGravity = true;

    public enum PlayerState
    {
        Default,
        DontMove,
        Ladder,
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
                isGravity = false;
                _velocity = Vector3.zero;
                break;

            case PlayerState.Hooked:
                audioSource.volume = 0.3F;
                audioSource.clip = hookedClip;
                audioSource.PlayOneShot(hookedClip);
                isGravity = false;
                _velocity = Vector3.zero;
                break;
            case PlayerState.Ladder:
                isGravity = false;
                _velocity = Vector3.zero;
                break;

        }


        playerState = value;
    }

    public PlayerState GetPlayerState()
    {
        return playerState;
    }

    private Player playerInput;

    private bool _isUpInWater;
    private bool _isDownInWater;

    void Awake()
    {

    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _controller = GetComponent<CharacterController2D>();

        // listen to some events for illustration purposes
        _controller.onControllerCollidedEvent += onControllerCollider;
        _controller.onTriggerEnterEvent += onTriggerEnterEvent;
        _controller.onTriggerExitEvent += onTriggerExitEvent;

        playerInput = ReInput.players.GetPlayer(playerInputId);
        defaultGravity = gravity;

        characterLife_ = GetComponent<CharacterLife>();

        audioSource = GetComponent<AudioSource>();
    }


    #region Event Listeners

    void onControllerCollider(RaycastHit2D hit)
    {

    }


    void onTriggerEnterEvent(Collider2D col)
    {
        Debug.Log("onTriggerEnterEvent: " + col.gameObject.name);
        if(col.gameObject.name == "ray") {
            StartCoroutine(paralize());
        }
    }

    IEnumerator paralize()
    {
        runSpeed *= 0.1f;
        yield return new WaitForSeconds(2f);
        runSpeed /= 0.1f;
    }


    void onTriggerExitEvent(Collider2D col)
    {
        //Debug.Log("onTriggerExitEvent: " + col.gameObject.name);
    }

    #endregion

    // the Update loop contains a very simple example of moving the character around and controlling the animation
    public void Update()
    {
        //Checking if player is partially in water or not
        _isUpInWater = (Physics2D.OverlapCircle(HighPoint.transform.position, 0.25f, waterMask) != null);

        if (Physics2D.OverlapCircle(DownPoint.transform.position, 0.25f, waterMask) == null)
        {
            _isDownInWater = false;
        }
        else
        {
            if(!_isDownInWater && _velocity.magnitude > 10)
            {
                audioSource.volume = 0.3F;
                audioSource.clip = waterClip;
                audioSource.PlayOneShot(waterClip);
            }

            _isDownInWater = true;
        }

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
            case PlayerState.Ladder:
                break;

        }

        if (isGravity)
        {
            // apply gravity before moving
            if (_isDownInWater)
            {
                if (_velocity.y < 0)
                {
                    _velocity.y = _velocity.y * 0.95f;
                }
                if (_isUpInWater)
                {
                    _velocity.y += floatingDelta;
                    //Debug.Log("GOING UP");
                }
            }
            else
            {
                _velocity.y += gravity * Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        _controller.move(_velocity * Time.deltaTime);

        // grab our current _velocity to use as a base for all calculations
        _velocity = _controller.velocity;

        UpdateAnimator();
    }


    public void Jump()
    {
        _velocity.y = Mathf.Sqrt(2f * jumpHeight * -gravity);
        _animator.SetTrigger("isJumping");

        audioSource.volume = 0.3F;
        audioSource.clip = jumpClip;
        audioSource.PlayOneShot(jumpClip);
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

            }
            else if (xAxis < 0.25)
            {
                normalizedHorizontalSpeed = -1;
                if (transform.localScale.x > 0f)
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

            }
        }
        else
        {
            normalizedHorizontalSpeed = 0;
        }


        // we can only jump whilst grounded
        if ((_controller.isGrounded || (_isDownInWater && !_isUpInWater)) && playerInput.GetButtonDown("Jump"))
        {
            Jump();
        }


        // apply horizontal speed smoothing it. dont really do this with Lerp. Use SmoothDamp or something that provides more control
        var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
        _velocity.x = Mathf.Lerp(_velocity.x, normalizedHorizontalSpeed * runSpeed, Time.deltaTime * smoothedMovementFactor);



        // if holding down bump up our movement amount and turn off one way platform detection for a frame.
        // this lets us jump down through one way platforms

        if (_controller.isGrounded && playerInput.GetAxisRaw("MoveY") < -0.5 && !playerInput.GetButtonDown("Jump"))
        {
            _velocity.y -= 3f;
            _controller.ignoreOneWayPlatformsThisFrame = true;
        }


    }

    private void UpdateAnimator()
    {
        _animator.SetBool("isGrounded", _controller.isGrounded);
        _animator.SetBool("isWalking", _controller.isGrounded && _velocity.magnitude > 0.35);
        _animator.SetBool("isInWater", _isUpInWater || _isDownInWater);
        _animator.SetBool("isAlive", characterLife_.life_ > 0);
        _animator.SetBool("isHooked", playerState == PlayerState.Hooked);

    }
}
