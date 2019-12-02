using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDestroyerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
