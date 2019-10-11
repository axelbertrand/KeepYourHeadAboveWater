using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour {
    public int playerId = 0;
    private Player player;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private Collider2D col;
    public Transform dirtParticuleSystem;
    public Color hurtColor;
    public float runVelocity = .15f;
    public float runMaxVelocity = .5f;
    public float jumpForce = 2;
    public float helpForce = .1f;
    public float airControl = .5f;
    public int health = 5;

    private void Awake() {
        //Time.timeScale = 0.1f;
        //Setup inputs
        player = ReInput.players.GetPlayer(playerId);
    }

    void Start() {
        //Setup components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }

    private void FixedUpdate() {
        //Raycast to detect if the player is on the ground
        Vector2 pos = transform.position;
        int maskLayer = ~(1 << LayerMask.NameToLayer("Player"));
        Vector2 offset = col.bounds.extents.x * Vector2.right;
        Vector2 ray = pos + .1f * Vector2.up - offset;
        int rayCount = 0;
        for (int i = 0; i < 3; i++) {
            RaycastHit2D hit = Physics2D.Raycast(ray, -Vector3.up, .12f, maskLayer);
            if(hit.collider != null) {
                rayCount++;
                Debug.DrawRay(ray, -.12f * Vector2.up, Color.red);
            } else {
                Debug.DrawRay(ray, -.12f * Vector2.up, Color.blue);
            }
            ray += offset;
        }

        //Help the player to climb the plateform
        if (rayCount == 1) {
            rb.AddForce(helpForce*Vector2.up);
        }

        //Running physics
        float runDir = player.GetAxis("move");
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(animator.GetLayerIndex("Base Layer"));
        if (!info.IsName("attack")) {
            rb.velocity += runDir * runVelocity * (rayCount > 0 ? 1 : airControl) * Vector2.right;
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -runMaxVelocity, runMaxVelocity), rb.velocity.y);
            if (runDir != 0 && ((runDir > 0) != (transform.localScale.x > 0))) { //flipX
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1);
            }
        }

        //Landing dirt particules
        if (info.IsName("land") && rayCount == 3) {
            dirtParticuleSystem.position = transform.position;
            dirtParticuleSystem.gameObject.SetActive(false);
            dirtParticuleSystem.gameObject.SetActive(true);
        }

        //Send informations to the animator
        animator.SetBool("running", runDir != 0);
        animator.SetBool("onGround", rayCount > 0);
        animator.SetBool("falling", rb.velocity.y < -0.01f);

        if (player.GetButtonDown("attack"))
            Attack();
        if (player.GetButtonDown("jump"))
            Jump();
    }

    //Attack action
    void Attack() {
        animator.SetTrigger("attack");
    }

    //Jump action
    void Jump() {
        if (animator.GetBool("onGround")) {
            animator.SetTrigger("jump");
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }

    public void TakeDamages(int amount, Vector2 dir) {
        health -= amount;
        rb.velocity = Vector2.zero;
        rb.AddForce(dir.normalized.x*2f*Vector2.right + Vector2.up*1f);
        
    }
}
