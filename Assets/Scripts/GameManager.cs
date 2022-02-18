using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    public int levelNumber;
    public GameObject currentLevel;
    public GameObject NextLevelPrefab;
    public GameObject RetryPrefab;
    void Awake()
    {
        gameManager = this;
        NextLevelPrefab.SetActive(false);
        RetryPrefab.SetActive(false);
    }
    void Start()
    {
        if (PlayerPrefs.HasKey("levelNumber"))
        {
            if(PlayerPrefs.GetInt("levelNumber")==4) PlayerPrefs.SetInt("levelNumber", 1);
            
            else
            {
                levelNumber = PlayerPrefs.GetInt("levelNumber");

            }
        }
        else
        {
            PlayerPrefs.SetInt("levelNumber", 1); 
        }

        SpawnLevel();
    }

    public bool PlayGame;

    void SpawnLevel()
    {
        

        GameObject level = Resources.Load<GameObject>("Levels/Level" + levelNumber);
        currentLevel = Instantiate(level); 
    }
}
