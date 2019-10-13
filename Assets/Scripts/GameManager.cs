using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public List<PressStartToJoin.PlayerMap> players = new List<PressStartToJoin.PlayerMap>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver(int winnerId)
    {
        List<int> loosersId = new List<int>();

        foreach(PressStartToJoin.PlayerMap map in players)
        {
            if(map.gamePlayerId != winnerId)
            {
                loosersId.Add(map.gamePlayerId);
            }
        }

        Time.timeScale = 0;
        FindObjectOfType<VictoryScreenManager>().StartVictoryScreen(winnerId, loosersId);
    }

    public void StartGame(List<PressStartToJoin.PlayerMap> plyrs)
    {
        Time.timeScale = 1;
        players = plyrs;
        SceneLoader.Instance.LoadScene("GameScene");
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene("GameScene");
    }

    public void GoMainMenu()
    {
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene("MainMenu");
    }
}
