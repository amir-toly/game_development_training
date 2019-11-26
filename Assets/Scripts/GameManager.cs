using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    void Awake()
    {
        StartGame();
    }

    public void StartGame()
    {
        isPlayerAlive = true;
        LoadGameData();
    }

    private void LoadGameData()
    {

    }

    private void SaveGameData()
    {

    }

    public void KillPlayer()
    {
        SaveGameData();

        isPlayerAlive = false;
    }

    public bool isPlayerAlive { get; private set; }
}
