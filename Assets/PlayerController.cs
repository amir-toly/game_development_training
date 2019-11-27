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
    private ICharacterable _characterBehaviour;

    private Vector3 _currentPosition;
    private Vector3 _targetPosition;

    private Quaternion _startRotation;
    private Quaternion _endRotation;

    private Vector3 _initialScale;

    private bool _playerMoving;
    private float _elapsedTime;

    private float _obstacleDetectionDistance = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        _currentPosition = transform.position;
        _initialScale = transform.localScale;

        _characterPrefab = Instantiate(characters[4].prefab, _currentPosition + characters[0].offset,
          Quaternion.identity, this.transform);

        _characterBehaviour = _characterPrefab.GetComponent<ICharacterable>();
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
        float y = Mathf.Sin(Mathf.PI * t) * 1;

        Vector3 displacement = new Vector3(x, y, z);

        transform.position = displacement;
        transform.rotation = Quaternion.Lerp(_startRotation, _endRotation, t);

        if (displacement == _targetPosition)
        {
            _playerMoving = false;
            _currentPosition = _targetPosition;

            GameManager.Instance.Score = Mathf.CeilToInt(transform.position.z);

            _characterPrefab.GetComponent<Rigidbody>()
                .AddForce(Vector3.down * 5, ForceMode.VelocityChange);
        }
    }

    private float Lerp(float min, float max, float t)
    {
        return min + (max - min) * t;
    }

    private void WaitForKeyboardInput()
    {
        if (GameManager.Instance.isPlayerAlive)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                float newScale = _initialScale.y * 0.8f;
                transform.localScale = new Vector3(_initialScale.x, newScale, _initialScale.z);
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
    }

    private void TranslatePlayer(char direction)
    {
        _currentPosition = transform.position;

        _startRotation = transform.rotation;
        _endRotation = Quaternion.identity;

        switch (direction)
        {
            case 'N':
                if (isJumpAllowed(new Vector3(0, 0, _obstacleDetectionDistance)))
                    _targetPosition = _currentPosition + new Vector3(0, 0, 1);

                _endRotation = Quaternion.Euler(0, 180, 0);
                break;
            case 'S':
                if (isJumpAllowed(new Vector3(0, 0, -_obstacleDetectionDistance)))
                    _targetPosition = _currentPosition + new Vector3(0, 0, -1);

                _endRotation = Quaternion.Euler(0, 0, 0);
                break;
            case 'W':
                if (isJumpAllowed(new Vector3(-_obstacleDetectionDistance, 0, 0)))
                    _targetPosition = _currentPosition + new Vector3(-1, 0, 0);

                _endRotation = Quaternion.Euler(0, 90, 0);
                break;
            case 'E':
                if (isJumpAllowed(new Vector3(_obstacleDetectionDistance, 0, 0)))
                    _targetPosition = _currentPosition + new Vector3(1, 0, 0);

                _endRotation = Quaternion.Euler(0, 270, 0);
                break;
        }

        _elapsedTime = 0f;

        //_characterBehaviour.HopAudioPlay();

        _playerMoving = true;
    }

    private bool isJumpAllowed(Vector3 direction)
    {
        RaycastHit raycastHit;

        LayerMask propMask = LayerMask.GetMask("WorldProps");

        // Does the ray intersect any objects in the "propMask" layer?
        bool canJump = Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), direction,
            out raycastHit, _obstacleDetectionDistance, propMask);

        // If the ray intersects any tree/stone then we cannot move. As the Raycast method returns 
        // true in case of intersection, then we must reverse the result (intersect true -> return
        // false to avoid jump). 
        return !canJump;
    }

    private void OnDrawGizmos()
    {
        RaycastHit raycastHit;
        LayerMask propMask = LayerMask.GetMask("WorldProps");

        if (Physics.Raycast(transform.position + new Vector3(0, 0.5f, 0), transform.forward,
            out raycastHit, _obstacleDetectionDistance, propMask))
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.forward);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position + new Vector3(0, 0.5f, 0), transform.forward);
        }
    }
}
