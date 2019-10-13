using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour{

    public GameObject owner = null;

    public bool locked = false;
    public void Use(ItemPickUpBehavior player) {
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
        locked = true;
        StartCoroutine(UseItem1(player));
    }
    private IEnumerator UseItem1(ItemPickUpBehavior player) {
        yield return UseItem(player.GetComponent<PlayerController2>());

        locked = false;

        player.DestroyCurrentItem();
    }
    protected abstract IEnumerator UseItem(PlayerController2 player);

} 