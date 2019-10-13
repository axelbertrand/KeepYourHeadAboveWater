using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScreenManager : MonoBehaviour
{
    public GameObject borderImage;
    public GameObject contentImage;

    public List<Sprite> winnerSprites;
    public List<Sprite> looserSprites;

    public Image winnerSpot;
    public List<Image> looserSpots;

    public void StartVictoryScreen(int winnerId, List<int> looserIds)
    {
        borderImage.SetActive(true);
        contentImage.SetActive(true);

        winnerSpot.sprite = winnerSprites[winnerId];
        for (int i = 0; i < looserIds.Count; ++i)
        {
            looserSpots[i].sprite = looserSprites[looserIds[i]];
            looserSpots[i].gameObject.SetActive(true);
        }
    }
}
