using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour, IPointerClickHandler
{
    private Text _scoreNumber;

    private void Awake()
    {
        _scoreNumber = GameObject.Find("/Canvas/ScoreNumber").GetComponent<Text>();
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
        _scoreNumber = GameObject.Find("/Canvas/ScoreNumber").GetComponent<Text>();
    }
}
