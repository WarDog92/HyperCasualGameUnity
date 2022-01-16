using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatutBar: MonoBehaviour
{
    public static RawImage batut_bar;
    // Start is called before the first frame update
    void Start()
    {
       batut_bar = GetComponent<RawImage>(); 
    }

    // Update is called once per frame
    void Update()
    {
    }
}
