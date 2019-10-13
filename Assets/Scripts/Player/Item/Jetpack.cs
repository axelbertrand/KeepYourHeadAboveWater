using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jetpack : Item{

    public AudioSource source;
    public AudioClip clip;
    protected override IEnumerator UseItem(PlayerController2 player){

    GetComponentInChildren<ParticleSystem>().Play();

        player.jumpHeight *= 5f;

        player.Jump();
        yield return new WaitForSeconds(2);
        player.jumpHeight /= 5f;

        source.volume = 0.7F;
        source.clip = clip;
        source.PlayOneShot(clip);
    }
}
