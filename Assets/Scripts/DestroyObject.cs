using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DestroyObject : MonoBehaviour
{
    private int counter = 0;
    private int batut_counter = 0;
    [SerializeField]
    private RawImage res_bar;
    [SerializeField]
    private RawImage batut_bar;
    [SerializeField]
    private GameObject batut;
    [SerializeField]
    private GameObject res_bar_g;

    private bool isBatutFull = false;

    private Animator anim;

    public PlayerController cont;


    public Texture[] myTextures = new Texture[4];
    public Texture[] myBatutTextures = new Texture[4];

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Resource" && !isBatutFull)
        {
            if (counter < 3)
            {
                Destroy(other.gameObject);
                counter++;
                res_bar.texture = myTextures[counter];
            }

        }

        if ((other.gameObject.name == "Batut" || other.gameObject.name == "Batut(Clone)") && counter == 3)
        {
            counter = 0;
            res_bar.texture = myTextures[counter];
            if (batut_counter < 2)
            {
                batut_counter++;
                batut_bar.texture = myBatutTextures[batut_counter];
                Debug.Log(batut_counter);
            }
            else 
            {
                res_bar_g.SetActive(false);
                cont.moveVector = new Vector3(0,0,0);
                batut_counter++;
                batut_bar.texture = myBatutTextures[batut_counter];
                isBatutFull = true;
                anim.SetTrigger("Jump");
            }
        }

    }
    void Update(){
    }
    void timesOver()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(0.4f);
        counter = 0;
        //batut_counter = 0;
        res_bar.texture = myTextures[counter];
        //batut_bar.textures = myBatutTextures[batut_counter];
    }
}
