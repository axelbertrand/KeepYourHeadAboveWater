using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookBehavior : MonoBehaviour
{

    private FishingRod playerFishingRod;
    private Sequence sequence;

    private float cableSpeed;

    private GameObject hookedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchHook(FishingRod player, float length, float speed)
    {
        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(transform.position.y - length, speed).SetEase(Ease.Linear)).AppendCallback(() => GoBackToPlayer()); ;

        cableSpeed = speed;
        playerFishingRod = player;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject parent = collision.transform.root.gameObject;

        if (parent.CompareTag("Player"))
        {
            parent.gameObject.GetComponent<PlayerController2>().SetPlayerState(PlayerController2.PlayerState.Hooked);
        }

        parent.transform.SetParent(transform);
        parent.transform.localPosition = new Vector3(2.5f, -1.15f);

        hookedObject = parent.gameObject;

        GoBackToPlayer();

    }

    private void GoBackToPlayer()
    {
        if (sequence != null)
        {
            sequence.Kill();
        }

        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(playerFishingRod.transform.position.y, cableSpeed).SetEase(Ease.Linear)).AppendCallback((() => playerFishingRod.CancelFishingRod(hookedObject)));
    }
}
