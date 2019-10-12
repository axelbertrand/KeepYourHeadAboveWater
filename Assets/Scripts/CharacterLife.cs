using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class CharacterLife : MonoBehaviour
{
    private CharacterController2D _controller;

    [SerializeField]
    private float life_ = 50.0f;
    private int damage_ = 0;
    private float time_left_=1;
    public bool isInWater;

    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<CharacterController2D>();

        // listen to some events for illustration purposes
        _controller.onControllerCollidedEvent += onControllerCollider;
        _controller.onTriggerEnterEvent += onTriggerEnterEvent;
        _controller.onTriggerExitEvent += onTriggerExitEvent;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDamage();
    }

    public void CheckDamage()
    {
        time_left_ -= Time.deltaTime;
        if (isInWater)
        {
            if (time_left_ < 0)
            {
                life_ -= 1;
                Debug.Log("OUCH");
                if (life_ <= 0)
                {
                    Debug.Log("PLAYER IS DEAD");
                }
                time_left_ = 1;
            }
        }
    }


    //COLLIDER EVENTS
    public void onControllerCollider(RaycastHit2D hit)
    {
    }


    void onTriggerEnterEvent(Collider2D col)
    {
        isInWater = true;
    }


    void onTriggerExitEvent(Collider2D col)
    {
        isInWater = false;
        time_left_ = 1;
    }

}
