using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform levelSurface;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.tag == "Player")
        {
            Debug.Log("Un joueur a gagné !");
            GameObject helicopter = GameObject.Find("Helicopter");

            Vector3 helicopterLandingPosition = levelSurface.position + new Vector3(-levelSurface.localScale.x / 4, 3 * helicopter.transform.localScale.y + levelSurface.localScale.y / 2);
            Vector3[] waypoints = new Vector3[] { new Vector3(levelSurface.position.x, helicopter.transform.position.y), helicopterLandingPosition };
            DOTween.Sequence()
                .Append(helicopter.transform.DOPath(waypoints, 2, PathType.CatmullRom).SetEase(Ease.OutQuad))
                .AppendInterval(1)
                .Append(helicopter.transform.DOMove(helicopterLandingPosition + new Vector3(0, 10), 2).SetEase(Ease.InQuad));
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
