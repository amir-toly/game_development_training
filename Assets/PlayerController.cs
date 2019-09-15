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

    private GameObject _characterPrefab;

    private Vector3 _currentPosition;
    private Vector3 _targetPosition;

    private Quaternion _startRotation;
    private Quaternion _endRotation;

    private Vector3 _initialScale;

    private bool _playerMoving;
    private float _elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        _currentPosition = transform.position;
        _initialScale = transform.localScale;

        _characterPrefab = Instantiate(characters[0].prefab, _currentPosition + characters[0].offset,
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
        float y = 0;

        Vector3 displacement = new Vector3(x, y, z);

        transform.position = displacement;
        transform.rotation = Quaternion.Lerp(_startRotation, _endRotation, t);

        if (displacement == _targetPosition)
        {
            _playerMoving = false;
            _currentPosition = _targetPosition;

            _characterPrefab.GetComponent<Rigidbody>()
                .AddForce(0, -5, 0, ForceMode.VelocityChange);
        }
        _playerMoving = false;
    }

    private float Lerp(float min, float max, float t)
    {
        return min + (max - min) * t;
    }

    private void WaitForKeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
            Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.localScale = new Vector3(0, -0.2f, 0);
        }

        //Debug.Log("Hello world");
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            TranslatePlayer('N');
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            TranslatePlayer('S');
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            TranslatePlayer('W');
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            TranslatePlayer('E');
        }
    }

    private void TranslatePlayer(char direction)
    {
        _currentPosition = transform.position;

        _startRotation = transform.rotation;
        _endRotation = Quaternion.identity;

        transform.localScale = _initialScale;

        switch (direction)
        {
            case 'N':
                _targetPosition += new Vector3(0, 0, 1);
                _endRotation = Quaternion.Euler(0, 0, 0);
                break;
            case 'S':
                _targetPosition += new Vector3(0, 0, -1);
                _endRotation = Quaternion.Euler(0, 180, 0);
                break;
            case 'W':
                _targetPosition += new Vector3(-1, 0, 0);
                _endRotation = Quaternion.Euler(0, 90, 0);
                break;
            case 'E':
                _targetPosition += new Vector3(1, 0, 0);
                _endRotation = Quaternion.Euler(0, 270, 0);
                break;
        }

        //_targetPosition = _currentPosition + displacement;

        _elapsedTime = 0;

        _playerMoving = true;
    }
}
