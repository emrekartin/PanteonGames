using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButonScripts : MonoBehaviour
{
    public static ButonScripts butonScripts;
    private void Awake()
    {
        butonScripts = this;
    }
    public void Retry()
    {
        SceneManager.LoadScene(0);

    }
    public void NextLevel()
    {
        

        PlayerPrefs.SetInt("levelNumber", GameManager.gameManager.levelNumber +1);
        PlayerPrefs.GetInt("levelNumber");
        SceneManager.LoadScene(0); 
    }
}
