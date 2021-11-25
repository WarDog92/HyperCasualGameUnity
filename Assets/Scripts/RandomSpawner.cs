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
    //[SerializeField]
    private float spawnRate;
    //[SerializeField]
    private float nextSpawn = 0.0f;
    public Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        int index = dropdown.value;
        switch (index)
        {
            case 0:
                spawnRate = 2.5f;
                break;

            case 1:
                spawnRate = 1.5f;
                break;

            case 2:
                spawnRate = 0.5f;
                break;
        }
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
