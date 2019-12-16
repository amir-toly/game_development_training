using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Log
{
    public string size;
    public GameObject prefab;
}

public class RiverController : MonoBehaviour
{
    [SerializeField] private List<Log> logs;

    private ParticleSystem _waterSplash;
    private AudioSource _splashSound;

    private void Awake()
    {
        _waterSplash = GetComponentInChildren<ParticleSystem>();
        _splashSound = GetComponentInChildren<AudioSource>();

        float logSpeed = Random.Range(1f, 3f);
        int moveDirection = new int[] { -1, 1 } [Random.Range(0, 2)];

        StartCoroutine(InstantiateLog(moveDirection, logSpeed));
    }

    private IEnumerator InstantiateLog(int direction, float speed)
    {
        Vector3 sideToStart = new Vector3(-10 * direction, 0.1f, 0);

        yield return new WaitForSeconds(Random.Range(0f, 2.5f));

        while (true)
        {
            int size = Random.Range(0, logs.Count);

            GameObject newLog = Instantiate(
                logs[size].prefab,
                transform.position + sideToStart,
                Quaternion.identity,
                transform);
            newLog.GetComponent<LogController>().logSpeed = speed * direction;

            yield return new WaitForSeconds(Random.Range(2f, 4.5f));
        }
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
