using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LineType
{
    Grass, // 0
    Road, // 1
    Railway, // 2
    River // 3
}

[System.Serializable]
public struct WorldLines
{
    public GameObject darkGrass;
    public GameObject lightGrass;
    public GameObject road;
    public GameObject roadBuffer;
    public GameObject roadLines;
    public GameObject railway;
    public GameObject river;
}

public class WorldGenerator : MonoBehaviour
{
    public WorldLines worldLines;

    private float builderZOffset = 3.6f;

    // Start is called before the first frame update
    void Start()
    {
        GenerateWorld(30);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateWorld(int linesToCreate = 1)
    {
        for (int i = 0; i < linesToCreate; i++)
        {
            LineType lineType = (LineType)Random.Range(0, 4);

            switch (lineType)
            {
                case LineType.Grass:
                    Instantiate(
                        worldLines.lightGrass,
                        new Vector3(0, 0, builderZOffset),
                        Quaternion.identity,
                        this.transform);
                    break;
                case LineType.Railway:
                    Instantiate(
                        worldLines.railway,
                        new Vector3(0, 0, builderZOffset),
                        Quaternion.identity,
                        this.transform);
                    break;
            }

            builderZOffset += 0.9f;
        }
    }
}
