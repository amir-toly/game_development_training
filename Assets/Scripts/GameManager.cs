using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    private Text _tapToPlay;
    private GameObject _gameOverPanel;

    private int _sessionScore;
    private int _topScore;

    void Awake()
    {
        _tapToPlay = GameObject.Find("/Canvas/TapToPlay").GetComponent<Text>();
        _gameOverPanel = GameObject.Find("/Canvas/GameOverPanel");

        InitialiseGame();
    }

    public void InitialiseGame()
    {
        _gameOverPanel.gameObject.SetActive(false);
        _tapToPlay.gameObject.SetActive(true);

        _sessionScore = 0;
    }

    public void StartGame()
    {
        _tapToPlay.gameObject.SetActive(false);

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

        _gameOverPanel.SetActive(true);
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
            //_scoreNumber.text = _sessionScore.ToString();
        }
    }
}
