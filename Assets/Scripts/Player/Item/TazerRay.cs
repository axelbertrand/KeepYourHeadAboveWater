using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TazerRay : MonoBehaviour {
    
    void OnTriggerEnter2D(Collider2D collider){
        PlayerController2 player = collider.GetComponent<PlayerController2>();
        if (player)
            StartCoroutine(paralize(player));
    }

    IEnumerator paralize(PlayerController2 player) {
        player.runSpeed *= 0.1f;
        yield return new WaitForSeconds(1f);
        player.runSpeed /= 0.1f;
    }
}
