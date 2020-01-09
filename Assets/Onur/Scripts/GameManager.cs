using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static bool gameStarted = false;
   
    
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 300;

        if(PlayerPrefs.GetInt("started") == 0)
        {
            PlayerPrefs.SetInt("started", 1);
            PlayerPrefs.SetInt("humanoid_score", 0);
            PlayerPrefs.SetInt("index_counter", 0);
            PlayerPrefs.SetInt("current_level", 1);
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        
        gameStarted = true;

    }
}
