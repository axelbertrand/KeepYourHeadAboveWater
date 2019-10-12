using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSelectScript : MonoBehaviour, IDeselectHandler, ISelectHandler
{
    public SoundEffectsManager soundManager;

    public void OnDeselect(BaseEventData eventData)
    {
    }

    public void OnSelect(BaseEventData eventData)
    {
        soundManager.PlaySound(0);
    }
}
