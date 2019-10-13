using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScripts : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void StartGame()
    {
        if (FindObjectOfType<PressStartToJoin>().playerMap.Count <= 0)
        {
            return;
        }
        GameManager.Instance.StartGame(FindObjectOfType<PressStartToJoin>().playerMap);
    }

}
