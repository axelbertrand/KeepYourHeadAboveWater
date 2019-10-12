using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    public enum ItemType {
        Jetpack,
        Boot,
        Tazer,
        Hand
    }
    public ItemType type;
    public bool locked = false;

    public void Use(PlayerController2 player){
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        locked = true;
        switch (type) {
            case ItemType.Boot:
                StartCoroutine(UseBoot(player));
                break;
            case ItemType.Tazer:
                break;
            case ItemType.Jetpack:
                StartCoroutine(UseJetpack(player));
                break;
            case ItemType.Hand:
                break;
        }
    }

    IEnumerator UseBoot(PlayerController2 player) {
        player.runSpeed *= 1.2f;
        yield return new WaitForSeconds(4);
        player.runSpeed /= 1.2f;
        player.item = null;
        Destroy(gameObject);
    }

    IEnumerator UseJetpack(PlayerController2 player) {
        player.gravity /= 1.4f;
        player.jumpHeight *= 1.4f;
        yield return new WaitForSeconds(1);
        player.gravity *= 1.4f;
        player.jumpHeight /= 1.4f;
        player.item = null;
        Destroy(gameObject);
    }
}
