using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.name == "Player") {
            col.gameObject.GetComponent<PlayerController>().TakeDamages(1, -col.relativeVelocity);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.name == "ToolCollider") {
            Destroy(gameObject);
        }
    }
}