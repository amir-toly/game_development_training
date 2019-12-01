using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverController : MonoBehaviour
{
    private ParticleSystem _waterSplash;
    private AudioSource _splashSound;

    private void Awake()
    {
        _waterSplash = GetComponentInChildren<ParticleSystem>();
        _splashSound = GetComponentInChildren<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*
    private IEnumerator InstantiateLog(int moveDirection, float speed)
    {

    }
    */
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            _waterSplash.transform.position = new Vector3(
                other.transform.position.x,
                _waterSplash.transform.position.y,
                _waterSplash.transform.position.z);

            _waterSplash.Play();
            _splashSound.Play();

            other.gameObject.SetActive(false);

            GameManager.Instance.KillPlayer();
        }
    }
}
