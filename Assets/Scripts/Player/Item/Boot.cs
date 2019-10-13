using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : Item {

    public AudioSource source;
    public AudioClip clip;
    protected override IEnumerator UseItem(PlayerController2 player) {
        player.runSpeed *= 1.5f;

        GetComponentInChildren<ParticleSystem>().Play();

        yield return new WaitForSeconds(4);
        player.runSpeed /= 1.5f;

        source.volume = 0.7F;
        source.clip = clip;
        source.PlayOneShot(clip);
    }
}
