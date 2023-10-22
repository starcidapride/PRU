using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private Button _playAgainButton;
    [SerializeField]
    private Button _quitButton;
    void Start()
    {
        _playAgainButton.onClick.AddListener(() => LoadingSceneManager.Instance.LoadScene(SceneName.GamePlay));
        _quitButton.onClick.AddListener(() => Application.Quit());  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
