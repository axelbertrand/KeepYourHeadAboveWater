using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject playerPrefab;

    public Transform[] spawnPoints;

    public Sprite[] headSprites;
    public Sprite[] bodySprites;

    public Dictionary<PressStartToJoin.PlayerMap, PlayerController2> playersInGame = new Dictionary<PressStartToJoin.PlayerMap, PlayerController2>();

    // Start is called before the first frame update
    void Start()
    {

        List<PressStartToJoin.PlayerMap> playerMap = GameManager.Instance.players;

        if(playerMap == null)
        {
            return;
        }

        if(playerMap.Count > 0)
        {
            PlayerController2[] playerController2s = FindObjectsOfType<PlayerController2>();
            foreach(PlayerController2 playerController2 in playerController2s)
            {
                FindObjectOfType<CinemachineTargetGroup>().RemoveMember(playerController2.transform);
                Destroy(playerController2.gameObject);
            }
        }

        foreach(PressStartToJoin.PlayerMap player in playerMap)
        {

            GameObject newPlayer = Instantiate(playerPrefab, spawnPoints[player.gamePlayerId].position, new Quaternion());
            newPlayer.GetComponent<PlayerController2>().playerInputId = player.rewiredPlayerId;
            newPlayer.GetComponent<PlayerController2>().gamePlayerId = player.gamePlayerId;

            newPlayer.GetComponent<PlayerSprites>().bodySpriteRenderer.sprite = bodySprites[player.gamePlayerId];
            newPlayer.GetComponent<PlayerSprites>().headSpriteRenderer.sprite = headSprites[player.gamePlayerId];

            FindObjectOfType<CinemachineTargetGroup>().AddMember(newPlayer.transform, 1, 14);

            playersInGame.Add(player, newPlayer.GetComponent<PlayerController2>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
