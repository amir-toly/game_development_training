using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLineController : MonoBehaviour
{
    public List<GameObject> worldProps;

    // Awake is called when instantiating the object
    void Awake()
    {
        foreach (int side in new int[] { -1, 1})
        {
            Instantiate(worldProps[Random.Range(0, 4)],
                new Vector3((4.5f * side), 0, -0.45f),
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
