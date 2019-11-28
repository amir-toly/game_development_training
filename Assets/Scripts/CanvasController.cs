using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour, IPointerClickHandler
{
    private Text _scoreNumber;
    private Text _bestScore;
    private Text _gameOverSessionScore;

    private void Awake()
    {
        _scoreNumber = GameObject.Find("/Canvas/ScoreNumber").GetComponent<Text>();
        _bestScore = GameObject.Find("/Canvas/GameOverPanel/BestScoreNumber").GetComponent<Text>();
        _gameOverSessionScore = GameObject.Find("/Canvas/GameOverPanel/SessionScoreNumber").GetComponent<Text>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.name == "TapToPlay")
        {
            GameManager.Instance.StartGame();
        }
    }

    public void RestartGame()
    {
        GameManager.Instance.InitialiseGame();
    }

    private void Update()
    {
        _scoreNumber.text = GameManager.Instance.Score.ToString();

        //TODO: Put in the right place
        _bestScore.text = GameManager.Instance.BestScore.ToString();
        _gameOverSessionScore.text = GameManager.Instance.Score.ToString();
    }
}
