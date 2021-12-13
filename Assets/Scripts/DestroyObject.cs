using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DestroyObject : MonoBehaviour
{
    private int counter = 0;
    [SerializeField]
    private RawImage res_bar;
    [SerializeField]
    private GameObject batut;


    public Texture[] myTextures = new Texture[4];

    void Strat(){
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Resource")
        {
            if (counter < 3)
            {
                Destroy(other.gameObject);
                counter++;
                res_bar.texture = myTextures[counter];
                Debug.Log(counter);
            }

        }

        if ((other.gameObject.name == "Batut" || other.gameObject.name == "Batut(Clone)") && counter == 3)
        {
            counter = 0;
            res_bar.texture = myTextures[counter];
        }
    }
    void Update(){
    }
}
