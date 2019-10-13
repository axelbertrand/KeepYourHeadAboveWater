using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSliderHandlers : MonoBehaviour
{
    public List<AudioSource> musicSources;
    public List<AudioSource> effectSources;

    static private float lastMasterMultiplier = 1;
    static private float lastMusicMultiplier = 1;
    static private float lastEffectMultiplier = 1;

    public void UpdateMusicsVolume(float multiplier)
    {
        lastMusicMultiplier = multiplier;
        for (int i = 0; i< musicSources.Count; ++i)
        {
            musicSources[i].volume = 1 * lastMusicMultiplier * lastMasterMultiplier;
        }
    }

    public void UpdateMasterVolume(float multiplier)
    {
        lastMasterMultiplier = multiplier;
        for (int i = 0; i < musicSources.Count; ++i)
        {
            musicSources[i].volume = 1 * lastMusicMultiplier * lastMasterMultiplier;
        }

        for (int i = 0; i < effectSources.Count; ++i)
        {
            effectSources[i].volume = 1 * lastEffectMultiplier * lastMasterMultiplier;
        }
    }

    public void UpdateEffectsVolume(float multiplier)
    {
        lastEffectMultiplier = multiplier;
        for (int i = 0; i < effectSources.Count; ++i)
        {
            effectSources[i].volume = 1 * lastEffectMultiplier * lastMasterMultiplier;
        }
    }
}
