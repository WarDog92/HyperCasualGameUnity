using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResBar: MonoBehaviour
{
    public static RawImage res_bar;
    // Start is called before the first frame update
    void Start()
    {
       res_bar = GetComponent<RawImage>(); 
    }

    // Update is called once per frame
    void Update()
    {
    }
}
