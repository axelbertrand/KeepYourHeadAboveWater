using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenButtons : MonoBehaviour
{
    public void OnReturnMenu()
    {
        GameManager.Instance.GoMainMenu();
    }

    public void OnRestart()
    {
        GameManager.Instance.RestartGame();
    }
}
