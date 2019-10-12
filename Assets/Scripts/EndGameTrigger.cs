using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndGameTrigger : MonoBehaviour
{
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
            GameObject helicoptere = GameObject.Find("Helicoptere");
            DOTween.Sequence()
                .Append(helicoptere.transform.DOMove(collided.transform.position, 1))
                .AppendInterval(1)
                .Append(helicoptere.transform.DOMove(collided.transform.position + new Vector3(0, 10), 1));
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
