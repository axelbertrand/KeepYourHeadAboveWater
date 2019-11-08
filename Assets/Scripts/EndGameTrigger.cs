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
    private Transform helicopter;

    [SerializeField]
    private GameObject helicopterPlayerPoint;

    [SerializeField]
    private Transform[] helicopterWayPoints;

    [SerializeField]
    private Transform pickUpPoint;

    [SerializeField]
    private waterBehavior  waterLevel;

    private bool isGameOver = false;

    
    void Update()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
        if (gos.Length <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (isGameOver)
        {
            return;
        }

        GameObject collided = collision.transform.root.gameObject;
        if (collided.tag == "Player")
        {
            isGameOver = true;

            Debug.Log("Un joueur a gagné !");
            waterLevel.setSpeed(0);


            // Focus the camera on the winner and the helicopter
            CinemachineTargetGroup targetGroupComponent = FindObjectOfType<CinemachineTargetGroup>();

            CinemachineTargetGroup.Target newTargetWinner = new CinemachineTargetGroup.Target();
            newTargetWinner.target = collided.transform;
            newTargetWinner.radius = 10;
            newTargetWinner.weight = 1;

            CinemachineTargetGroup.Target newTargetHelicopter = new CinemachineTargetGroup.Target();
            newTargetHelicopter.target = helicopter;
            newTargetHelicopter.radius = 15;
            newTargetHelicopter.weight = 1.1f;


            List<CinemachineTargetGroup.Target> targetList = new List<CinemachineTargetGroup.Target>();
            targetList.Add(newTargetWinner);
            targetList.Add(newTargetHelicopter);


            targetGroupComponent.m_Targets = targetList.ToArray();

            // Disable player control
            collided.GetComponentInParent<PlayerController2>().SetPlayerState(PlayerController2.PlayerState.DontMove);
           
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


            List<Vector3> waypoints = new List<Vector3>();

            foreach(Transform waypoint in helicopterWayPoints){
                waypoints.Add(waypoint.position);
            }
            waypoints.Add(pickUpPoint.position);



            
            DOTween.Sequence()
                
                // Move helicopter to the landing spot
                .Append(helicopter.DOPath(waypoints.ToArray(), 4, PathType.CatmullRom).SetEase(Ease.OutQuad))
                .AppendInterval(0.25f)
                // Attach winner to helicopter
                .AppendCallback(() =>
                {
                    collided.transform.SetParent(helicopterPlayerPoint.transform);
                    collided.transform.localPosition = Vector3.zero;

                    collided.GetComponentInParent<PlayerController2>().SetPlayerState(PlayerController2.PlayerState.Hooked);
                })
                // Helicopter landing off
                .Append(helicopter.transform.DOMove(pickUpPoint.position + new Vector3(0, 15), 2).SetEase(Ease.InQuad))

                // When finished show end game menu
                .AppendCallback(() =>
                {
                    Debug.Log("complete animation");
                    GameManager.Instance.GameOver(collider.GetComponent<PlayerController2>().gamePlayerId);
                });
        }
    }
}
