using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character {
    public String name;
    public GameObject prefab;
    public Vector3 offset;
    public bool unlocked;
}

public class PlayerController : MonoBehaviour
{
    public List<Character> characters;
    public float jumpDuration = 0.30f;

    private Vector3 _currentPosition;
    private Vector3 _targetPosition;

    private Vector3 _initialScale;

    private bool _playerMoving;
    private float _elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        _currentPosition = transform.position;
        _initialScale = transform.localScale;

        Instantiate(characters[0].prefab, _currentPosition + characters[0].offset,
          Quaternion.identity, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerMoving)
        {
            KeepMovingUntilDestination();
        }
        else
        {
            WaitForKeyboardInput();
        }
    }

    private void KeepMovingUntilDestination()
    {
        _elapsedTime += Time.deltaTime;

        float t = (_elapsedTime < jumpDuration) ? (_elapsedTime / jumpDuration) : 1;

        float x = Lerp(_currentPosition.x, _targetPosition.x, t);
        float z = Lerp(_currentPosition.z, _targetPosition.z, t);
        float y = 0.5f;

        Vector3 displacement = new Vector3(x, y, z);

        transform.position = displacement;

        if (displacement == _targetPosition)
        {
            _playerMoving = false;
            _currentPosition = _targetPosition;
        }
        _playerMoving = false;
    }

    private float Lerp(float min, float max, float t)
    {
        return min + (max - min) * t;
    }

    private void WaitForKeyboardInput()
    {
        //TODO(check syntax): Debug.LogMessage("Hello world");
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            TranslatePlayer(new Vector3(0, 0, 1));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            TranslatePlayer(new Vector3(0, 0, -1));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            TranslatePlayer(new Vector3(-1, 0, 0));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            TranslatePlayer(new Vector3(1, 0, 0));
        }
    }

    private void TranslatePlayer(Vector3 displacement)
    {
        _currentPosition = transform.position;
        _targetPosition = _currentPosition + displacement;

        _elapsedTime = 0;

        _playerMoving = true;
    }
}
