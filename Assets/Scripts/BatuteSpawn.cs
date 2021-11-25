using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatuteSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject obj;

    float RandX;
    float RandZ;
    Vector3 whereToSpawn;
    int spawnCount;
    public Dropdown dropdown;
    public static bool FirstInstance = true;
    public int SomeValue = 100;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject = obj;
        gameObject.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
        int index = dropdown.value;
        switch (index)
        {
            case 0:
                spawnCount = 1;
                break;

            case 1:
                spawnCount = 2;
                break;

            case 2:
                spawnCount = 3;
                break;

            case 3:
                spawnCount = 4;
                break;
        }

        RandX = Random.Range(-25f, 25f);
        RandZ = Random.Range(-25f, 25f);
        if (spawnCount > 0)
        {
            
            SomeValue--;
            var randomPosition = new Vector3(RandX, transform.position.y, RandZ);
            var clone = Instantiate(gameObject, randomPosition, Quaternion.identity) as GameObject;
            spawnCount--;
        }



        //for(int i = 0; i < spawnCount; ++i)
        //{
        //    RandX = Random.Range(-25f, 25f);
        //    RandZ = Random.Range(-25f, 25f);
        //    whereToSpawn = new Vector3(RandX, transform.position.y, RandZ);
        //    Instantiate(gameObject, whereToSpawn, Quaternion.identity);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
    



