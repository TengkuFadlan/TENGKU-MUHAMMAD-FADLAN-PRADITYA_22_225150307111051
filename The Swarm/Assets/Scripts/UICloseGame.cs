using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICloseGame : MonoBehaviour
{
    public void CloseGame()
    {
        Debug.Log("Game Closed");
        Application.Quit();
    }
}
