using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [HideInInspector]
    public float carSpeed;

    private Vector3 _carPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _carPosition = transform.position;

        transform.position = Vector3.MoveTowards(
            _carPosition,
            _carPosition + new Vector3(24, 0, 0),
            carSpeed * Time.deltaTime);

        if (transform.position.x > 12 || transform.position.x < -12)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Character") && GameManager.Instance.isPlayerAlive)
        {
            other.transform.localScale = new Vector3(1.5f, 0.05f, 0.8f);

            GameManager.Instance.KillPlayer();
            //Debug.Log("Hit by car!");
        }
    }
}
