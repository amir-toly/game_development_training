using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct GameStats
{
    public int bestScore;
    public int availableCoins;
    // public IDictionary<int, bool> characters;
    // public int activeCharacter;

    public GameStats(int bestScore, int availableCoins)
    {
        this.bestScore = bestScore;
        this.availableCoins = availableCoins;
    }
}

public class GameManager : Singleton<GameManager>
{
    private Text _tapToPlay;
    private GameObject _gameOverPanel;
    private GameObject _activeCharacter;

    private Vector3 _charInitialLocation;
    private Quaternion _charInitialRotation;
    private Vector3 _charInitialScale;

    private int _sessionScore;
    private int _topScore;
    private int _availableCoins;

    void Awake()
    {
        _tapToPlay = GameObject.Find("/Canvas/TapToPlay").GetComponent<Text>();
        _gameOverPanel = GameObject.Find("/Canvas/GameOverPanel");

        InitialiseGame();
    }

    private void Start()
    {
        _activeCharacter = GameObject.FindGameObjectWithTag("Player");

        _charInitialLocation = _activeCharacter.transform.position;
        _charInitialRotation = _activeCharacter.transform.rotation;
        _charInitialScale = _activeCharacter.transform.localScale;
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

        _activeCharacter.transform.position = _charInitialLocation;
        _activeCharacter.transform.rotation = _charInitialRotation;
        _activeCharacter.transform.localScale = _charInitialScale;

        _sessionScore = 0;
        isPlayerAlive = true;

        LoadGameData();
    }

    private void LoadGameData()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.v1"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            // TODO: Create the try-catch block
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.v1", FileMode.Open);
            GameStats loadedGameStats = (GameStats) bf.Deserialize(file);
            file.Close();

            _topScore = loadedGameStats.bestScore;
            _availableCoins = loadedGameStats.availableCoins;
        }
        else
        {
            // Assume it's the first time the game is executed
            _topScore = 0;
            _availableCoins = 0;
        }
    }

    private void SaveGameData()
    {
        FileStream file = null;

        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Create(Application.persistentDataPath + "/gamesave.v1");

            _topScore = Math.Max(_sessionScore, _topScore);

            GameStats gameStats = new GameStats(_topScore, _availableCoins);

            bf.Serialize(file, gameStats);
        }
        catch (Exception e)
        {
            //TODO: Remove this sh*t for production
            Debug.Log(e.Message);
        }
        finally
        {
            if (file != null)
                file.Close();
        }
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

    public int BestScore
    {
        get
        {
            return _topScore;
        }
    }
}
