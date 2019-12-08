using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogController : MonoBehaviour
{
    [HideInInspector] public float logSpeed;

    private Vector3 _currentLogPosition;
    private int _logDirection;

    private float _currentSpeed;        // log speed
    private float _maxSpeed;            // maximum speed of log at any time
    private float _acceleration = 7f;   // how fast the log will accelerate/decelerate from central speed

    // Start is called before the first frame update
    void Start()
    {
        _currentLogPosition = transform.position;
        _currentSpeed = 5f * Mathf.Sign(logSpeed);
        _maxSpeed = 6f * Mathf.Sign(logSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (logSpeed > 0)
        {
            if (_currentLogPosition.x >= -5 && _currentLogPosition.x < 4)
            {
                if (_currentSpeed > logSpeed)
                    _currentSpeed -= _acceleration * Time.deltaTime;
            }
            else if (_currentLogPosition.x >= 4 && _currentLogPosition.x < 11)
            {
                if (_currentSpeed < _maxSpeed)
                    _currentSpeed += _acceleration * Time.deltaTime;
            }
            else if (_currentLogPosition.x >= 11)
            {
                Destroy(this.gameObject);
            }
        }
        else // Now we move in negative speed
        {
            if (_currentLogPosition.x <= 5 && _currentLogPosition.x > -4)
            {
                if (_currentSpeed < logSpeed)
                    _currentSpeed += _acceleration * Time.deltaTime;
            }
            else if (_currentLogPosition.x <= -4 && _currentLogPosition.x > -11)
            {
                if (_currentSpeed > _maxSpeed)
                    _currentSpeed -= _acceleration * Time.deltaTime;
            }
            else if (_currentLogPosition.x <= -11)
            {
                Destroy(this.gameObject);
            }
        }

        // Increment the X position with the speed increment in units
        _currentLogPosition.x += _currentSpeed * Time.deltaTime;

        transform.position = _currentLogPosition;
    }
}
