using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour {
    public List<float> players; //Values between 0 and 1
    public List<Texture> images;
    public RectTransform layout, defaultPlayer;

    void Update() {
        while (players.Count > layout.childCount) {
            RectTransform player = Instantiate(defaultPlayer);
            player.parent = layout;
            player.localPosition = new Vector3(0, 0, 0);
            player.localScale = new Vector3(1, 1, 1);
            player.gameObject.SetActive(true);
            player.Find("Image").GetComponent<RawImage>().texture = images[layout.childCount%images.Count];
        }
        while (players.Count < layout.childCount) {
            DestroyImmediate(layout.GetChild(0));
        }
        for(int i = 0; i < layout.childCount; i++) {
            Transform mask = layout.GetChild(i).Find("Mask");
            ((RectTransform)mask).anchoredPosition = new Vector3(0, -(1-players[i])*130, 0);
            ((RectTransform)mask.Find("Circle")).anchoredPosition = new Vector3(0, (1 - players[i]) * 130, 0);
        }
    }
}
