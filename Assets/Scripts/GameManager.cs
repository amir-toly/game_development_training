﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private Text _tapToPlay;
    private Text _scoreNumber;

    private int _sessionScore;
    private int _topScore;

    void Awake()
    {
        _tapToPlay = GameObject.Find("/Canvas/TapToPlay").GetComponent<Text>();
        _scoreNumber = GameObject.Find("/Canvas/ScoreNumber").GetComponent<Text>();
    }

    public void StartGame()
    {
        _tapToPlay.gameObject.SetActive(false);
        _scoreNumber.gameObject.SetActive(true);

        _sessionScore = 0;

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

    public int Score
    {
        get
        {
            return _sessionScore;
        }

        set
        {
            _sessionScore = Mathf.Max(value, _sessionScore);
            _scoreNumber.text = _sessionScore.ToString();
        }
    }
}
