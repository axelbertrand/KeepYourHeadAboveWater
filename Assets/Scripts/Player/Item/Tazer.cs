using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tazer : Item {
    public Transform ray;

    public AudioSource source;
    public AudioClip clip;

    protected override IEnumerator UseItem(PlayerController2 player){
        ray.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        ray.gameObject.SetActive(false);

        source.volume = 0.7F;
        source.clip = clip;
        source.PlayOneShot(clip);

    }
}
