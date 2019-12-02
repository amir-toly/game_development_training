using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 cameraOffset;
    [SerializeField] private float cameraFollowSpeed = 5.0f;
    [SerializeField] private bool smoothFollow;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.transform.position + cameraOffset;
    }

    // Update is called once per frame
    void Update()
    {
        if (smoothFollow)
        {
            transform.position = Vector3.Lerp(transform.position,
                player.transform.position + cameraOffset,
                Time.deltaTime * cameraFollowSpeed);
        }
        else
        {
            transform.position = player.transform.position + cameraOffset;
        }
    }
}
