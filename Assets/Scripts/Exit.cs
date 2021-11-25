using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Exit : MonoBehaviour
{
    public void ExitPressed()
    {
#if DEBUG
        Debug.Log("Exit pressed!");
#else
        Application.Quit();
#endif
    }
}
