using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    public List<PressStartToJoin.PlayerMap> players;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(List<PressStartToJoin.PlayerMap> plyrs)
    {
        players = plyrs;
        SceneLoader.Instance.LoadScene("GameScene");
    }

    public void RestartGame()
    {
        SceneLoader.Instance.LoadScene("GameScene");
    }

    public void GoMainMenu()
    {
        SceneLoader.Instance.LoadScene("MainMenu");
    }
}
