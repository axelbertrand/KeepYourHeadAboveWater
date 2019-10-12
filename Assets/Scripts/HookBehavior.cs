using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HookBehavior : MonoBehaviour
{

    
    private Rigidbody2D myRigidBody2D;

    private float rodLength;
    private FishingRod playerGameObject;

    private Sequence backSequence = null;

    private bool isHookAnchored = false;
    
    public bool IsHookAnchored
    {
        get
        {
            return isHookAnchored;
        }
    }

    private void Awake()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(playerGameObject!= null)
        {
            Debug.Log(Vector3.Distance(transform.position, playerGameObject.transform.position));
        }


        if (playerGameObject != null && Vector3.Distance(transform.position, playerGameObject.transform.position) > rodLength)
        {
            GoBackToPlayer();
        } else if (playerGameObject != null && Vector3.Distance(transform.position, playerGameObject.transform.position) < 0.5)
        {
            playerGameObject.HookDestroyed();
            Destroy(gameObject);
        }
    }

    public void LaunchHook(Vector3 direction, float speed, FishingRod player, float maxLength)
    {
        myRigidBody2D.AddForce(direction * speed, ForceMode2D.Impulse);
        playerGameObject = player;

        rodLength = maxLength;
    }

    public void GoBackToPlayer()
    {
        // If the hook is too far, we go back to the player
        if(myRigidBody2D != null)
        {
            Destroy(myRigidBody2D);
        }

        Destroy(GetComponent<Collider2D>());

        if(backSequence == null)
        {
            backSequence = DOTween.Sequence();
            backSequence.Append(transform.DOJump(playerGameObject.transform.position, 0.25f, 1, 1.25f));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myRigidBody2D != null)
        {
            Destroy(myRigidBody2D);
        }

        isHookAnchored = true;
    }
}
