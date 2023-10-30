using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : Singleton<ScoreManager> 
{
    private TMP_Text text;
    private int current;
    public void Increase(int amount)
    {   
        current += amount;
    }
    void Start()
    {
        current = 0;
        text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        text.text = current.ToString();
        if (current == 300)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
