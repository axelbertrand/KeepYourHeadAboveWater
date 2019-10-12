using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : Item{
    protected override IEnumerator UseItem(PlayerController2 player){
        player.gravity /= 3f;
        player.jumpHeight *= 3f;
        yield return new WaitForSeconds(2);
        player.gravity *= 3f;
        player.jumpHeight /= 3f;
    }
}
