using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour{
    public bool locked = false;
    public void Use(PlayerController2 player) {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        locked = true;
        StartCoroutine(UseItem1(player));
    }
    private IEnumerator UseItem1(PlayerController2 player) {
        yield return UseItem(player);
        player.item = null;
        Destroy(gameObject);
    }
    protected abstract IEnumerator UseItem(PlayerController2 player);
} 