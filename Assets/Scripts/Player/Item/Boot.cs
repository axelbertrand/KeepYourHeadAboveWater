using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boot : Item {

    public AudioSource source;
    public AudioClip clip;
    protected override IEnumerator UseItem(PlayerController2 player) {

        source.volume = 0.7F;
        source.clip = clip;
        source.PlayOneShot(clip);

        player.runSpeed *= 2f;

        GetComponentInChildren<ParticleSystem>().Play();

        yield return new WaitForSeconds(4);
        player.runSpeed /= 2f;
    }
}
