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

    private Vector3 bottomPosition;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchHook(FishingRod player, float length, float speed)
    {

        transform.localScale = player.transform.localScale.x < 0 ? new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z) : transform.localScale; 

        bottomPosition = new Vector3(transform.position.x, transform.position.y - length, transform.position.z);

        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(transform.position.y - length, speed).SetEase(Ease.Linear)).AppendCallback(() => GoBackToPlayer()); ;

        cableSpeed = speed;
        playerFishingRod = player;

        GetComponent<SpringJointRenderer>().parent = playerFishingRod.hookPosition.transform;
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

        float perc = Vector3.Distance(bottomPosition, transform.position) / Vector3.Distance(bottomPosition, playerFishingRod.hookPosition.transform.position);
        float newSpeed = (1 - perc) * cableSpeed; 

        sequence = DOTween.Sequence();
        sequence.Append(transform.DOMoveY(playerFishingRod.transform.position.y, newSpeed).SetEase(Ease.Linear)).AppendCallback((() => playerFishingRod.CancelFishingRod(hookedObject)));
    }
}
