using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHUD : MonoBehaviour {

    public List<Texture> images;

    public RectTransform layout;
    public RectTransform defaultPlayer;

    private void Start()
    {
        SetupHealthComponents();

    }

    void Update() {
           
    }

    public void SetupHealthComponents()
    {

       
        foreach (KeyValuePair<PressStartToJoin.PlayerMap, PlayerController2> entry in FindObjectOfType<PlayerSpawner>().playersInGame)
        {
            RectTransform newHUD = Instantiate(defaultPlayer, layout.transform);
            newHUD.Find("Image").GetComponent<RawImage>().texture = images[entry.Key.gamePlayerId];
            newHUD.GetComponent<IndividualHealthHUD>().characterLife = entry.Value.GetComponent<CharacterLife>();
        }
    }
}
