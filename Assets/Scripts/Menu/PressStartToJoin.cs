using UnityEngine;
using System.Collections.Generic;
using Rewired;

[AddComponentMenu("")]
public class PressStartToJoin : MonoBehaviour
{
    public GameObject player1Text;
    public GameObject player1Img;
    public GameObject player2Text;
    public GameObject player2Img;
    public GameObject player3Text;
    public GameObject player3Img;
    public GameObject player4Text;
    public GameObject player4Img;

    private static PressStartToJoin instance;

    public static Rewired.Player GetRewiredPlayer(int gamePlayerId)
    {
        if (!Rewired.ReInput.isReady) return null;
        if (instance == null)
        {
            Debug.LogError("Not initialized. Do you have a PressStartToJoinPlayerSelector in your scehe?");
            return null;
        }
        for (int i = 0; i < instance.playerMap.Count; i++)
        {
            if (instance.playerMap[i].gamePlayerId == gamePlayerId) return ReInput.players.GetPlayer(instance.playerMap[i].rewiredPlayerId);
        }
        return null;
    }

    // Instance

    public int maxPlayers = 4;

    public List<PlayerMap> playerMap; // Maps Rewired Player ids to game player ids
    private List<int> freeGamePlayerId = new List<int>{ 0, 1, 2, 3 };

    void Awake()
    {
        playerMap = new List<PlayerMap>();
        instance = this; // set up the singleton
    }

    void Update()
    {

        // Watch for JoinGame action in each Player
        for (int i = 0; i < ReInput.players.playerCount; i++)
        {
            if (ReInput.players.GetPlayer(i).GetButtonDown("JoinGame"))
            {
                AssignNextPlayer(i);
            }

            if (ReInput.players.GetPlayer(i).GetButtonDown("LeaveGame"))
            {
                UnAssignPlayer(i);
            }
        }
    }

    void AssignNextPlayer(int rewiredPlayerId)
    {
        if (playerMap.Count >= maxPlayers)
        {
            Debug.LogError("Max player limit already reached!");
            return;
        }

        int gamePlayerId = GetNextGamePlayerId();

        // Add the Rewired Player as the next open game player slot
        playerMap.Add(new PlayerMap(rewiredPlayerId, gamePlayerId));

        SwitchTextToImg(gamePlayerId);

        //Player rewiredPlayer = ReInput.players.GetPlayer(rewiredPlayerId);

        // Disable the Assignment map category in Player so no more JoinGame Actions return
        //rewiredPlayer.controllers.maps.SetMapsEnabled(false, "Assignment");

        Debug.Log("Added Rewired Player id " + rewiredPlayerId + " to game player " + gamePlayerId);
    }

    void UnAssignPlayer(int rewiredPlayerId)
    {
        bool found = false;
        int index = 0;
        for (; index < playerMap.Count; ++index)
        {
            if (playerMap[index].rewiredPlayerId == rewiredPlayerId)
            {
                found = true;
                break;
            }
        }

        if (found)
        {
            int gamePlayerId = playerMap[index].gamePlayerId;
            SwitchImgToTxt(gamePlayerId);
            freeGamePlayerId.Add(gamePlayerId);
            freeGamePlayerId.Sort();
            playerMap.RemoveAt(index);
        }
        else
        {
            Debug.Log("Deleting unknown RewiredId");
        }
    }

    private void SwitchTextToImg(int playerId)
    {
        switch (playerId)
        {
            case 0:
                player1Text.SetActive(false);
                player1Img.SetActive(true);
                break;
            case 1:
                player2Text.SetActive(false);
                player2Img.SetActive(true);
                break;
            case 2:
                player3Text.SetActive(false);
                player3Img.SetActive(true);
                break;
            case 3:
                player4Text.SetActive(false);
                player4Img.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void SwitchImgToTxt(int playerId)
    {
        switch (playerId)
        {
            case 0:
                player1Text.SetActive(true);
                player1Img.SetActive(false);
                break;
            case 1:
                player2Text.SetActive(true);
                player2Img.SetActive(false);
                break;
            case 2:
                player3Text.SetActive(true);
                player3Img.SetActive(false);
                break;
            case 3:
                player4Text.SetActive(true);
                player4Img.SetActive(false);
                break;
            default:
                break;
        }
    }



    private int GetNextGamePlayerId()
    {
        if (freeGamePlayerId.Count <= 0)
        {
            Debug.Log("No more game id to give.");
            return 0;
        }

        int res = freeGamePlayerId[0];
        freeGamePlayerId.RemoveAt(0);
        return res;
    }

    // This class is used to map the Rewired Player Id to your game player id
    public class PlayerMap
    {
        public int rewiredPlayerId;
        public int gamePlayerId;

        public PlayerMap(int rewiredPlayerId, int gamePlayerId)
        {
            this.rewiredPlayerId = rewiredPlayerId;
            this.gamePlayerId = gamePlayerId;
        }
    }

    public void Reset()
    {
        for (int i = 0; i<4; ++i)
        {
            SwitchImgToTxt(i);
        }
        freeGamePlayerId = new List<int> { 0, 1, 2, 3 };
        playerMap.Clear();
    }
}
