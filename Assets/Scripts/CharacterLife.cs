using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prime31;

public class CharacterLife : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hurstClip;


    private CharacterController2D _controller;

    [SerializeField]
    public float life_ = 50.0f;
    private int damage_ = 0;
    private float time_left_=1;
    public bool isInWater;

    public static float MAX_LIFE = 20.0f;
    public static float DAMAGE_PER_TICK = 1f;
    public static float TICK_INTERVAL = 1f;

    // Start is called before the first frame update
    void Awake()
    {
        _controller = GetComponent<CharacterController2D>();

        // listen to some events for illustration purposes
        _controller.onControllerCollidedEvent += onControllerCollider;
        _controller.onTriggerEnterEvent += onTriggerEnterEvent;
        _controller.onTriggerExitEvent += onTriggerExitEvent;

        life_ = MAX_LIFE;
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
                life_ -= DAMAGE_PER_TICK;

                if (life_ <= 0)
                {
                    Destroy(gameObject);
                }
                else
                {
                    audioSource.volume = 0.4F;
                    audioSource.clip = hurstClip;
                    audioSource.PlayOneShot(hurstClip);
                }
                time_left_ = TICK_INTERVAL;
            }
        }
    }


    //COLLIDER EVENTS
    public void onControllerCollider(RaycastHit2D hit)
    {
    }


    void onTriggerEnterEvent(Collider2D col)
    {
        if (col.CompareTag("Water"))
        {
            isInWater = true;
        }
        
    }


    void onTriggerExitEvent(Collider2D col)
    {
        if (col.CompareTag("Water"))
        {
            isInWater = false;
            time_left_ = 1;
        }
    }

}
