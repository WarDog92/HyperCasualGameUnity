using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelScene : MonoBehaviour
{
    public GameObject NextLevelScene;
    public GameObject EndingScene;

    // Update is called once per frame
    void Update()
    {

    }
    public void Finnish()
    {
        GameObject[] batut = GameObject.FindGameObjectsWithTag("Batut");
        int batutCount = batut.Length;
        GameObject[] player = GameObject.FindGameObjectsWithTag("Player");
        int playerCount = player.Length;
        if (batutCount > 2)
        {
            NextLevelScene.SetActive(true);
            Time.timeScale = 0.1f;
            StartCoroutine(Continue());
        } else
        {
            EndingScene.SetActive(true);
            Time.timeScale = 0.1f;
            StartCoroutine(ReturnToMenu());
        }
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(0.4f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    IEnumerator Continue()
    {
        yield return new WaitForSeconds(0.4f);
        NextLevelScene.SetActive(false);
        Time.timeScale = 1f;
        var Resources = GameObject.FindGameObjectsWithTag("Resource");
        for (var i = 0; i < Resources.Length; i++)
            Destroy(Resources[i]);
    }
}
