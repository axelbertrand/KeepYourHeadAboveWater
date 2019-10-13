using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour{
    private Text text;
    void Start(){
        text = GetComponent<Text>();
        StartCoroutine(countDown());
    }
    
    public IEnumerator countDown() {
        Time.timeScale = 0;
        for(int i = 3; i > 0; i--) {
            text.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        text.text = "Go";
        yield return new WaitForSecondsRealtime(1);
        text.text = "";
        Time.timeScale = 1;
    }
}
