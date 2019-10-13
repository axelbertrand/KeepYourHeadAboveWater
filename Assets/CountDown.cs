using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour{

    public AudioSource audioSource;
    public AudioClip[] clips;

    private Text text;
    void Start(){
        text = GetComponent<Text>();
        StartCoroutine(countDown());
    }
    
    public IEnumerator countDown() {
        Time.timeScale = 0;
        for(int i = 3; i > 0; i--) {
            text.text = i.ToString();
            audioSource.volume = 0.5F;
            audioSource.clip = clips[i-1];
            audioSource.PlayOneShot(clips[i-1]);
            yield return new WaitForSecondsRealtime(1);
        }
        text.text = "Go";
        yield return new WaitForSecondsRealtime(1);
        text.text = "";
        Time.timeScale = 1;
    }
}
