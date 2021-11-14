using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject obj;
    float RandX;
    float RandZ;
    Vector3 whereToSpawn;
    [SerializeField]
    private float spawnRate = 2f;
    [SerializeField]
    private float nextSpawn = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            RandX = Random.Range(-19f, 19f);
            RandZ = Random.Range(-19f, 19f);
            whereToSpawn = new Vector3(RandX, transform.position.y, RandZ);
            Instantiate(obj, whereToSpawn, Quaternion.identity);
        }
    }
}
