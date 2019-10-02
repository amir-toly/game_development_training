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
    public GameObject roadWithLines;
    public GameObject roadWithoutLines;
    public GameObject railway;
    public GameObject river;
}

public class WorldGenerator : MonoBehaviour
{
    public WorldLines worldLines;

    private float builderZOffset = 4;
    private LineType _lastLineType;

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
            LineType lineType = (LineType) Random.Range(0, 4);

            switch (lineType)
            {
                /*case LineType.Grass:
                    Instantiate(
                        worldLines.lightGrass,
                        new Vector3(0, 0, builderZOffset),
                        Quaternion.identity,
                        this.transform);
                    break;*/
                case LineType.Grass:
                    if (builderZOffset % 2 == 0)
                        Instantiate(
                            worldLines.darkGrass,
                            new Vector3(0, 0, builderZOffset),
                            Quaternion.identity,
                            this.transform);
                    else
                        Instantiate(
                            worldLines.lightGrass,
                            new Vector3(0, 0, builderZOffset),
                            Quaternion.identity,
                            this.transform);
                    break;
                /*case LineType.Railway:
                    Instantiate(
                        worldLines.railway,
                        new Vector3(0, 0, builderZOffset),
                        Quaternion.identity,
                        this.transform);
                    break;*/
                case LineType.Railway: // Fake RoadBuffer
                    Instantiate(
                        worldLines.railway,
                        new Vector3(0, 0, builderZOffset),
                        Quaternion.identity,
                        this.transform);
                    break;
                /*case LineType.Road:
                    Instantiate(
                        worldLines.road,
                        new Vector3(0, 0, builderZOffset),
                        Quaternion.identity,
                        this.transform);
                    break;*/
                case LineType.Road:
                    if (_lastLineType == LineType.Road)
                        Instantiate(
                            worldLines.roadWithLines,
                            new Vector3(0, 0, builderZOffset),
                            Quaternion.identity,
                            this.transform);
                    else
                        Instantiate(
                            worldLines.roadWithoutLines,
                            new Vector3(0, 0, builderZOffset),
                            Quaternion.identity,
                            this.transform);
                    break;
                case LineType.River:
                    Instantiate(
                        worldLines.river,
                        new Vector3(0, 0, builderZOffset),
                        Quaternion.identity,
                        this.transform);
                    break;
            }

            builderZOffset += 1;
            _lastLineType = lineType;
        }
    }
}
