using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Vehicle
{
    public string model;
    public GameObject prefab;
    public bool unlocked;
}

public class RoadController : MonoBehaviour
{
    public List<Vehicle> vehicles;

    // Awake is called when instantiating the object
    void Awake()
    {
        float distanceBetweenCarsInSeconds = Random.Range(2.2f, 3.7f);
        int carSpeed = Random.Range(2, 5);
        int moveDirection = new int[] { -1, 1 } [Random.Range(0, 2)];
        int carModel = Random.Range(0, vehicles.Count);

        // Will open a new thread
        StartCoroutine(InstantiateCar(carModel, distanceBetweenCarsInSeconds, moveDirection, carSpeed));
    }

    private IEnumerator InstantiateCar(int carModel, float distanceInSeconds, int moveDirection, int speed)
    {
        while (true)
        {
            Vector3 sideToStart = new Vector3((-10 * moveDirection), 0.1f, 0);
            Quaternion rotation = Quaternion.Euler(0, (moveDirection == 1) ? 180 : 0, 0);

            GameObject newCar = Instantiate(vehicles[carModel].prefab, transform.position + sideToStart, rotation, this.transform);

            yield return new WaitForSeconds(distanceInSeconds);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
