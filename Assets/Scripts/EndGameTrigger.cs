using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using UnityEngine.UI;
using Cinemachine;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField]
    private Transform levelSurface;

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.tag == "Player")
        {
            Debug.Log("Un joueur a gagné !");

            // Focus the camera on the winner
            CinemachineTargetGroup targetGroupComponent = GameObject.Find("TargetGroup").GetComponent<CinemachineTargetGroup>();
            List<CinemachineTargetGroup.Target> targetList = new List<CinemachineTargetGroup.Target>(targetGroupComponent.m_Targets);
            CinemachineTargetGroup.Target winnerTarget = targetList.Find(target => target.target == collided.transform.parent);
            winnerTarget.radius = 10;
            targetList.Clear();
            targetList.Add(winnerTarget);
            targetGroupComponent.m_Targets = targetList.ToArray();

            // Disable player control
            collided.GetComponentInParent<PlayerController2>().enabled = false;
            collided.GetComponentInParent<Prime31.CharacterController2D>().enabled = false;
            GameObject helicopter = GameObject.Find("Helicopter");

            // Disable collisions with the object being attached
            BoxCollider2D collider = collided.GetComponentInParent<BoxCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            // Don't allow physics to affect the object
            Rigidbody2D rigidBody = collided.GetComponentInParent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.isKinematic = true;
            }

            Vector3 helicopterLandingPosition = levelSurface.position + new Vector3(-levelSurface.localScale.x / 4, 2 * helicopter.transform.localScale.y + levelSurface.localScale.y / 2);
            Vector3 winnerPosition = new Vector3(helicopterLandingPosition.x, levelSurface.position.y + collided.transform.parent.localScale.y / 2 + levelSurface.localScale.y / 2);
            Vector3[] waypoints = new Vector3[] { new Vector3(levelSurface.position.x, helicopter.transform.position.y), helicopterLandingPosition };
            
            DOTween.Sequence()
                // Move winner to the landing spot
                .Append(collided.transform.parent.DOMove(winnerPosition, 1))
                // Move helicopter to the landing spot
                .Append(helicopter.transform.DOPath(waypoints, 3, PathType.CatmullRom).SetEase(Ease.OutQuad))
                .AppendInterval(1)
                // Attach winner to helicopter
                .AppendCallback(() =>
                {
                    collided.transform.parent.SetParent(helicopter.transform);
                    collided.transform.parent.localPosition = Vector3.zero;
                    CinemachineVirtualCamera camera2D = GameObject.Find("2DCamera").GetComponent<CinemachineVirtualCamera>();
                    camera2D.Follow = null;
                    camera2D.LookAt = collided.transform.parent;
                })
                // Helicopter landing off
                .Append(helicopter.transform.DOMove(helicopterLandingPosition + new Vector3(0, 15), 2).SetEase(Ease.InQuad))
                // When finished show end game menu
                .OnComplete(() =>
                {
                    GameObject.Find("EndGameText").GetComponent<Text>().enabled = true;
                });
        }
    }
}
