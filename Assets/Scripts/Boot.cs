using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : Item {
    protected override IEnumerator UseItem(PlayerController2 player) {
        player.runSpeed *= 1.5f;
        yield return new WaitForSeconds(4);
        player.runSpeed /= 1.5f;
    }
}
