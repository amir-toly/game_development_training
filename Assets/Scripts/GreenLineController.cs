using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenLineController : MonoBehaviour
{
    public List<GameObject> worldProps;

    private int propDensity = 10;

    // Awake is called when instantiating the object
    void Awake()
    {
        foreach (int side in new int[] { -1, 1})
        {
            Instantiate(worldProps[Random.Range(0, 4)],
                new Vector3(
                    5 * side,
                    0.2f,
                    this.transform.position.z
                    ),
                Quaternion.identity,
                // Attached to the parent
                this.transform);

            for (int i = 1; i < 5; i++)
            {
                int propType = Random.Range(0, propDensity);

                if (propType < 4) // Any kind of tree
                {
                    Instantiate(worldProps[propType],
                        new Vector3(
                            i * side,
                            0.2f,
                            this.transform.position.z
                            ),
                        Quaternion.identity,
                        this.transform
                        );
                }
                else if (propType == 4) // This is going to be a stone
                {
                    int stoneRotation = new int[] { 0, 90, 180, 270 } [ Random.Range(0, 4) ];

                    Instantiate(worldProps[propType],
                        new Vector3(
                            i * side,
                            0.2f,
                            this.transform.position.z
                            ),
                        Quaternion.Euler(0, stoneRotation, 0),
                        this.transform
                        );
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
