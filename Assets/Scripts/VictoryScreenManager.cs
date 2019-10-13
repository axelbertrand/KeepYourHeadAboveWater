using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VictoryScreenManager : MonoBehaviour
{
    public EventSystem eventSystem;
    public Button restartButton;

    public GameObject borderImage;
    public GameObject contentImage;

    public List<Sprite> winnerSprites;
    public List<Sprite> looserSprites;

    public Image winnerSpot;
    public List<Image> looserSpots;

    private void Awake()
    {
        StartVictoryScreen(1, new List<int> { 0, 2 });
    }

    public void StartVictoryScreen(int winnerId, List<int> looserIds)
    {
        borderImage.SetActive(true);
        contentImage.SetActive(true);
        eventSystem.SetSelectedGameObject(restartButton.gameObject);

        winnerSpot.sprite = winnerSprites[winnerId];
        for (int i = 0; i < looserIds.Count; ++i)
        {
            looserSpots[i].sprite = looserSprites[looserIds[i]];
            looserSpots[i].gameObject.SetActive(true);
        }
    }

    public void DisableVictoryScreen()
    {
        borderImage.SetActive(false);
        contentImage.SetActive(false);

        for (int i = 0; i < looserSpots.Count; ++i)
        {
            looserSpots[i].gameObject.SetActive(false);
        }
    }
}
