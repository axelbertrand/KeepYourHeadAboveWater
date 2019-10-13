using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SoundEffectsManager : MonoBehaviour
{
    public AudioClip onSelectClip;
    public AudioClip onClickClip;

    private Button button { get { return GetComponent<Button>(); } }
    private AudioSource source { get { return GetComponent<AudioSource>(); } }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<AudioSource>();
        gameObject.AddComponent<AudioSource>();

        source.playOnAwake = false;
    }

    public void PlaySound(int soundIndex)
    {

        if (soundIndex == 0)
        {
            source.clip = onSelectClip;
            source.PlayOneShot(onSelectClip);
        }
        else if (soundIndex == 1)
        {
            source.clip = onClickClip;
            source.PlayOneShot(onClickClip);
        }
    }
}
