using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLineController : MonoBehaviour
{
    private Vector3 _currentPosition;
    public List<GameObject> worldProps;

    // Awake is called when instantiating the object
    void Awake()
    {
        _currentPosition = transform.position;

        foreach (int side in new int[] { -1, 1})
        {
            Instantiate(worldProps[Random.Range(0, 4)],
                new Vector3(
                    (4.5f * side) + _currentPosition.x,
                    0 + _currentPosition.y,
                    -0.45f + _currentPosition.z
                    ),
                Quaternion.identity,
                // Attached to the parent
                this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
