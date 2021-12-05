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

    // Start is called before the first frame update
    void Start()
    {
        GameObject gameObject = obj;
        int index = dropdown.value;
        switch (index)
        {
            case 0:
                spawnCount = 0;
                break;

            case 1:
                spawnCount = 1;
                break;

            case 2:
                spawnCount = 2;
                break;

            case 3:
                spawnCount = 3; //так как один батут уже стоит по дефолту
                break;
        }

        if (spawnCount > 0)
        {
            while(spawnCount > 0){
                //RandX = Random.Range(-25f, 25f);
                RandZ = Random.Range(-25f, 25f);
                var randomPosition = new Vector3(33, transform.position.y, 5 * (spawnCount * 5) - 54);//RandZ);
                var clone = Instantiate(gameObject, randomPosition, Quaternion.identity) as GameObject;
                clone.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
                spawnCount--;
            }
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
    



