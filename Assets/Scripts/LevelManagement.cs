using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    public static LevelManagement levelManagement;
    public List<Stage> stage = new List<Stage>();
    public GameObject finishLine;
    public GameObject finishForRun;
    public GameObject LevelCompleted;
    public GameObject GameOver;
    public GameObject ScoreForPaint;

    public GameObject Paintable1;
    public GameObject Paintable2;
    private void Awake()
    {
        Paintable1.SetActive(false);
        Paintable2.SetActive(false);
        LevelCompleted.SetActive(false);
        GameOver.SetActive(false);
        ScoreForPaint.SetActive(false);
        levelManagement = this;
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    [System.Serializable]
    public class Stage
    {
        public GameObject stage; 
        public List<GameObject> obs;
    }
}
