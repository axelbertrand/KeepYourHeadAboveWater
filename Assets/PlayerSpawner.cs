using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{

    public GameObject playerPrefab;

    public Transform[] spawnPoints;
    public Color[] colors;

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
            newPlayer.GetComponent<PlayerController2>().playerId = player.rewiredPlayerId;
            newPlayer.GetComponent<SpriteRenderer>().color = colors[player.gamePlayerId];

            FindObjectOfType<CinemachineTargetGroup>().AddMember(newPlayer.transform, 1, 14);

            playersInGame.Add(player, newPlayer.GetComponent<PlayerController2>());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
